using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Accounts;
using Microsoft.Practices.ServiceLocation;

namespace FashionAde.ApplicationServices
{
    /// <summary>
    /// Provides an abstraction layer to invite users through messaging service.
    /// </summary>
    public class InviteContactsService : IInviteContactsService
    {
        private const int MAX_TO_INVITE = 5;

        private IBasicUserRepository basicUserRepository;
        private IFriendProviderRepository friendProviderRepository;
        private IFriendCreatorService friendCreatorService;
        private IMessageSenderService messageSenderService;
        private IInvitationCodeRepository invitationCodeRepository;

        public InviteContactsService(IInvitationCodeRepository invitationCodeRepository, IBasicUserRepository registeredUserRepository, IFriendProviderRepository friendProviderRepository, IFriendCreatorService friendCreatorService, IMessageSenderService messageSenderService)
        {
            this.basicUserRepository = registeredUserRepository;
            this.friendProviderRepository = friendProviderRepository;
            this.friendCreatorService = friendCreatorService;
            this.messageSenderService = messageSenderService;
            this.invitationCodeRepository = invitationCodeRepository;
        }

        public void InviteUsers(BasicUser invitedBy, string comments, FriendProvider friendProvider, IList<IBasicUser> contacts, bool includeInvitationCode)
        {
            // Make sure we get the complete user.
            BasicUser user = basicUserRepository.Get(invitedBy.Id);

            // If we are including the invitation code is because we are in Beta, no more than 10 invites per user.
#if !DEBUG
            if (includeInvitationCode)
            {
                if (user.Friends.Count + contacts.Count >= MAX_TO_INVITE)
                {
                    if (MAX_TO_INVITE - user.Friends.Count > 0)
                        throw new LimitOfFriendsExceededException(string.Format("You can add up to {0} more friends in the Beta period.", MAX_TO_INVITE - user.Friends.Count));
                    else
                        throw new LimitOfFriendsExceededException(string.Format("You have reached the {0} contacts limit in the Beta period.", MAX_TO_INVITE));
                }
            }
#endif

            basicUserRepository.DbContext.BeginTransaction();
            foreach (IBasicUser contact in contacts)
            {
                if (contact.EmailAddress.Trim() == string.Empty || user.EmailAddress.Trim().ToLower() == contact.EmailAddress.Trim().ToLower())
                    continue;

                // Call the Friend Creator service
                Friend f = friendCreatorService.Create(contact.EmailAddress.Trim().ToLower(), contact.FirstName, contact.LastName, friendProvider);

                // Make sure it does not have it as a friend to send the email.
                if (!user.HasFriend(f))
                {
                    // Save the Friend into the user collection
                    f.BasicUser = user;
                    user.AddFriend(f);
                }

                // TODO: Review how to get the Url from the controller
                string confirmUrl = string.Format("/Friend/InvitedMe/default.aspx");
                InviteData inviteData = new InviteData { Friend = f, AcceptUrl= confirmUrl, InvitationCode = Guid.NewGuid().ToString() };

                if (f.User is InvitedUser)
                {
                    if (includeInvitationCode)
                    {
                        // Create an invitation code
                        InvitationCode ic = new InvitationCode();
                        ic.EmailAddress = f.User.EmailAddress;
                        ic.Code = inviteData.InvitationCode;
                        ic.InvitedBy = f.BasicUser;
                        invitationCodeRepository.SaveOrUpdate(ic);

                        messageSenderService.SendWithTemplate("invitation_with_code", user, inviteData, f.User.EmailAddress);
                    }
                    else
                        messageSenderService.SendWithTemplate("invitation", user, f, f.User.EmailAddress);
                }
                else
                    messageSenderService.SendWithTemplate("acceptinvite", user, inviteData, f.User.EmailAddress);
            }

            basicUserRepository.SaveOrUpdate(user);
            basicUserRepository.DbContext.CommitTransaction();
        }

        private class InviteData
        {
            public Friend Friend { get; set; }
            public string AcceptUrl { get; set; }
            public string InvitationCode { get; set; }
        }

        public void InviteUser(BasicUser invitedBy, string comments, FriendProvider friendProvider, IBasicUser contact, bool includeInvitationCode)
        {
            List<IBasicUser> lst = new List<IBasicUser>(1);
            lst.Add(contact);
            InviteUsers(invitedBy, comments, friendProvider, lst, includeInvitationCode);
        }
    }

    public class LimitOfFriendsExceededException : Exception
    {
        public LimitOfFriendsExceededException()
        {
        }

        public LimitOfFriendsExceededException(string message)
            : base(message)
        {
        }

        public LimitOfFriendsExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
