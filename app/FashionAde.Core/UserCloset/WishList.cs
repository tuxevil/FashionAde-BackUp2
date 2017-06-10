using System;
using System.Collections.Generic;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class WishList : Entity
    {
        private IList<WishGarment> garments = new List<WishGarment>();
        private RegisteredUser user;

        public virtual IList<WishGarment> Garments
        {
            get { return garments; }
            protected set { garments = value;  }
        }

        public virtual RegisteredUser User
        {
            get { return user; }
            set { user = value; }
        }

        public virtual void RemoveWishGarment(WishGarment wg)
        {
            if ((new List<WishGarment>(this.garments)).Exists(delegate(WishGarment record) { if (record.Id == wg.Id) { return true; } return false; }))
                garments.Remove(wg);    
        }

        public virtual void AddGarment(Garment g)
        {
            if (!(new List<WishGarment>(this.garments)).Exists(delegate(WishGarment record) { if (record.Garment.Id == g.Id) { return true; } return false; }))
                garments.Add(new WishGarment(g));
        }
    }
}