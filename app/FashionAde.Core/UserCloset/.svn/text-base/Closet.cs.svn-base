using System;
using System.Collections.Generic;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.OutfitEngine;
using SharpArch.Core.DomainModel;
using System.Collections.ObjectModel;
using FashionAde.Core.OutfitCombination;

namespace FashionAde.Core
{
    public class Closet : Entity
    {
        public Closet(int id)
        {
            this.Id = id;
        }

        public Closet() { }

        private RegisteredUser user;
        private PrivacyLevel privacyLevel;
        private ClosetOutfit favoriteOutfit;
        private ClosetStatus status;

        public virtual RegisteredUser User
        {
            get { return user; }
            set { user = value; }
        }

        private IList<ClosetGarment> _garments = new List<ClosetGarment>();
        private IList<ClosetGarment> garments
        {
            get { return _garments; }
            set { _garments = value; }
        }

        private ReadOnlyCollection<ClosetGarment> garmentsView;
        public virtual ReadOnlyCollection<ClosetGarment> Garments
        {
            get
            {
                if (this.garmentsView == null)
                    garmentsView = new ReadOnlyCollection<ClosetGarment>(garments);
                return this.garmentsView;
            }
        }

        private IList<ClosetOutfit> _outfits = new List<ClosetOutfit>();
        private IList<ClosetOutfit> outfits
        {
            get { return _outfits; }
            set { _outfits = value; }
        }

        private ReadOnlyCollection<ClosetOutfit> outfitsView;
        public virtual ReadOnlyCollection<ClosetOutfit> Outfits
        {
            get
            {
                if (this.outfitsView == null)
                    outfitsView = new ReadOnlyCollection<ClosetOutfit>(outfits);
                return this.outfitsView;
            }
        }
        public virtual PrivacyLevel PrivacyLevel
        {
            get { return privacyLevel; }
            set { privacyLevel = value; }
        }

        public virtual ClosetOutfit FavoriteOutfit
        {
            get { return favoriteOutfit; }
            protected set { favoriteOutfit = value; }
        }

        public virtual ClosetStatus Status
        {
            get { return status; }
            protected set { status = value; }
        }

        private ClosetGarmentStatus creationStatus;

        public virtual ClosetGarmentStatus CreationStatus
        {
            get { return creationStatus; }
            protected set { creationStatus = value; }
        }

        public virtual void MarkAsProcessed()
        {
            creationStatus = ClosetGarmentStatus.Processed;
        }

        public virtual void StartProcessing()
        {
            creationStatus = ClosetGarmentStatus.Pending;
        }

        public virtual bool HasGarment(Garment g)
        {
            return (new List<ClosetGarment>(this.garments)).Exists(delegate(ClosetGarment record)
                                                                {
                                                                    if (record.Garment.Id == g.Id)
                                                                    {
                                                                        return true;
                                                                    }
                                                                    return false;
                                                                });
        }

        public virtual ClosetGarment AddGarment(Garment g)
        {
            ClosetGarment cg = new ClosetGarment(g, this);
            garments.Add(cg);
            return cg;
        }

        public virtual void RemoveGarment(ClosetGarment g)
        {
            garments.Remove(g);
        }

        public virtual void AddPrecombination(PreCombination pc)
        {
            ClosetOutfit co = new ClosetOutfit { PreCombination = pc, Closet = this };
            outfits.Add(co);
        }

        public virtual void SetFavoriteOutfit(ClosetOutfit favoriteOutfit)
        {
            this.favoriteOutfit = favoriteOutfit;
        }

        public virtual void ClearFavoriteOutfit()
        {
            this.favoriteOutfit = null;
        }
    }

    public class NotPublicClosetException : Exception
    { }
}