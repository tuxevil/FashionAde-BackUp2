using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;

namespace FashionAde.ApplicationServices
{
    /// <summary>
    /// Manage the creation of a friend, assuring only user per email.
    /// </summary>
    public interface IFriendCreatorService
    {
        /// <summary>
        /// Creates a new friend.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="friendProvider">Provider if any</param>
        /// <returns>The new friend created with a valid basic user.</returns>
        Friend Create(string email, string firstName, string lastName, FriendProvider friendProvider);
    }
}
