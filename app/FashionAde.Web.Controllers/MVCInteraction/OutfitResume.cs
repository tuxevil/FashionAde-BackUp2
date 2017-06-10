using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.MVCInteraction;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class OutfitResume
    {
        private ClosetOutfitView outfitView;
        private string userClosetUrl;
        private List<OutfitResumeFriendComments> comments = new List<OutfitResumeFriendComments>();
        private List<OutfitResumeFriendRating> ratings = new List<OutfitResumeFriendRating>();
        private int totalFriendRatings;
        private bool showRatings;

        public virtual ClosetOutfitView OutfitView
        {
            get { return outfitView; }
            set { outfitView = value; }
        }

        public virtual string OutfitUserName
        {
            get { return outfitView.ClosetOutfit.Closet.User.UserName; }
        }

        public virtual string UserClosetUrl
        {
            get { return userClosetUrl; }
            set { userClosetUrl = value; }
        }

        public List<OutfitResumeFriendComments> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public List<OutfitResumeFriendRating> Ratings
        {
            get { return ratings; }
            set { ratings = value; }
        }

        public int TotalFriendRatings
        {
            get { return totalFriendRatings; }
            set { totalFriendRatings = value; }
        }

        public bool ShowRatings
        {
            get { return showRatings; }
            set { showRatings = value; }
        }

        public OutfitResume(){}

        public void GetRatings()
        {
            foreach (FriendRating rating in outfitView.ClosetOutfit.Rating.FriendRatings)
            {
                AddFriendRatings(Convert.ToInt32(rating.RatingValue));
                if(!string.IsNullOrEmpty(rating.Observations))
                {
                    string friendName = "Anonymous";
                    if (rating.Friend != null)
                        friendName = rating.Friend.FullName;
                    AddFriendComment(friendName, rating.Observations);
                }
                this.ratings.Sort(delegate(OutfitResumeFriendRating p1, OutfitResumeFriendRating p2) { return p1.Rating.CompareTo(p2.Rating); });
                this.ratings.Reverse();
            }
            this.showRatings = true;
        }

        private void AddFriendRatings(int rating)
        {
            OutfitResumeFriendRating r = this.ratings.Find(e => e.Rating.Equals(rating));
            if (r != null)
                r.Count++;
            else this.ratings.Add(new OutfitResumeFriendRating(rating));
            this.totalFriendRatings++;
        }

        private void AddFriendComment(string name, string comment)
        {
            this.comments.Add(new OutfitResumeFriendComments(name, comment));
        }
    }

    public class OutfitResumeFriendComments
    {
        private string name;
        private string comment;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public OutfitResumeFriendComments(string name, string comment)
        {
            this.name = name;
            this.comment = comment;
        }
    }

    public class OutfitResumeFriendRating
    {
        private int rating;
        private int count;

        public virtual int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public virtual int Count
        {
            get { return count; }
            set { count = value; }
        }

        public OutfitResumeFriendRating(int rating)
        {
            this.rating = rating;
            this.count = 1;
        }
        public OutfitResumeFriendRating(int rating, int count)
        {
            this.rating = rating;
            this.count = count;
        }
    }
}
