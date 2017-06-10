using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;

namespace FashionAde.ApplicationServices
{
    public class UserContact : IBasicUser
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
    }
}
