using FashionAde.Core.Accounts;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class UserRating : Entity
    {
        private float ratingValue;

        public virtual float RatingValue
        {
            get { return ratingValue; }
            set { ratingValue = value; }
        }

        public virtual ClosetOutfit ClosetOutfit
        {
            get; set;
        }
        
        public UserRating() {}
        public UserRating(float ratingValue)
        {
            this.ratingValue = ratingValue;
        }
    }
}