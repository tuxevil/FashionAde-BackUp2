using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Accounts;

namespace FashionAde.ApplicationServices
{
    /// <summary>
    /// Generates an invitation to use the system.
    /// </summary>
    public interface IInviteContactsService
    {
        /// <summary>
        /// Invites a bulk of UserContacts
        /// </summary>
        /// <param name="invitedBy">Invited By</param>
        /// <param name="comments">Comments</param>
        /// <param name="friendProvider">Friend Provider</param>
        /// <param name="contacts">List of Contacts</param>
        void InviteUsers(BasicUser invitedBy, string comments, FriendProvider friendProvider, IList<IBasicUser> contacts, bool includeInvitationCode);

        /// <summary>
        /// Invites a UserContact
        /// </summary>
        /// <param name="invitedBy">Invited By</param>
        /// <param name="comments">Comments</param>
        /// <param name="friendProvider">Friend Provider</param>
        /// <param name="contact">Contact</param>
        void InviteUser(BasicUser invitedBy, string comments, FriendProvider friendProvider, IBasicUser contact, bool includeInvitationCode);
    }
}
