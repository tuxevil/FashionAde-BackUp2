using System;
using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class WishGarment : Entity
    {
        private DateTime wantedOn;
        private Garment garment;

        public virtual Garment Garment
        {
            get { return garment; }
            set { garment = value; }
        }

        public virtual DateTime WantedOn
        {
            get { return wantedOn; }
            set { wantedOn = value; }
        }

        public WishGarment() {}

        public WishGarment(int id) 
        {
            this.Id = id;
        }

        public WishGarment(Garment g)
        {
            this.garment = g;
            this.wantedOn = DateTime.Now;
        }
    }
}