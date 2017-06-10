using System;
using System.Collections.Generic;
using FashionAde.Core.Accounts;
using FashionAde.Core.UserCloset;
using SharpArch.Core.DomainModel;
using System.Collections.ObjectModel;
using FashionAde.Core.Clothing;
using FashionAde.Core.Services;

namespace FashionAde.Core
{
    public class Rating
    {
        private float myRating;
        private float friendRatingAverage;
        private float editorRating;

        public virtual float EditorRating
        {
            get { return editorRating; }
            protected set { editorRating = value; }
        }

        private IList<FriendRating> _friendRatings = new List<FriendRating>();
        private IList<FriendRating> friendRatings
        {
            get { return _friendRatings; }
            set { _friendRatings = value; }
        }

        private ReadOnlyCollection<FriendRating> friendRatingsView;
        public virtual ReadOnlyCollection<FriendRating> FriendRatings
        {
            get
            {
                if (this.friendRatingsView == null)
                    friendRatingsView = new ReadOnlyCollection<FriendRating>(friendRatings);
                return this.friendRatingsView;
            }
        }

        public virtual float MyRating
        {
            get { return myRating; }
            protected set { myRating = value; }
        }

        public virtual float FriendRatingAverage
        {
            get { return friendRatingAverage; }
            protected set { friendRatingAverage = value; }
        }

        private IList<UserRating> _userRatings = new List<UserRating>();
        private IList<UserRating> userRatings
        {
            get { return _userRatings; }
            set { _userRatings = value; }
        }

        private ReadOnlyCollection<UserRating> userRatingsView;
        public virtual ReadOnlyCollection<UserRating> UserRatings
        {
            get
            {
                if (this.userRatingsView == null)
                    userRatingsView = new ReadOnlyCollection<UserRating>(userRatings);
                return this.userRatingsView;
            }
        }

        public virtual void Rate(ClosetOutfit outfit, float ratingValue)
        {
            this.myRating = ratingValue;
            UserRating ur = new UserRating();
            ur.RatingValue = ratingValue;
            ur.ClosetOutfit = outfit;
            this.userRatings.Add(ur);
        }

        public virtual void Rate(ClosetOutfit outfit, float ratingValue, InvitedUser f)
        {
            FriendRating fr = new FriendRating();
            fr.Friend = f;
            fr.RatingValue = ratingValue;
            fr.ClosetOutfit = outfit;
            this.friendRatings.Add(fr);
            float average = 0;
            foreach (FriendRating r in this.friendRatings)
                average += (float)r.RatingValue;
            average = average/this.friendRatings.Count;
            this.FriendRatingAverage = average;
        }

        public virtual void Rate(float ratingValue, FriendRatingInvitation f)
        {
            FriendRating rating = new FriendRating();
            rating.RatingValue = f.Rate;
            rating.RequestedOn = f.InvitationSentOn;
            rating.ClosetOutfit = f.ClosetOutfit;
            rating.Observations = f.Message;
            this.friendRatings.Add(rating);
            float average = 0;
            foreach (FriendRating r in this.friendRatings)
                average += (float)r.RatingValue;
            average = average / this.friendRatings.Count;
            this.FriendRatingAverage = average;
        }


        public void CalculateEditorRating(IEnumerable<Garment> garments)
        {
            if (this.EditorRating > 0)
                throw new Exception("Can not change current rating");

            this.EditorRating = EditorRatingCalculatorService.CalculateRating(garments);
        }
    }
}