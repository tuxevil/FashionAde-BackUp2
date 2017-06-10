using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Accounts
{
    public class Friend : Entity
    {
        public virtual FriendProvider FriendProvider
        {
            get; set;
        }

        public virtual BasicUser BasicUser
        {
            get; set;
        }

        public virtual BasicUser User
        {
            get; set;
        }

        private FriendStatus _status = FriendStatus.Pending;
        public virtual FriendStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

    public enum FriendStatus
    {
        All = 0,
        Pending = 1,
        Accepted = 2,
        Denied = 3,
        
    }
}
