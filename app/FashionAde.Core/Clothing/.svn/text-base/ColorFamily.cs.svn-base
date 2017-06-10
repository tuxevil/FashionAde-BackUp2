using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Clothing
{
    public class ColorFamily : Entity
    {
        private string description;
        //private IList<ColorFamilyKeywordsByPartner> keywords;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual ColorFamily AnalogousFamily { get; set; }

        public virtual ColorFamily AnalogousFamily2 { get; set; }

        public virtual ColorFamily ComplimentaryFamily { get; set; }

        public virtual bool IsPrimary { get; set; }

        public virtual bool IsSecondary { get; set; }

        public virtual bool IsNeutral { get; set; }

        private IList<Color> colors = new List<Color>();
        public virtual IList<Color> Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        //public virtual IList<ColorFamilyKeywordsByPartner> Keywords
        //{
        //    get { return keywords; }
        //    set { keywords = value; }
        //}

        public ColorFamily() {}

        public ColorFamily(int id)
        {
            this.Id = id;
        }

    }
}
