using System;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class GarmentDetails : Entity
    {
        private string purchasedAt;
        private DateTime? purchasedOn;
        private string madeBy;
        private string madeOf;
        private bool isTailored;
        private string careConditions;
        private string storeConditions;

        public virtual string PurchasedAt
        {
            get { return purchasedAt; }
            set { purchasedAt = value; }
        }

        public virtual DateTime? PurchasedOn
        {
            get { return purchasedOn; }
            set { purchasedOn = value; }
        }

        public virtual string MadeBy
        {
            get { return madeBy; }
            set { madeBy = value; }
        }

        public virtual string MadeOf
        {
            get { return madeOf; }
            set { madeOf = value; }
        }

        public virtual bool IsTailored
        {
            get { return isTailored; }
            set { isTailored = value; }
        }

        public virtual string CareConditions
        {
            get { return careConditions; }
            set { careConditions = value; }
        }

        public virtual string StoreConditions
        {
            get { return storeConditions; }
            set { storeConditions = value; }
        }
    }
}