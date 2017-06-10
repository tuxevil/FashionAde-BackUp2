using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FashionAde.Core.MVCInteraction;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class FriendRatingHelper
    {
        private ClosetOutfitView closetOutfit;
        private int _friendsrating;
        private string _comment;
        private string keyCode;

        public ClosetOutfitView ClosetOutfit
        {
            get { return closetOutfit; }
            set { closetOutfit = value; }
        }

        [Range(1,5, ErrorMessage = "Rating required")]
        public int friendsrating
        {
            get { return _friendsrating; }
            set { _friendsrating = value; }
        }

        [Required(ErrorMessage = "Comment required")]
        public string comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public string KeyCode
        {
            get { return keyCode; }
            set { keyCode = value; }
        }

        public FriendRatingHelper() { }

        public FriendRatingHelper(ClosetOutfitView closetOutfit, string keyCode)
        {
            this.closetOutfit = closetOutfit;
            this.keyCode = keyCode;
        }
    }
}
