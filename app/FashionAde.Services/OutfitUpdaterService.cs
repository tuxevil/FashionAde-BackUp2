using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using FashionAde.Data.Repository;
using FashionAde.Core.Clothing;
using System.IO;
using System.Configuration;
using FashionAde.OutfitUpdaterImportation.Controllers;
using FashionAde.OutfitUpdaterImportation.Core;
using FashionAde.Core.Services;

namespace FashionAde.Services
{
    public class OutfitUpdaterService : IOutfitUpdaterService
    {
        #region Constructors

        private IOutfitUpdaterRepository outfitUpdaterRepository;
        private IPreCombinationRepository preCombinationRepository;
        private IStyleRuleRepository styleRuleRepository;
        private IPartnerRepository partnerRepository;
        private ISilouhetteRepository silouhetteRepository;
        private IPatternRepository patternRepository;
        private IColorFamilyRepository colorFamilyRepository;
        private ITrendRepository trendRepository;
        private log4net.ILog logger;

        public OutfitUpdaterService(IOutfitUpdaterRepository outfitUpdaterRepository, IPreCombinationRepository preCombinationRepository, IStyleRuleRepository styleRuleRepository)
        {
            this.outfitUpdaterRepository = outfitUpdaterRepository;
            this.preCombinationRepository = preCombinationRepository;
            this.styleRuleRepository = styleRuleRepository;
            logger = log4net.LogManager.GetLogger(this.GetType().Namespace);
        }

        //For update feeds
        public OutfitUpdaterService(IOutfitUpdaterRepository outfitUpdaterRepository, IPreCombinationRepository preCombinationRepository, IStyleRuleRepository styleRuleRepository, ISilouhetteRepository silouhetteRepository, IPatternRepository patternRepository, IColorFamilyRepository colorFamilyRepository, ITrendRepository trendRepository, IPartnerRepository partnerRepository)
        {
            this.outfitUpdaterRepository = outfitUpdaterRepository;
            this.preCombinationRepository = preCombinationRepository;
            this.styleRuleRepository = styleRuleRepository;
            this.silouhetteRepository = silouhetteRepository;
            this.patternRepository = patternRepository;
            this.colorFamilyRepository = colorFamilyRepository;
            this.trendRepository = trendRepository;
            this.partnerRepository = partnerRepository;
            logger = log4net.LogManager.GetLogger(this.GetType().Namespace);
        }

        #endregion

        #region Private Properties

        private string SharePath
        {
            get { return ConfigurationManager.AppSettings["OutfitEngine_SharePath"].ToString(); }
        }

        private string LocalDatabasePath
        {
            get { return ConfigurationManager.AppSettings["OutfitEngine_DatabaseFilePath"].ToString(); }
        }

        #endregion

        public void UpdateFeeds()
        {
            try
            {
                logger.InfoFormat("Starting to update feeds");
                OUImportationController temp = new OUImportationController(new ZapposClassBuilder(), outfitUpdaterRepository, trendRepository, silouhetteRepository, patternRepository, colorFamilyRepository);
                temp.HaveHeader = true;
                temp.Separator = "\t";
                temp.Filename = "Zappos_Complete.txt";
                temp.Partner = partnerRepository.GetByCode("ZP");
                temp.MemorySafe = Convert.ToDouble(5);

                logger.InfoFormat("Starting to import");
                temp.Create();

                logger.InfoFormat("Match updaters");
                MatchOutfitUpdaters();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void MatchOutfitUpdatersForCloset(int closetId)
        {
            try
            {
                logger.InfoFormat("Matching updaters");
                IList<PreCombination> combs = preCombinationRepository.GetNewsFor(new Closet(closetId));
                logger.DebugFormat("Searching updaters");
                IList<OutfitUpdater> updaters = outfitUpdaterRepository.GetOnly(OutfitUpdaterStatus.Processed);
                logger.DebugFormat("Starting to match");
                IList<OutfitUpdaterByPreCombination> result = GetResult(updaters, combs);
                if (result.Count > 0)
                {
                    logger.DebugFormat("Changing Precombination Status to Processed");
                    preCombinationRepository.ChangePreCombinationsStatus(new Closet(closetId));
                }

                logger.InfoFormat("{0} matches created.", result.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        private void MatchOutfitUpdaters()
        {
            try
            {
                IList<OutfitUpdaterByPreCombination> result = GetResult(outfitUpdaterRepository.GetOnly(OutfitUpdaterStatus.Valid), preCombinationRepository.GetOnly(PreCombinationStatus.Processed));
                outfitUpdaterRepository.ChangeOutfitUpdatersStatus();
                logger.InfoFormat("{0} matches created.", result.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
        
        #region OutfitUpdatersByPreCombination
        private IList<OutfitUpdaterByPreCombination> GetResult(IList<OutfitUpdater> outfitUpdaters, IList<PreCombination> pcb)
        {
            logger.DebugFormat("Found {0} valid outfit updaters to match.", outfitUpdaters.Count);
            logger.DebugFormat("Found {0} precombinations pending to be updated.", pcb.Count);

            if (outfitUpdaters.Count > 0)
            {
                IList<OutfitUpdaterByPreCombination> result = GetOutfitUpdatersByPreCombinations(outfitUpdaters, pcb);
                logger.DebugFormat("Found {0} matches to be created.", result.Count);
                if (result.Count > 0)
                {
                    string filename = SaveOutfitUpdatersByPreCombinationsToFile(result);
                    outfitUpdaterRepository.ProcessOutfitUpdatersByPreCombinationsFile(filename);
                }

                return result;
            }
            else
            {
                logger.DebugFormat("No valid outfit updaters to match. Process ended.");
                return new List<OutfitUpdaterByPreCombination>();
            }

        }
        
        private IList<OutfitUpdaterByPreCombination> GetOutfitUpdatersByPreCombinations(IEnumerable<OutfitUpdater> outfitUpdaters, IEnumerable<PreCombination> lst)
        {
            IList<StyleRule> lstStyleRules = styleRuleRepository.GetAll();
            
            //REVIEW: We are not expecting duplicates here, no need for HashSet.
            IList<OutfitUpdaterByPreCombination> result = new List<OutfitUpdaterByPreCombination>();

            // Exclude By Rules
            foreach (PreCombination pcb in lst)
            {
                var styleRule = from s in lstStyleRules
                                where s.FashionFlavor.Id == pcb.FashionFlavor.Id
                                select s;

                StyleRule sr = styleRule.First<StyleRule>();

                logger.InfoFormat("Matching PrecombinationId : {0}", pcb.Id);

                foreach (OutfitUpdater outfitUpdater in outfitUpdaters)
                {
                    HashSet<ColorFamily> colorFamiliesHash = new HashSet<ColorFamily>();
                    if (pcb.Garment1 != null && pcb.Garment1.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment1)) colorFamiliesHash.Add(pcb.Garment1.ColorFamily);
                    if (pcb.Garment2 != null && pcb.Garment2.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment2)) colorFamiliesHash.Add(pcb.Garment2.ColorFamily);
                    if (pcb.Garment3 != null && pcb.Garment3.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment3)) colorFamiliesHash.Add(pcb.Garment3.ColorFamily);
                    if (pcb.Garment4 != null && pcb.Garment4.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment4)) colorFamiliesHash.Add(pcb.Garment4.ColorFamily);
                    if (pcb.Garment5 != null && pcb.Garment5.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment5)) colorFamiliesHash.Add(pcb.Garment5.ColorFamily);
                    colorFamiliesHash.Add(outfitUpdater.ColorFamily);

                    HashSet<PatternType> pt = new HashSet<PatternType>();
                    if (pcb.Garment1 != null && pcb.Garment1.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment1)) pt.Add(pcb.Garment1.PatternType);
                    if (pcb.Garment2 != null && pcb.Garment2.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment2)) pt.Add(pcb.Garment2.PatternType);
                    if (pcb.Garment3 != null && pcb.Garment3.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment3)) pt.Add(pcb.Garment3.PatternType);
                    if (pcb.Garment4 != null && pcb.Garment4.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment4)) pt.Add(pcb.Garment4.PatternType);
                    if (pcb.Garment5 != null && pcb.Garment5.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment5)) pt.Add(pcb.Garment5.PatternType);
                    pt.Add(outfitUpdater.Pattern.Type);

                    HashSet<StructureType> st = new HashSet<StructureType>();
                    if (pcb.Garment1 != null && pcb.Garment1.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment1)) st.Add(pcb.Garment1.Silouhette.Structure.Type);
                    if (pcb.Garment2 != null && pcb.Garment2.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment2)) st.Add(pcb.Garment2.Silouhette.Structure.Type);
                    if (pcb.Garment3 != null && pcb.Garment3.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment3)) st.Add(pcb.Garment3.Silouhette.Structure.Type);
                    if (pcb.Garment4 != null && pcb.Garment4.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment4)) st.Add(pcb.Garment4.Silouhette.Structure.Type);
                    if (pcb.Garment5 != null && pcb.Garment5.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment5)) st.Add(pcb.Garment5.Silouhette.Structure.Type);
                    if (outfitUpdater.Silouhette.Structure != null)
                        st.Add(outfitUpdater.Silouhette.Structure.Type);

                    HashSet<ShapeType> sh = new HashSet<ShapeType>();
                    if (pcb.Garment1 != null && pcb.Garment1.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment1)) sh.Add(pcb.Garment1.Silouhette.Shape.Type);
                    if (pcb.Garment2 != null && pcb.Garment2.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment2)) sh.Add(pcb.Garment2.Silouhette.Shape.Type);
                    if (pcb.Garment3 != null && pcb.Garment3.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment3)) sh.Add(pcb.Garment3.Silouhette.Shape.Type);
                    if (pcb.Garment4 != null && pcb.Garment4.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment4)) sh.Add(pcb.Garment4.Silouhette.Shape.Type);
                    if (pcb.Garment5 != null && pcb.Garment5.Id != 0 && !OutfitValidationService.IsAccessory(pcb.Garment5)) sh.Add(pcb.Garment5.Silouhette.Shape.Type);
                    if (outfitUpdater.Silouhette.Shape != null)
                        sh.Add(outfitUpdater.Silouhette.Shape.Type);

                    if (OutfitValidationService.IsValidCombination(
                        colorFamiliesHash, 
                        null,
                        pt, 
                        st, 
                        sr, 
                        sh, 
                        pcb.Garment1.Silouhette.Layers.Contains(LayerCode.Ai), 
                        OutfitValidationService.IsAccessory(outfitUpdater.Silouhette)))
                        result.Add(new OutfitUpdaterByPreCombination(outfitUpdater, pcb));
                }
            }

            return result;
        }

        private const int lineSize = 30;
        private const int maxLineSize = 50;
        private const int maxRecords = 500000;

        private string SaveOutfitUpdatersByPreCombinationsToFile(IList<OutfitUpdaterByPreCombination> outfitUpdaterByPreCombinations)
        {
            string filename = Guid.NewGuid().ToString() + ".txt";
            logger.DebugFormat("Saving to file for import: ", filename);

            int i = 0;

            StringBuilder sb = new StringBuilder(lineSize * maxRecords, maxLineSize * maxRecords);

            foreach (OutfitUpdaterByPreCombination item in outfitUpdaterByPreCombinations)
            {
                StringBuilder lb = new StringBuilder(lineSize, maxLineSize);

                // Add Closet & Flavor
                lb.Append(item.OutfitUpdater.Id);
                lb.Append(",");
                lb.Append(item.PreCombination.Id);

                sb.AppendLine(lb.ToString());

                i++;
                if (i == 200000)
                {
                    System.IO.File.AppendAllText(Path.Combine(this.SharePath, filename), sb.ToString());
                    sb = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    sb = new StringBuilder(lineSize * maxRecords, maxLineSize * maxRecords);
                    i = 0;
                }
            }

            if (i > 0)
            {
                System.IO.File.AppendAllText(Path.Combine(this.SharePath, filename), sb.ToString());
                sb = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return LocalDatabasePath + @"\" + filename;
        }

        #endregion

    }
}
