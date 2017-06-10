using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.Clothing;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using SharpArch.Core;
using System.Configuration;
using System.IO;
using FashionAde.Core.ThirdParties;
using FashionAde.Core.Services;

namespace FashionAde.Services
{
    public class OutfitEngineProcessor : IOutfitEngineProcessor
    {
        #region Private Properties

        private HashSet<PreOutfit> outfits = new HashSet<PreOutfit>();
        private HashSet<Combination> flavorCombinations = new HashSet<Combination>();

        private HashSet<Garment> newGarments = null;

        private IList<StyleRule> lstStyleRules = null;
        private IList<FashionFlavor> currentFlavors = null;
        private IList<Garment> currentGarments = null;
        private FashionFlavor currentFlavor = null;

        private string fileName;

        private string SharePath
        {
            get { return ConfigurationManager.AppSettings["OutfitEngine_SharePath"].ToString(); }
        }

        private string LocalDatabasePath
        {
            get { return ConfigurationManager.AppSettings["OutfitEngine_DatabaseFilePath"].ToString(); }
        }

        #endregion

        #region Constructors

        private readonly IOutfitUpdaterService outfitUpdaterService;
        private readonly IStyleRuleRepository styleRuleRepository;
        private readonly IClosetRepository closetRepository;
        private log4net.ILog logger;

        public OutfitEngineProcessor(IStyleRuleRepository styleRuleRepository,
            IClosetRepository closetRepository,
            IOutfitUpdaterService outfitUpdaterService)
        {
            Check.Require(styleRuleRepository != null, "styleRuleRepository may not be null");
            Check.Require(closetRepository != null, "closetRepository may not be null");
            Check.Require(outfitUpdaterService != null, "outfitUpdaterService may not be null");

            this.styleRuleRepository = styleRuleRepository;
            this.closetRepository = closetRepository;
            this.outfitUpdaterService = outfitUpdaterService;
            logger = log4net.LogManager.GetLogger(this.GetType().Namespace);
        }

        #endregion

        #region IOutfitEngineProcessor Members

        private IList<FashionFlavor> fashionFlavors = new List<FashionFlavor>();
        public IList<FashionFlavor> FashionFlavors
        {
            get { return fashionFlavors; }
            set { fashionFlavors = value; }
        }

        public IList<Garment> Garments
        {
            get { return currentGarments; }
            set { currentGarments = value; }
        }

        public virtual Closet Closet { get; set; }

        public bool HasValidCombinations()
        {
            currentFlavors = FashionFlavors;
            currentGarments = Garments;
            lstStyleRules = styleRuleRepository.GetAll();

            return ExecuteOutfitGenerator(null, false);
        }

        public bool CreateCombinations()
        {
            return CreateCombinations(null);
        }

        public bool CreateCombinations(IList<Garment> addedGarments)
        {
            Check.Require(FashionFlavors.Count > 0, "Flavors must be included");
            Check.Require(Garments.Count > 0, "Garments must be included");
            Check.Require(Closet != null, "Closet is required");

            lstStyleRules = styleRuleRepository.GetAll();
            currentFlavors = FashionFlavors;
            currentGarments = Garments;

            return ExecuteOutfitGenerator(addedGarments,true);
        }

        #endregion

        #region Execute Sets

        private IList<Garment> lstGarments;
        private IList<Garment> lstAccesories;
        private bool createRecords;
        private IEnumerable<Combination> accesories1;
        private IEnumerable<Combination> accesories2;
        private IEnumerable<Combination> accesories23;
        private IEnumerable<Combination> accesories24;
        private IEnumerable<Combination> accesories25;
        private IEnumerable<Combination> accesories26;
        private IEnumerable<Combination> accesories27;
        private IEnumerable<Combination> accesories28;
        private IEnumerable<Combination> accesories3;
        private IEnumerable<Combination> accesories4;
        private IEnumerable<Combination> accesories5;
        private IEnumerable<Combination> accesories6;
        private IEnumerable<Combination> accesories7;
        private IEnumerable<Combination> accesories8;
        private StyleRule currentStyleRule;

        private bool ExecuteOutfitGenerator(IList<Garment> addedGarments, bool createRecords)
        {
            newGarments = null;
            flavorCombinations.Clear();
            outfits.Clear();

            this.createRecords = createRecords;
            if (addedGarments != null)
            {
                newGarments = new HashSet<Garment>();
                foreach (Garment g in addedGarments)
                    newGarments.Add(g);
            }

            lstGarments = (from cg in currentGarments where !OutfitValidationService.IsAccessory(cg) select cg).ToList<Garment>();

            logger.DebugFormat("Found {0} garments.", lstGarments.Count);

            lstAccesories = (from cg in currentGarments where OutfitValidationService.IsAccessory(cg) select cg).ToList<Garment>();

            logger.DebugFormat("Found {0} accesories.", lstAccesories.Count);

            if (lstGarments.Count == 0 || lstAccesories.Count == 0)
                return false;

            if (createRecords)
            {
                closetRepository.DbContext.BeginTransaction();
                this.Closet.StartProcessing();
                closetRepository.SaveOrUpdate(this.Closet);
                closetRepository.DbContext.CommitTransaction();
            }

            bool retVal = ExecuteSet();

            if (createRecords)
            {
                closetRepository.DbContext.BeginTransaction();
                this.Closet.MarkAsProcessed();
                closetRepository.SaveOrUpdate(this.Closet);
                closetRepository.DbContext.CommitTransaction();
            }

            return retVal;
        }

        public bool ExecuteSet()
        {
            bool found = false;

            IList<Garment> garmentsA = (from g in lstGarments
                                           where g.Tags.Silouhette.Layers.Contains(LayerCode.A)
                                           select g).ToList<Garment>();
            IList<Garment> garmentsC = (from g in lstGarments
                                           where g.Tags.Silouhette.Layers.Contains(LayerCode.C)
                                           select g).ToList<Garment>();
            IList<Garment> garmentsD = (from g in lstGarments
                                           where g.Tags.Silouhette.Layers.Contains(LayerCode.D)
                                           select g).ToList<Garment>();

            IList<Garment> garmentsAii = (from g in lstGarments
                                             where g.Tags.Silouhette.Layers.Contains(LayerCode.Aii)
                                             select g).ToList<Garment>();

            IList<Garment> garmentsAi = (from g in lstGarments
                                            where g.Tags.Silouhette.Layers.Contains(LayerCode.Ai)
                                            select g).ToList<Garment>();

            IList<Garment> garmentsB = (from g in lstGarments where g.Tags.Silouhette.Layers.Contains(LayerCode.B) select g).ToList<Garment>();
            IList<Garment> garmentsCD = (from g in lstGarments
                                            where g.Tags.Silouhette.Layers.Contains(LayerCode.C) ||
                                             g.Tags.Silouhette.Layers.Contains(LayerCode.D)
                                         select g).ToList<Garment>();
            IList<Garment> garmentsBCD = (from g in lstGarments
                                             where g.Tags.Silouhette.Layers.Contains(LayerCode.C) ||
                                              g.Tags.Silouhette.Layers.Contains(LayerCode.D) ||
                                              g.Tags.Silouhette.Layers.Contains(LayerCode.B)
                                          select g).ToList<Garment>();

            //  A + B, A+C, A+D 
            IEnumerable<Combination> comb = Combine(2, garmentsA, garmentsBCD, null, null, null);

            // A+B+C, A+B+D
            IEnumerable<Combination> combABC_D = Combine(3, garmentsA, garmentsB, garmentsCD, null, null);

            // A+C+D
            IEnumerable<Combination> combACD = Combine(3, garmentsA, garmentsC, garmentsD, null, null);

            // Ai + Aii + B, Ai + Aii + C, Ai + Aii + D
            IEnumerable<Combination> combAii = Combine(3, garmentsAi, garmentsAii, garmentsBCD, null, null);

            // A+B+C+D
            IEnumerable<Combination> combABCD = Combine(4, garmentsA, garmentsB, garmentsC, garmentsD, null);

            // Ai + Aii + B + C, Ai + Aii + C + D
            IEnumerable<Combination> combAiAiiBC = Combine(4, garmentsAi, garmentsAii, garmentsB, garmentsC, null);
            IEnumerable<Combination> combAiAiiCD = Combine(4, garmentsAi, garmentsAii, garmentsC, garmentsD, null);

            // Ai + Aii + B + C + D
            IEnumerable<Combination> combAiAiiBCD = Combine(5, garmentsAi, garmentsAii, garmentsB, garmentsC, garmentsD);

            // Find accesories
            var linqAccesories1 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC1)
                                   select g).ToList<Garment>();

            var linqAccesories2 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC2)
                                   select g).ToList<Garment>();

            var linqAccesories3 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC3)
                                   select g).ToList<Garment>();

            var linqAccesories4 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC4)
                                   select g).ToList<Garment>();

            var linqAccesories5 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC5)
                                   select g).ToList<Garment>();

            var linqAccesories6 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC6)
                                   select g).ToList<Garment>();

            var linqAccesories7 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC7)
                                   select g).ToList<Garment>();

            var linqAccesories8 = (from g in lstAccesories
                                   where g.Tags.Silouhette.Layers.Contains(LayerCode.ACC8)
                                   select g).ToList<Garment>();

            accesories1 = Combine(1, linqAccesories1, null, null, null, null);
            accesories2 = Combine(2, linqAccesories1, linqAccesories2, null, null, null);

            accesories23 = Combine(2, linqAccesories1, linqAccesories3, null, null, null);
            accesories24 = Combine(2, linqAccesories1, linqAccesories4, null, null, null);
            accesories25 = Combine(2, linqAccesories1, linqAccesories5, null, null, null);
            accesories26 = Combine(2, linqAccesories1, linqAccesories6, null, null, null);
            accesories27 = Combine(2, linqAccesories1, linqAccesories7, null, null, null);
            accesories28 = Combine(2, linqAccesories1, linqAccesories8, null, null, null);

            accesories3 = Combine(3, linqAccesories1, linqAccesories2, linqAccesories3, null, null);
            accesories4 = Combine(4, linqAccesories1, linqAccesories2, linqAccesories3, linqAccesories4, null);
            accesories5 = Combine(5, linqAccesories1, linqAccesories2, linqAccesories3, linqAccesories4, linqAccesories5);
            accesories6 = Combine(6, linqAccesories1, linqAccesories2, linqAccesories3, linqAccesories4, linqAccesories5, linqAccesories6, null, null);
            accesories7 = Combine(7, linqAccesories1, linqAccesories2, linqAccesories3, linqAccesories4, linqAccesories5, linqAccesories6, linqAccesories7, null);
            accesories8 = Combine(8, linqAccesories1, linqAccesories2, linqAccesories3, linqAccesories4, linqAccesories5, linqAccesories6, linqAccesories7, linqAccesories8);

            foreach (FashionFlavor fv in currentFlavors)
            {
                logger.DebugFormat("Started Flavor {0}.", fv.Name);

                this.currentFlavor = fv;

                var styleRule = from s in lstStyleRules
                                where s.FashionFlavor.Id == fv.Id
                                select s;

                currentStyleRule = styleRule.First<StyleRule>();

                for (int i = currentStyleRule.MinimumLayers; i <= currentStyleRule.MaximumLayers; i++)
                {
                    bool recordsFound = false;
                    logger.DebugFormat("Started Layers {0}", i);
                    switch (i)
                    {
                        case 2:

                            recordsFound = ExcludeCombinations(currentStyleRule, comb);

                            break;
                        case 3:

                            recordsFound = ExcludeCombinations(currentStyleRule, combABC_D);


                            recordsFound = recordsFound || ExcludeCombinations(currentStyleRule, combACD);


                            recordsFound = recordsFound || ExcludeCombinations(currentStyleRule, combAii);

                            break;
                        case 4:
                            recordsFound = ExcludeCombinations(currentStyleRule, combABCD);

                            recordsFound = recordsFound || ExcludeCombinations(currentStyleRule, combAiAiiBC);

                            recordsFound = recordsFound || ExcludeCombinations(currentStyleRule, combAiAiiCD);

                            break;
                        case 5:

                            recordsFound = ExcludeCombinations(currentStyleRule, combAiAiiBCD);

                            break;

                    }

                    // No need to continue to check.
                    if (!createRecords && recordsFound)
                        return recordsFound;

                    found = found || recordsFound;
                }
            }

            if (createRecords && found)
            {
                logger.DebugFormat("Adding outfit updaters");
                outfitUpdaterService.MatchOutfitUpdatersForCloset(Closet.Id);
                logger.DebugFormat("Finish adding outfit updaters");
            }

            return found;
        }

        public bool ProcessFoundCombinations(StyleRule sr)
        {
            bool found = false;
            if (flavorCombinations.Count == 0)
                return found;

            var limitCombinations = (from fc in flavorCombinations orderby fc.EditorRating descending select fc).Take(5000);

            if (lstAccesories.Count != 0)
            {
                logger.DebugFormat("Joinining {0} combinations with {1} accesories", flavorCombinations.Count, lstAccesories.Count);

                if (createRecords)
                    this.fileName = Guid.NewGuid().ToString() + ".txt";

                foreach (int i in sr.AccessoriesAmount)
                {
                    switch (i)
                    {
                        case 1:
                            // Only ACC1.
                            this.IncludeAccesories(limitCombinations, accesories1, 1);
                            break;
                        case 2:
                            // Only ACC1, ACC2.
                            this.IncludeAccesories(limitCombinations, accesories2, 2);
                            this.IncludeAccesories(limitCombinations, accesories23, 2);
                            this.IncludeAccesories(limitCombinations, accesories24, 2);
                            this.IncludeAccesories(limitCombinations, accesories25, 2);
                            this.IncludeAccesories(limitCombinations, accesories26, 2);
                            this.IncludeAccesories(limitCombinations, accesories27, 2);
                            this.IncludeAccesories(limitCombinations, accesories28, 2);
                            break;
                        case 3:
                            // Only ACC1, ACC2, ACC3.
                            this.IncludeAccesories(limitCombinations, accesories3, 3);
                            break;
                        case 4:
                            // Only ACC1, ACC2, ACC3, ACC4.
                            this.IncludeAccesories(limitCombinations, accesories4, 4);
                            break;
                        case 5:
                            // Only ACC1, ACC2, ACC3, ACC4, ACC5.
                            this.IncludeAccesories(limitCombinations, accesories5, 5);
                            this.IncludeAccesories(limitCombinations, accesories6, 6);
                            this.IncludeAccesories(limitCombinations, accesories7, 7);
                            this.IncludeAccesories(limitCombinations, accesories8, 8);
                            break;
                    }

                    // Don't keep processing if we are not going to save them.
                    if (!createRecords && this.outfits.Count > 0)
                        return true;

                    logger.DebugFormat("Found {0} outfits for {1} accesories.", outfits.Count, i);

                    // Save each combination while processing.
                    if (outfits.Count > 0)
                    {
                        found = true;
                        if (createRecords)
                        {
                            logger.DebugFormat("Import to database with file: {0}", fileName);
                            SaveToFile();
                            closetRepository.ProcessClosetFile(Path.Combine(this.LocalDatabasePath, fileName));

                            logger.DebugFormat("Update database with file: {0}", fileName);
                            closetRepository.CompleteClosetCreation(this.Closet.Id);
                            logger.DebugFormat("End update database with file: {0}", fileName);
                        }
                    }

                    outfits.Clear();
                }
            }

            flavorCombinations.Clear();

            return found;
        }
        #endregion

        #region Save To File

        public const int lineSize = 150;
        public const int maxLineSize = 190;
        public const int maxRecords = 200000;

        public void SaveToFile()
        {
            int i = 0;

            StringBuilder sb = new StringBuilder(lineSize * 200000, maxLineSize * maxRecords);

            foreach (PreOutfit po in this.outfits)
            {

                Combination pc = po.Combination;

                StringBuilder lb = new StringBuilder(lineSize,maxLineSize);

                // Add Closet & Flavor
                lb.Append(this.Closet.Id);
                lb.Append(",");
                lb.Append(pc.FashionFlavor.Id);

                int used = 2;

                // Let's setup in the correct order to avoid duplicates.
                Garment[] arrItems = new Garment[13];
                foreach (Garment g in po.ToList())
                {
                    // Always use first silouhette to position
                    int pos = OutfitValidationService.GetClosetOutfitPosition(g);
                    arrItems[pos] = g;
                }

                foreach (Garment g in arrItems)
                {
                    if (g != null)
                    {
                        lb.Append(",");
                        lb.Append(g.Id);
                        lb.Append(",");
                        lb.Append(g.PreGarment.Id);
                        used++;
                    }
                    else
                    {
                        lb.Append(",");
                        lb.Append(@"0");
                        lb.Append(",");
                        lb.Append(@"0");
                    }
                }

                lb.Append(",");
                lb.Append(po.Seasons.ToString());
                lb.Append(",");
                lb.Append(po.EventTypes.ToString());
                lb.Append(",");
                lb.Append(pc.EditorRating);

                sb.AppendLine(lb.ToString());

                i++;
                if (i == 200000)
                {
                    System.IO.File.AppendAllText(Path.Combine(this.SharePath,fileName), sb.ToString());
                    sb = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    sb = new StringBuilder(lineSize * maxRecords, maxLineSize * maxRecords);
                    i = 0;
                }
            }

            if (i > 0)
            {
                System.IO.File.AppendAllText(Path.Combine(this.SharePath, fileName), sb.ToString());
                sb = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        #endregion

        #region Exclude Combinations

        private const int MAX_ACCESORIES_PER_COMBINATION = 20;
        private const int MAX_COMBINATIONS_PER_SET = 5000;

        private void IncludeAccesories(IEnumerable<Combination> currentCombinations, IEnumerable<Combination> accesories, int amount)
        {
            HashSet<int> seasons = new HashSet<int>();
            HashSet<int> eventTypes = new HashSet<int>();

            foreach (Combination comb in currentCombinations)
            {
                // Make sure there will be only 10 garments per outfit.
                int combinationAmount = 2;
                if (comb.GarmentC != null)
                    combinationAmount++;
                if (comb.GarmentD != null)
                    combinationAmount++;
                if (comb.GarmentE != null)
                    combinationAmount++;

                if (combinationAmount + amount > 10)
                    continue;

                int i = 0;

                foreach (Combination acc in accesories)
                {
                    // Exclude combination if does not contains an added garment.
                    if (newGarments != null)
                    {
                        if (!newGarments.Contains(comb.GarmentA) && !newGarments.Contains(comb.GarmentB) && 
                            !newGarments.Contains(comb.GarmentC) && !newGarments.Contains(comb.GarmentD) && 
                            !newGarments.Contains(comb.GarmentE) && !newGarments.Contains(acc.GarmentA) &&
                            !newGarments.Contains(acc.GarmentB) && !newGarments.Contains(acc.GarmentC) &&
                            !newGarments.Contains(acc.GarmentD) && !newGarments.Contains(acc.GarmentE) &&
                            !newGarments.Contains(acc.GarmentF) && !newGarments.Contains(acc.GarmentG) &&
                            !newGarments.Contains(acc.GarmentH)
                            )
                            continue;
                    }

                    PreOutfit outfit = new PreOutfit();
                    outfit.Combination = comb;
                    outfit.Accesory1 = acc.GarmentA;
                    outfit.Accesory2 = acc.GarmentB;
                    outfit.Accesory3 = acc.GarmentC;
                    outfit.Accesory4 = acc.GarmentD;
                    outfit.Accesory5 = acc.GarmentE;
                    outfit.Accesory6 = acc.GarmentF;
                    outfit.Accesory7 = acc.GarmentG;
                    outfit.Accesory8 = acc.GarmentH;

                    if (!OutfitValidationService.IsValidOutfit(outfit, currentStyleRule, seasons, eventTypes))
                        continue;

                    outfit.Seasons = seasons.Sum();
                    outfit.EventTypes = eventTypes.Sum();
                    outfits.Add(outfit);

                    seasons.Clear();
                    eventTypes.Clear();

                    // Limit to 20 accesories per combination maximum.
                    if (i >= MAX_ACCESORIES_PER_COMBINATION)
                        break;

                    i++;
                }
            }
        }

        public bool ExcludeCombinations(StyleRule sr, IEnumerable<Combination> lst)
        {
            foreach (Combination cb in lst)
            {
                if (!OutfitValidationService.IsValidCombination(cb, sr))
                    continue;

                HashSet<Fabric> fabrics = new HashSet<Fabric>();
                if (cb.GarmentA != null) fabrics.Add(cb.GarmentA.Tags.Fabric);
                if (cb.GarmentB != null) fabrics.Add(cb.GarmentB.Tags.Fabric);
                if (cb.GarmentC != null) fabrics.Add(cb.GarmentC.Tags.Fabric);
                if (cb.GarmentD != null) fabrics.Add(cb.GarmentD.Tags.Fabric);
                if (cb.GarmentE != null) fabrics.Add(cb.GarmentE.Tags.Fabric);

                HashSet<Color> colors = new HashSet<Color>();
                if (cb.GarmentA != null) colors.Add(cb.GarmentA.Tags.Colors[0]);
                if (cb.GarmentB != null) colors.Add(cb.GarmentB.Tags.Colors[0]);
                if (cb.GarmentC != null) colors.Add(cb.GarmentC.Tags.Colors[0]);
                if (cb.GarmentD != null) colors.Add(cb.GarmentD.Tags.Colors[0]);
                if (cb.GarmentE != null) colors.Add(cb.GarmentE.Tags.Colors[0]);

                HashSet<PatternType> pt = new HashSet<PatternType>();
                if (cb.GarmentA != null) pt.Add(cb.GarmentA.Tags.Pattern.Type);
                if (cb.GarmentB != null) pt.Add(cb.GarmentB.Tags.Pattern.Type);
                if (cb.GarmentC != null) pt.Add(cb.GarmentC.Tags.Pattern.Type);
                if (cb.GarmentD != null) pt.Add(cb.GarmentD.Tags.Pattern.Type);
                if (cb.GarmentE != null) pt.Add(cb.GarmentE.Tags.Pattern.Type);

                cb.EditorRating = EditorRatingCalculatorService.CalculateRating(colors, fabrics, pt);

                cb.FashionFlavor = sr.FashionFlavor;
                flavorCombinations.Add(cb);
            }

            if (flavorCombinations.Count > 0)
                return ProcessFoundCombinations(sr);

            return false;
        }

        #endregion

        #region Combine Cartesian Method

        public IEnumerable<Combination> Combine(int minAmount, IEnumerable<Garment> garmentListFor1, IEnumerable<Garment> garmentListFor2, IEnumerable<Garment> garmentListFor3, IEnumerable<Garment> garmentListFor4, IEnumerable<Garment> garmentListFor5)
        {
            return Combine(minAmount, garmentListFor1, garmentListFor2, garmentListFor3, garmentListFor4, garmentListFor5, null, null, null);
        }

        public IEnumerable<Combination> Combine(int minAmount, IEnumerable<Garment> garmentListFor1, IEnumerable<Garment> garmentListFor2, IEnumerable<Garment> garmentListFor3, IEnumerable<Garment> garmentListFor4, IEnumerable<Garment> garmentListFor5, IEnumerable<Garment> garmentListFor6, IEnumerable<Garment> garmentListFor7, IEnumerable<Garment> garmentListFor8)
        {
            foreach (Garment ga in garmentListFor1)
            {
                if (garmentListFor2 == null && minAmount == 1)
                {
                    yield return new Combination { GarmentA = ga };
                    continue;
                }

                foreach (Garment gb in garmentListFor2)
                {
                    if (garmentListFor3 == null && minAmount == 2)
                    {
                        yield return new Combination { GarmentA = ga, GarmentB = gb };
                        continue;
                    }

                    foreach (Garment gc in garmentListFor3)
                    {
                        if (garmentListFor4 == null && minAmount == 3)
                        {
                            yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc };
                            continue;
                        }

                        foreach (Garment gd in garmentListFor4)
                        {
                            if (garmentListFor5 == null && minAmount == 4)
                            {
                                yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc, GarmentD = gd };
                                continue;
                            }

                            foreach (Garment ge in garmentListFor5)
                            {
                                if (garmentListFor6 == null && minAmount == 5)
                                {
                                    yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc, GarmentD = gd, GarmentE = ge };
                                    continue;
                                }

                                foreach (Garment gf in garmentListFor6)
                                {
                                    if (garmentListFor7 == null && minAmount == 6)
                                    {
                                        yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc, GarmentD = gd, GarmentE = ge, GarmentF = gf };
                                        continue;
                                    }

                                    foreach (Garment gg in garmentListFor7)
                                    {
                                        if (garmentListFor8 == null && minAmount == 7)
                                        {
                                            yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc, GarmentD = gd, GarmentE = ge, GarmentF = gf, GarmentG = gg };
                                            continue;
                                        }

                                        foreach (Garment gh in garmentListFor8)
                                        {
                                            yield return new Combination { GarmentA = ga, GarmentB = gb, GarmentC = gc, GarmentD = gd, GarmentE = ge, GarmentF = gf, GarmentG = gg, GarmentH = gh };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
