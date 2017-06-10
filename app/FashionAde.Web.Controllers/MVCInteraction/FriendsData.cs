using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class FriendsData
    {
        private IList<Friend> friends;
        private bool list;
        private string userMail;
        private int newFriendsRequest;

        public virtual IList<Friend> Friends
        {
            get { return friends; }
            set { friends = value; }
        }

        public virtual bool List
        {
            get { return list; }
            set { list = value; }
        }

        public virtual int NewFriendsRequest
        {
            get { return newFriendsRequest; }
            set { newFriendsRequest = value; }
        }

        public string UserMail
        {
            get { return userMail; }
            set { userMail = value; }
        }
    }
}
