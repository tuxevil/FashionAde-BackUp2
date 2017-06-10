using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class InviteFriends
    {
        private string outfitId;
        private string friendsEmails;
        private string message;
        private bool sendMe;
        private string siteURL;

        public virtual string OutfitId
        {
            get { return outfitId; }
            set { outfitId = value; }
        }

        public virtual string FriendsEmails
        {
            get { return friendsEmails; }
            set { friendsEmails = value; }
        }

        public virtual string Message
        {
            get { return message; }
            set { message = value; }
        }

        public virtual bool SendMe
        {
            get { return sendMe; }
            set { sendMe = value; }
        }

        public virtual string SiteURL
        {
            get { return siteURL; }
            set { siteURL = value; }
        }
    }
}
