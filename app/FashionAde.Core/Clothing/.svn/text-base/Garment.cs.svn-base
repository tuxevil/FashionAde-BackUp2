using System.Collections.Generic;
using SharpArch.Core.DomainModel;
using FashionAde.Core.OutfitEngine;

namespace FashionAde.Core.Clothing
{
    public abstract class Garment : Entity
    {
        public virtual IList<ClosetGarment> ClosetGarments { get; set; }

        private string imageUri;
        private string linkUri;
        private GarmentStatus status;
        private GarmentTags tags = new GarmentTags();
        private string title;
        private PreGarment pregarment;

        public virtual int SeasonCode
        {
            get;
            set;
        }

        public virtual int EventCode
        {
            get;
            set;
        }

        public virtual void UpdateSeasonCode()
        {
            int total = 0;
            foreach (Season s in this.Tags.Seasons)
                total += (int)s;
            this.SeasonCode = total;
        }

        public virtual void UpdateEventTypeCode()
        {
            int total = 0;
            foreach (EventType et in this.Tags.EventTypes)
                total += et.BinaryNumber;
            this.EventCode = total;
        }

        public virtual string Title
        {
            get { return title; }
            set { title = value; }
        }

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public virtual GarmentStatus Status
        {
            get { return status; }
            protected set { status = value; }
        }

        public virtual GarmentTags Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        public virtual string LinkUri
        {
            get { return linkUri; }
            set { linkUri = value; }
        }

        public virtual PreGarment PreGarment
        {
            get { return pregarment; }
            set { pregarment = value; }
        }
    }
}