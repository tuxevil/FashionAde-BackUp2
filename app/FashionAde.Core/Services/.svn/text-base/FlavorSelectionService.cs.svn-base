using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.FlavorSelection;

namespace FashionAde.Core.Services
{
    public class FlavorSelectionService : IFlavorSelectionService
    {
        #region IFlavorSelectionService Members

        private IList<BrandSet> brandSets = new List<BrandSet>();
        private IList<StylePhotograph> stylePhotographs = new List<StylePhotograph>();
        private IList<Wording> wordings = new List<Wording>();

        public IList<BrandSet> BrandSets
        {
            get { return brandSets; }
            set { brandSets = value; }
        }

        public IList<StylePhotograph> StylePhotographs
        {
            get { return stylePhotographs; }
            set { stylePhotographs = value; }
        }

        public IList<Wording> Wordings
        {
            get { return wordings; }
            set { wordings = value; }
        }

        public IList<FashionFlavor> DetermineFlavors()
        {
            List<UserFlavor> result = new List<UserFlavor>();

            foreach (BrandSet brandSet in BrandSets)
                Add(result, brandSet.Flavor);

            foreach (StylePhotograph stylePhotograph in StylePhotographs)
                Add(result, stylePhotograph.Flavor);

            foreach (Wording wording in Wordings)
                Add(result, wording.Flavor);

            result.Sort(delegate(UserFlavor uf1, UserFlavor uf2) { return uf2.Weight.CompareTo(uf1.Weight); });

            List<FashionFlavor> finalResult = new List<FashionFlavor>();
            for (int i = 0; i < 2 && i < result.Count ; i++)
                finalResult.Add(result[i].Flavor);

            return finalResult;
        }

        private void Add(List<UserFlavor> result, FashionFlavor fashionFlavor)
        {
            if (result.Exists(delegate(UserFlavor record){if (record.Flavor.Id == fashionFlavor.Id){return true;}return false;}))
                result.Find(delegate(UserFlavor record){if (record.Flavor.Id == fashionFlavor.Id){return true;}return false;}).Weight++;
            else
            {
                UserFlavor uf = new UserFlavor();
                uf.Flavor = fashionFlavor;
                uf.Weight = 1;
                result.Add(uf);
            }
        }

        #endregion

     
    }
}
