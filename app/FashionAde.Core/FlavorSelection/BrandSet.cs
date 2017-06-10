using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.FlavorSelection
{
    public class BrandSet : Entity
    {
        private string description;
        private FashionFlavor flavor;
        private string imageUri;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public virtual FashionFlavor Flavor
        {
            get { return flavor; }
            set { flavor = value; }
        }

        public BrandSet() { }

        public BrandSet(string description, string imageUri, FashionFlavor flavor)
        {
            this.description = description;
            this.imageUri = imageUri;
            this.flavor = flavor;
        }
    }
}