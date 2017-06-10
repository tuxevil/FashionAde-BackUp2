using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.DomainModel;
using FashionAde.Core.Clothing;
using FashionAde.Core.Services;
using System.Reflection;

namespace FashionAde.Core.OutfitEngine
{
    public class PreCombination : Entity
    {
        private PreGarment garment1 = new PreGarment(0);
        private PreGarment garment2 = new PreGarment(0);
        private PreGarment garment3 = new PreGarment(0);
        private PreGarment garment4 = new PreGarment(0);
        private PreGarment garment5 = new PreGarment(0);

        public virtual FashionFlavor FashionFlavor { get; set; }

        public virtual PreGarment Garment1
        {
            get { return garment1; }
            protected set { garment1 = value; }
        }

        public virtual PreGarment Garment2
        {
            get { return garment1; }
            protected set { garment1 = value; }
        }

        public virtual PreGarment Garment3
        {
            get { return garment1; }
            protected set { garment1 = value; }
        }


        public virtual PreGarment Garment4
        {
            get { return garment1; }
            protected set { garment1 = value; }
        }


        public virtual PreGarment Garment5
        {
            get { return garment1; }
            protected set { garment1 = value; }
        }

        private float averageUserRating = 0;
        private float averageFriendRating = 0;
        private float averageWearRating = 0;

        private PreCombinationStatus status = PreCombinationStatus.New;

        private IList<OutfitUpdater> outfitUpdaters;

        public virtual float AverageUserRating
        {
            get { return averageUserRating; }
            protected set { averageUserRating = value; }
        }

        public virtual float AverageFriendRating
        {
            get { return averageFriendRating; }
            protected set { averageFriendRating = value; }
        }

        public virtual float AverageWearRating
        {
            get { return averageWearRating; }
            protected set { averageWearRating = value; }
        }

        public virtual PreCombinationStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual IList<OutfitUpdater> OutfitUpdaters
        {
            get { return outfitUpdaters; }
            set { outfitUpdaters = value; }
        }

        public PreCombination() { }
        public PreCombination(int id)
        {
            this.Id = id;
        }

        public virtual void AddPreGarment(Garment garment)
        {
            int pos = OutfitValidationService.GetClosetOutfitPosition(garment);

            PropertyInfo pi = new PreCombination().GetType().GetProperty(string.Format("Garment{0}", pos + 1));
            pi.SetValue(this, garment.PreGarment, null);
        }
    }

    public enum PreCombinationStatus
    {
        New = 0,
        Processed = 1
    }
}
