using System;
using System.Collections.Generic;
using FashionAde.Core.Services;
using System.Collections.ObjectModel;
using FashionAde.Core.ThirdParties;

namespace FashionAde.Core.Accounts
{
    public class RegisteredUser : BasicUser
    {
        #region Constructors
        public RegisteredUser(int id)
        {
            this.Id = id;
        }

        public RegisteredUser() { }

        #endregion

        #region Properties

        private string phoneNumber;
        private Closet closet;
        private string zipCode;
        private UserSize size;
        private int? membershipUserId;
        private string newMail;
        
        public virtual int? MembershipUserId
        {
            get { return membershipUserId; }
            set { membershipUserId = value; }
        }

        public virtual Closet Closet
        {
            get { return closet; }
            set { closet = value; }
        }

        public virtual string ZipCode
        {
            get { return zipCode; }
            protected set { zipCode = value; }
        }

        public virtual UserSize Size
        {
            get { return size; }
            set { size = value; }
        }

        public virtual string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public virtual Partner Partner
        {
            get;
            set;
        }

        private RegisteredUserStatus _status = RegisteredUserStatus.Unverified;

        public virtual RegisteredUserStatus Status
        {
            get { return _status; }
            protected set { _status = value; }
        }

        public virtual string RegistrationCode
        {
            get;
            set;
        }

        public virtual string UserName
        {
            get;
            set;
        }

        public override string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                    return UserName;

                return base.FullName;
            }
        }

        public virtual string NewMail
        {
            get { return newMail; }
            set { newMail = value; }
        }

        #endregion

        public virtual void ChangeZipCode(string newZipCode)
        {
            this.zipCode = newZipCode;
        }

        public virtual void Confirm()
        {
            this.Status = RegisteredUserStatus.Verified;
            this.RegistrationCode = string.Empty;
        }
    }
}