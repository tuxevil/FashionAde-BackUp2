using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.ThirdParties;
using FashionAde.Core.Trends;

namespace FashionAde.OutfitUpdaterImportation.Core
{
    public class OUImportationLine
    {
        public virtual string ProgramName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string Description { get; set; }
        public virtual string Sku { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual string BuyUrl { get; set; }
        public virtual string ImageUrl { get; set; }
        private IList<Trend> trends = new List<Trend>();
        public virtual Silouhette Silouhette { get; set; }
        public virtual Pattern Pattern { get; set; }
        public virtual ColorFamily ColorFamily { get; set; }
        private OutfitUpdaterStatus status = OutfitUpdaterStatus.Incomplete;

        public virtual IList<Trend> Trends
        {
            get { return trends; }
            set { trends = value; }
        }

        public virtual OutfitUpdaterStatus Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
