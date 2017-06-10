using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;

namespace FashionAde.ApplicationServices
{
    public class FriendCreatorService : IFriendCreatorService
    {
        private IBasicUserRepository basicUserRepository;
        private IFriendProviderRepository friendProviderRepository;

        public FriendCreatorService(IBasicUserRepository registeredUserRepository, IFriendProviderRepository friendProviderRepository)
        {
            this.basicUserRepository = registeredUserRepository;
            this.friendProviderRepository = friendProviderRepository;
        }

        public Friend Create(string email, string firstName, string lastName, FriendProvider friendProvider)
        {
            // Validate if the user exists.
            IDictionary<string, object> propertyValues = new Dictionary<string, object>();
            propertyValues.Add("EmailAddress", email);
            BasicUser bu = basicUserRepository.FindOne(propertyValues);

            Friend f = new Friend();
            if (bu != null)
                f.User = bu;
            else
            {
                // If not, create as an invitation
                InvitedUser invited = new InvitedUser();
                invited.EmailAddress = email;
                invited.FirstName = firstName;
                invited.LastName = lastName;

                f.User = invited;
            }

            // Set Provider
            f.FriendProvider = friendProvider;
            return f;
        }

    }
}
