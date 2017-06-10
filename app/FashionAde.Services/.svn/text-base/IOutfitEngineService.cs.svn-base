using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using System.ServiceModel;

namespace FashionAde.Services
{
    [ServiceContract]
    public interface IOutfitEngineService
    {
        /// <summary>
        /// Validates if the selected garments are valid for a collection of flavors.
        /// </summary>
        /// <param name="garments">List of garments</param>
        /// <param name="flavors">Flavors selected</param>
        /// <returns>True if is valid. Otherwise false.</returns>
        [OperationContract]
        bool HasValidCombinations(IList<int> garments, IList<int> flavors);

        /// <summary>
        /// Create combinations for the whole closet
        /// </summary>
        /// <param name="closetId">Closet to process</param>
        [OperationContract(IsOneWay = true)]
        void CreateOutfits(int closetId);

        /// <summary>
        /// Create combinations that contain only the garments supplied on the set.
        /// </summary>
        /// <param name="closetId">Closet to process</param>
        /// <param name="addedGarments">Garments to be validated</param>
        [OperationContract(IsOneWay = true)]
        void AddOutfits(int closetId, IList<int> addedGarments);
    }
}
