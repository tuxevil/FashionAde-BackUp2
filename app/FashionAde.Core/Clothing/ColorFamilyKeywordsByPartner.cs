using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Clothing
{
    public class ColorFamilyKeywordsByPartner : Entity
    {
        private ColorFamily colorFamily;
        private Partner partner;
        private string keywords;

        public virtual ColorFamily ColorFamily
        {
            get { return colorFamily; }
            set { colorFamily = value; }
        }

        public virtual Partner Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        public virtual string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }
    }
}
