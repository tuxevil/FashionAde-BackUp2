using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core;
using FashionAde.Core.Clothing;

namespace FashionAde.Services
{
    public interface IOutfitEngineProcessor
    {
        /// <summary>
        /// List of flavors to process
        /// </summary>
        IList<FashionFlavor> FashionFlavors { get; set; }

        /// <summary>
        /// List of garments to be combined
        /// </summary>
        IList<Garment> Garments { get; set; }

        /// <summary>
        /// Closet to create the combinations to.
        /// </summary>
        Closet Closet { get; set; }

        /// <summary>
        /// Validates if there are any possible combination.
        /// </summary>
        /// <returns>True if combinations are found, otherwise false.</returns>
        bool HasValidCombinations();

        /// <summary>
        /// Creates any possible combination with the current garments and closet.
        /// </summary>
        /// <returns>True if combinations are created, otherwise false.</returns>
        bool CreateCombinations();

        /// <summary>
        /// Creates any possible combination that only includes the new garments or accessories, and fill the Outfits property.
        /// </summary>
        /// <returns>True if combinations are created, otherwise false.</returns>
        bool CreateCombinations(IList<Garment> addedGarments);
    }
}
