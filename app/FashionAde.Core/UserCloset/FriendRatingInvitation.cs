using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.MVCInteraction;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.UserCloset
{
    public class FriendRatingInvitation : Entity
    {
        private RegisteredUser user;
        private string keyCode;
        private string friendEmail;
        private DateTime invitationSentOn;
        private DateTime friendRateOn;
        private int rate;
        private ClosetOutfit closetOutfit;
        private ClosetOutfitView outfit;
        private string message;

        public virtual RegisteredUser User
        {
            get { return user; }
            set { user = value; }
        }

        public virtual string KeyCode
        {
            get { return keyCode; }
            set { keyCode = value; }
        }

        public virtual DateTime InvitationSentOn
        {
            get { return invitationSentOn; }
            set { invitationSentOn = value; }
        }

        public virtual DateTime FriendRateOn
        {
            get { return friendRateOn; }
            set { friendRateOn = value; }
        }

        public virtual int Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public virtual string FriendEmail
        {
            get { return friendEmail; }
            set { friendEmail = value; }
        }

        public virtual ClosetOutfit ClosetOutfit
        {
            get { return closetOutfit; }
            set { closetOutfit = value; }
        }

        public virtual ClosetOutfitView Outfit
        {
            get { return outfit; }
            set { outfit = value; }
        }

        public virtual string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
