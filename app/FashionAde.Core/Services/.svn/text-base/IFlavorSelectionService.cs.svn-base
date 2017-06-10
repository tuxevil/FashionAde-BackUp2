using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Core.FlavorSelection
{
    public interface IFlavorSelectionService
    {
        IList<BrandSet> BrandSets { get; set; }
        IList<StylePhotograph> StylePhotographs { get; set;}
        IList<Wording> Wordings { get; set; }

        /// <summary>
        /// Determines the best matched flavors given the user selections.
        /// </summary>
        /// <returns>At most two fashion flavors</returns>
        IList<FashionFlavor> DetermineFlavors();
    }
}
