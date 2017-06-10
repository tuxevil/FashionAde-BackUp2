using System.Collections.Generic;
namespace FashionAde.Core.Clothing
{
    public class Color : Tag
    {
        private ColorFamily family;

        public virtual ColorFamily Family
        {
            get { return family; }
            set { family = value; }
        }

        public virtual bool IsColorful { get; set; }

        public virtual bool Shines { get; set; }

        private IList<Color> complimentaryColors = new List<Color>();
        public virtual IList<Color> ComplimentaryColors
        {
            get { return complimentaryColors; }
            set { complimentaryColors = value; }
        }

    }
}