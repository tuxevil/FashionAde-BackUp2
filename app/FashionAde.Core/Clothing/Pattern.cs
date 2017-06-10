using System.Collections.Generic;

namespace FashionAde.Core.Clothing
{
    public enum PatternType
    {
        Solid = 1,
        Minimal = 2,
        Bold = 3
    }

    public class Pattern : Tag
    {
        private string imageUri;
        //private IList<PatternKeywordsByPartner> keywords;

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public virtual PatternType Type { get; set; }

        //public virtual IList<PatternKeywordsByPartner> Keywords
        //{
        //    get { return keywords; }
        //    set { keywords = value; }
        //}

        public Pattern() {}

        public Pattern(int id)
        {
            this.Id = id;
        }
    }
}