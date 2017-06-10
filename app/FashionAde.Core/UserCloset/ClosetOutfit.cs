using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.OutfitEngine;
using SharpArch.Core.DomainModel;
using FashionAde.Core.Services;
using System.Reflection;

namespace FashionAde.Core
{
    public class ClosetOutfit : Entity, ICloneable
    {
        private Closet closet;
        private PreCombination precombination;
        private Garment garment1;
        private Garment garment2;
        private Garment garment3;
        private Garment garment4;
        private Garment garment5;
        private Garment garment6;
        private Garment garment7;
        private Garment garment8;
        private Garment garment9;
        private Garment garment10;
        private Garment garment11;
        private Garment garment12;
        private Garment garment13;
        private Rating rating = new Rating();
        private bool isFavouriteOutfit;
        private IList<OutfitDetails> details;
        private ClosetOutfitStatus status = ClosetOutfitStatus.Ready;
        private ClosetOutfitVisibility visibility = ClosetOutfitVisibility.Public;
        private int seasons;
        private int eventTypes;
        private RegisteredUser user;

        public virtual ClosetOutfit CreatedFrom
        {
            get; set;
        }

        public virtual RegisteredUser User
        {
            get { return user; }
            set { user = value; }
        }

        public virtual MaxOutfitDetail Detail { get; set; }

        public virtual FashionFlavor FashionFlavor { get; set; }

        public virtual PreCombination PreCombination
        {
            get { return precombination; }
            set { precombination = value; }
        }

        public virtual Closet Closet
        {
            get { return closet; }
            set { closet = value; }
        }

        public virtual IList<OutfitDetails> Details
        {
            get { return details; }
            protected set { details = value; }
        }

        public virtual bool IsFavouriteOutfit
        {
            get { return isFavouriteOutfit; }
            set { isFavouriteOutfit = value; }
        }

        public virtual Rating Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public virtual Garment Garment1
        {
            protected get { return garment1; }
            set { garment1 = value; }
        }

        public virtual Garment Garment2
        {
            protected get { return garment2; }
            set { garment2 = value; }
        }

        public virtual Garment Garment3
        {
            get { return garment3; }
            set { garment3 = value; }
        }

        public virtual Garment Garment4
        {
            protected get { return garment4; }
            set { garment4 = value; }
        }

        public virtual Garment Garment5
        {
            protected get { return garment5; }
            set { garment5 = value; }
        }

        public virtual Garment Garment6
        {
            protected get { return garment6; }
            set { garment6 = value; }
        }

        public virtual Garment Garment7
        {
            protected get { return garment7; }
            set { garment7 = value; }
        }

        public virtual Garment Garment8
        {
            protected get { return garment8; }
            set { garment8 = value; }
        }

        public virtual Garment Garment9
        {
            protected get { return garment9; }
            set { garment9 = value; }
        }

        public virtual Garment Garment10
        {
            protected get { return garment10; }
            set { garment10 = value; }
        }

        public virtual Garment Garment11
        {
            protected get { return garment11; }
            set { garment11 = value; }
        }

        public virtual Garment Garment12
        {
            protected get { return garment12; }
            set { garment12 = value; }
        }

        public virtual Garment Garment13
        {
            protected get { return garment13; }
            set { garment13 = value; }
        }

        public virtual ClosetOutfitStatus Status
        {
            get { return status; }
            protected set { status = value; }

        }

        public virtual ClosetOutfitVisibility Visibility
        {
            get { return visibility; }
            protected set { visibility = value; }
        }

        public virtual void SetVisibility(ClosetOutfitVisibility visibility)
        {
            if (visibility == ClosetOutfitVisibility.Public)
            {
                bool isPublic = true;
                foreach(Garment g in this.Components)
                    if (g is UserGarment)
                    {
                        UserGarment ug = (g as UserGarment);
                        if (!(ug.ApprovalStatus == ApprovalStatus.Approved && ug.Visibility == GarmentVisibility.Public))
                            isPublic = false;
                    }

                if (!isPublic)
                    this.visibility = ClosetOutfitVisibility.Private;
            }

            this.visibility = visibility;
        }

        public virtual void Notate(string location, DateTime wearedOn)
        {
            OutfitDetails detail = new OutfitDetails();
            detail.Location = location;
            detail.WornDate = wearedOn;
            this.Details.Add(detail);
        }

        public virtual IList<Garment> Components
        {
            get { 
                
                List<Garment> components = new List<Garment>();
                if (this.garment1 != null && this.garment1.Id != 0)
                    components.Add(this.garment1);
                if (this.garment2 != null && this.garment2.Id != 0)
                    components.Add(this.garment2);
                if (this.garment3 != null && this.garment3.Id != 0)
                    components.Add(this.garment3);
                if (this.garment4 != null && this.garment4.Id != 0)
                    components.Add(this.garment4);
                if (this.garment5 != null && this.garment5.Id != 0)
                    components.Add(this.garment5);
                if (this.garment6 != null && this.garment6.Id != 0)
                    components.Add(this.garment6);
                if (this.garment7 != null && this.garment7.Id != 0)
                    components.Add(this.garment7);
                if (this.garment8 != null && this.garment8.Id != 0)
                    components.Add(this.garment8);
                if (this.garment9 != null && this.garment9.Id != 0)
                    components.Add(this.garment9);
                if (this.garment10 != null && this.garment10.Id != 0)
                    components.Add(this.garment10);
                if (this.garment11 != null && this.garment11.Id != 0)
                    components.Add(this.garment11);
                if (this.garment12 != null && this.garment12.Id != 0)
                    components.Add(this.garment12);
                if (this.garment13 != null && this.garment13.Id != 0)
                    components.Add(this.garment13);
                return components;
            }
        }

        public virtual IEnumerable<Garment> RetrieveCombinableComponents()
        {
            foreach(Garment g in this.Components)
                if (!OutfitValidationService.IsAccessory(g))
                    yield return g;
        }

        public virtual int Seasons
        {
            get { return seasons; }
            set { seasons = value; }
        }

        public virtual int EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }
        
        public virtual void SetSeason(Season season)
        {
            if (season != Season.All)
                this.seasons = (int) season;
            else
            {
                int tmp = (int) Season.Fall + (int) Season.Spring + (int) Season.Summer + (int) Season.Winter;
                this.seasons = tmp;
            }
        }

        public virtual void SetEventTypes(RegisteredUser user)
        {
            int total = 0;
            foreach (EventType et in user.EventTypes)
                total += et.BinaryNumber;

            this.eventTypes = total;
        }

        public virtual void AddComponent(Garment garment, Garment hack)
        {
            int pos = OutfitValidationService.GetClosetOutfitPosition(garment);

            PropertyInfo pi = this.GetType().GetProperty(string.Format("Garment{0}", pos + 1));
            pi.SetValue(this, garment, null);

            for (int i = 0; i <= 12; i++)
            {
                pi = this.GetType().GetProperty(string.Format("Garment{0}", i + 1));
                if (pi.GetValue(this, null) == null)
                    pi.SetValue(this, hack, null);
            }
        }

        public virtual void SendToColdStorage()
        {
            this.status = ClosetOutfitStatus.ColdStorage;
        }

        public ClosetOutfit() { }
        public ClosetOutfit(int id)
        {
            this.Id = id;
        }

        public override bool IsValid()
        {
            return OutfitValidationService.IsValidCombination(this.Components);
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            ClosetOutfit copy = new ClosetOutfit();
            copy.Garment1 = this.Garment1;
            copy.Garment2 = this.Garment2;
            copy.Garment3 = this.Garment3;
            copy.Garment4 = this.Garment4;
            copy.Garment5 = this.Garment5;
            copy.Garment6 = this.Garment6;
            copy.Garment7 = this.Garment7;
            copy.Garment8 = this.Garment8;
            copy.Garment9 = this.Garment9;
            copy.Garment10 = this.Garment10;
            copy.Garment11 = this.Garment11;
            copy.Garment12 = this.Garment12;
            copy.Garment13 = this.Garment13;
            copy.PreCombination = this.PreCombination;
            copy.Status = this.Status;
            copy.Seasons = this.Seasons;
            copy.Visibility = this.Visibility;
            return copy;
        }

        #endregion
    }
}