using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.Trends;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.ThirdParties
{
    public class OutfitUpdater : Entity
    {
        private Partner partner;
        
        private string name;
        private string keywords;
        private string description;
        private string externalId;
        private decimal? price;
        private string buyUrl;
        private string imageUrl;
        private IList<Trend> trends = new List<Trend>();
        private Silouhette silouhette;
        private Pattern pattern;
        private ColorFamily colorFamily;
        private OutfitUpdaterStatus status = OutfitUpdaterStatus.Incomplete;
        private IList<PreCombination> preCombinations = new List<PreCombination>();

        public virtual Partner Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual string ExternalId
        {
            get { return externalId; }
            set { externalId = value; }
        }

        public virtual decimal? Price
        {
            get { return price; }
            set { price = value; }
        }

        public virtual string BuyUrl
        {
            get { return buyUrl; }
            set { buyUrl = value; }
        }

        public virtual string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public virtual IList<Trend> Trends
        {
            get { return trends; }
            set { trends = value; }
        }

        public virtual Silouhette Silouhette
        {
            get { return silouhette; }
            set { silouhette = value; }
        }

        public virtual Pattern Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public virtual ColorFamily ColorFamily
        {
            get { return colorFamily; }
            set { colorFamily = value; }
        }

        public virtual OutfitUpdaterStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual IList<PreCombination> PreCombinations
        {
            get { return preCombinations; }
            set { preCombinations = value; }
        }

        public OutfitUpdater(){ }
        public OutfitUpdater(int id, string name, string keywords, string description, string externalId, double price, string buyUrl, string imageUrl, int partnerId, int silouhetteId, int patternId, int colorFamilyId, int status)
        {
            this.Id = id;
            this.name = name;
            this.description = description;
            this.keywords = keywords;
            this.externalId = externalId;
            if(price > 0)
            this.price = Convert.ToDecimal(price);
            this.buyUrl = buyUrl;
            this.imageUrl = imageUrl;
            this.partner = new Partner(partnerId);
            this.silouhette = new Silouhette(silouhetteId);
            this.pattern = new Pattern(patternId);
            this.colorFamily = new ColorFamily(colorFamilyId);
            this.status = (OutfitUpdaterStatus) status;
        }

        public OutfitUpdater(int Id, Silouhette s, ColorFamily c, Pattern p) 
        {
            this.silouhette = s;
            this.colorFamily = c;
            this.pattern = p;
        }

    }


    public enum OutfitUpdaterStatus
    {
        Incomplete = 0,
        Valid = 1,
        Processed = 2
    }
}
