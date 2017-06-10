using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;
using System;

namespace FashionAde.Core.Accounts
{
    [Serializable]
    public class UserFlavor : Entity
    {
        private FashionFlavor flavor;
        private decimal weight;

        public virtual FashionFlavor Flavor
        {
            get { return flavor; }
            set { flavor = value; }
        }

        public virtual decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public UserFlavor() { }

        public UserFlavor(FashionFlavor fashionFlavor, decimal weight)
        {
            this.flavor = fashionFlavor;
            this.weight = weight;
        }
    }
}