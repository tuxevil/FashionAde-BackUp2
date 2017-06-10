using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharpArch.Core.DomainModel;
using NHibernate.Validator.Constraints;

namespace FashionAde.Core.Accounts
{
    public class BasicUser : Entity, IBasicUser
    {
        #region Properties

        private IList<UserFlavor> _userFlavors = new List<UserFlavor>();
        private IList<UserFlavor> userFlavors
        {
            get { return _userFlavors; }
            set { _userFlavors = value; }
        }

        private string emailAddress;
        [Email]
        public virtual string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        private IList<Friend> _friends = new List<Friend>();
        private IList<Friend> friends
        {
            get { return _friends; }
            set { _friends = value; }
        }

        private IList<Friend> _friendsAccepted = new List<Friend>();
        private IList<Friend> friendsAccepted
        {
            get { return _friendsAccepted; }
            set { _friendsAccepted = value; }
        }

        private IList<Friend> _friendsThatInvitedMe = new List<Friend>();
        private IList<Friend> friendsThatInvitedMe
        {
            get { return _friendsThatInvitedMe; }
            set { _friendsThatInvitedMe = value; }
        }

        private ReadOnlyCollection<Friend> friendsView;
        public virtual ReadOnlyCollection<Friend> Friends
        {
            get
            {
                if (this.friendsView == null)
                    friendsView = new ReadOnlyCollection<Friend>(friends);
                return this.friendsView;
            }
        }

        private ReadOnlyCollection<Friend> friendsAcceptedView;
        public virtual ReadOnlyCollection<Friend> FriendsAccepted
        {
            get
            {
                if (this.friendsAcceptedView == null)
                    friendsAcceptedView = new ReadOnlyCollection<Friend>(friendsAccepted);
                return this.friendsAcceptedView;
            }
        }

        private ReadOnlyCollection<Friend> friendsThatInvitedMeView;
        public virtual ReadOnlyCollection<Friend> FriendsThatInvitedMe
        {
            get
            {
                if (this.friendsThatInvitedMeView == null)
                    friendsThatInvitedMeView = new ReadOnlyCollection<Friend>(friendsThatInvitedMe);
                return this.friendsThatInvitedMeView;
            }
        }

        private ReadOnlyCollection<UserFlavor> userFlavorsView;
        public virtual ReadOnlyCollection<UserFlavor> UserFlavors
        {
            get
            {
                if (this.userFlavorsView == null)
                    userFlavorsView = new ReadOnlyCollection<UserFlavor>(userFlavors);
                return this.userFlavorsView;
            }
        }

        private IList<EventType> _eventTypes = new List<EventType>();
        private IList<EventType> eventTypes
        {
            get { return _eventTypes; }
            set { _eventTypes = value; }
        }

        private ReadOnlyCollection<EventType> eventTypesView;
        public virtual ReadOnlyCollection<EventType> EventTypes
        {
            get {
                if (this.eventTypesView == null)
                    eventTypesView = new ReadOnlyCollection<EventType>(eventTypes);
                return this.eventTypesView;
            }
        }
                
        public virtual string FirstName
        {
            get; set;
        }

        public virtual string LastName
        {
            get; set;
        }

        public virtual string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        #endregion

        /// <summary>
        /// Set the user flavors with the new defined.
        /// </summary>
        /// <param name="flavors">A collection of new user flavors</param>
        public virtual void SetFlavors(IList<UserFlavor> flavors)
        {
            if(flavors != null && flavors.Count > 0)
            {
                userFlavors.Clear();
                foreach (UserFlavor flavor in flavors)
                    userFlavors.Add(flavor);
            }
        }

        /// <summary>
        /// Add a new event type
        /// </summary>
        /// <param name="eventType">An event</param>
        public virtual void AddEventType(EventType eventType)
        {
            if (eventType != null)
                this.eventTypes.Add(eventType);
        }

        /// <summary>
        /// Remove a new event type
        /// </summary>
        /// <param name="eventType">An event</param>
        public virtual void RemoveEventType(EventType eventType)
        {
            if (eventType != null)
                this.eventTypes.Remove(eventType);
        }

        public virtual void AddFriends(IList<Friend> newFriends)
        {
            foreach (Friend friend in newFriends)
                this.AddFriend(friend);
        }

        public virtual void AddFriend(Friend friend)
        {
            if (!HasFriend(friend))
                this.friends.Add(friend);
        }

        public virtual bool HasFriend(Friend friend)
        {
            return (new List<Friend>(this.friends)).Exists(delegate(Friend record) { if (record.User.EmailAddress == friend.User.EmailAddress) { return true; } return false; });
        }

        public virtual void RemoveFriend(Friend friend)
        {
            this.friends.Remove(friend);
        }

        public virtual FashionFlavor GetPreferredFlavor()
        {
            List<UserFlavor> lst = new List<UserFlavor>(this.UserFlavors);
            lst.Sort(delegate(UserFlavor uf1, UserFlavor uf2) { return uf2.Weight.CompareTo(uf1.Weight); });
            return lst[0].Flavor;
        }
    }
}