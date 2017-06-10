using System;
using FashionAde.Core.Accounts;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core
{
    public class FriendRating : Entity
    {
        private InvitedUser friend;
        private float? ratingValue;
        private string observations = string.Empty;
        private DateTime requestedOn;
        private ClosetOutfit closetOutfit;

        public virtual InvitedUser Friend
        {
            get { return friend; }
            set { friend = value; }
        }

        public virtual float? RatingValue
        {
            get { return ratingValue; }
            set { ratingValue = value; }
        }

        public virtual string Observations
        {
            get { return observations; }
            set { observations = value; }
        }

        public virtual bool IsRated
        {
            get { return RatingValue.HasValue; }
        }

        public virtual DateTime RequestedOn
        {
            get { return requestedOn; }
            set { requestedOn = value; }
        }

        public virtual ClosetOutfit ClosetOutfit
        {
            get { return closetOutfit; }
            set { closetOutfit = value; }
        }
    }
}