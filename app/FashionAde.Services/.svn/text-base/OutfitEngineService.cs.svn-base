using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using SharpArch.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core;
using System.Collections;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.OutfitEngine;
using System.Threading;
using log4net;

namespace FashionAde.Services
{
    public class OutfitEngineService : IOutfitEngineService
    {
        #region Constructors

        private readonly IGarmentRepository garmentRepository;
        private readonly IClosetRepository closetRepository;
        private readonly IOutfitEngineProcessor processor;
        private IFashionFlavorRepository fashionFlavorRepository;
        private ILog logger;

        public OutfitEngineService(IGarmentRepository garmentRepository, IClosetRepository closetRepository, IOutfitEngineProcessor processor, IFashionFlavorRepository fashionFlavorRepository)
        {
            this.garmentRepository = garmentRepository;
            this.closetRepository = closetRepository;
            this.processor = processor;
            this.fashionFlavorRepository = fashionFlavorRepository;
            logger = log4net.LogManager.GetLogger(this.GetType().Namespace);
        }

        #endregion

        public bool HasValidCombinations(IList<int> garments, IList<int> flavors)
        {
            try
            {
                string log = string.Empty;
                foreach (int i in garments)
                    log += i.ToString() + ",";

                logger.InfoFormat("Trying to combine these garments: {0}", log);
                processor.Garments = garmentRepository.GetByIds(garments);
                processor.FashionFlavors = fashionFlavorRepository.GetByIds(flavors);

                bool result = processor.HasValidCombinations();
                logger.InfoFormat("Result of combining garments: {0}", result);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Creates the necessary combinations for a current closet
        /// </summary>
        /// <param name="c">Current closet</param>
        public void CreateOutfits(int closetId)
        {
            logger.InfoFormat("Creating full closet {0}", closetId);

            try
            {
                UpdateOutfits(closetId, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Creates the necessary combinations for a current closet
        /// </summary>
        /// <param name="c">Current closet</param>
        public void AddOutfits(int closetId, IList<int> addedGarments)
        {
            logger.InfoFormat("Adding {0} outfits for closet {0}", addedGarments.Count, closetId);

            try
            {
                UpdateOutfits(closetId, addedGarments);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        private void UpdateOutfits(int closetId, IList<int> addedGarments)
        {
            processor.Closet = closetRepository.Get(closetId);

            processor.FashionFlavors.Clear();
            foreach (UserFlavor uf in processor.Closet.User.UserFlavors)
                processor.FashionFlavors.Add(uf.Flavor);

            processor.Garments = garmentRepository.GetForProcess(closetId);
            processor.CreateCombinations(garmentRepository.GetByIds(addedGarments));
        }
    }
}