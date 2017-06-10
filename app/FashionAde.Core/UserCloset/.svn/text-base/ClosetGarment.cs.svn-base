using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class ClosetGarment : Entity
    {
        private Closet closet;
        private Garment garment;
        private GarmentDetails details;

        public virtual int CategoryId
        {
            get { return garment.Tags.Category.Id; }
        }

        private ClosetGarmentStatus status = ClosetGarmentStatus.Pending;
        public virtual ClosetGarmentStatus Status 
        {
            get { return status; }
            protected set { status = value; } 
        
        }
        
        public virtual Garment Garment
        {
            get { return garment; }
            set { garment = value; }
        }

        public virtual GarmentDetails Details
        {
            get { return details; }
            set { details = value; }
        }

        public virtual Closet Closet
        {
            get { return closet; }
            set { closet = value; }
        }

        public ClosetGarment(){}

        public ClosetGarment(int id)
        {
            this.Id = id;
        }

        public ClosetGarment(Garment garment, Closet closet)
        {
            this.garment = garment;
            this.closet = closet;
        }
    }
}