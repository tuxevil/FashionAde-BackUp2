using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core;
using FashionAde.Core.Clothing;

namespace FashionAde.ApplicationServices
{
    public class RegisterMemberService : IRegisterMemberService
    {
        private IBasicUserRepository basicUserRepository;
        private IWishListRepository wishListRepository;
        private IClosetRepository closetRepository;
        private IMessageSenderService messageSenderService;
        private IInvitationCodeRepository invitationCodeRepository;
        private IFriendRepository friendRepository;

        public RegisterMemberService(IBasicUserRepository basicUserRepository, IWishListRepository wishListRepository, IClosetRepository closetRepository, IUserSizeRepository userSizeRepository, ISecurityQuestionRepository securityQuestionRepository, IMessageSenderService messageSenderService, IInvitationCodeRepository invitationCodeRepository, 
            IFriendRepository friendRepository)
        {
            this.basicUserRepository = basicUserRepository;
            this.wishListRepository = wishListRepository;
            this.closetRepository = closetRepository;
            this.messageSenderService = messageSenderService;
            this.invitationCodeRepository = invitationCodeRepository;
            this.friendRepository = friendRepository;
        }

        public void RegisterMember(string email, string userName, string password, 
            UserSize userSize, int membershipUserId, string zipCode,
            IList<UserFlavor> userFlavors, IList<EventType> eventTypes,
            IList<Garment> mygarments, IList<Garment> mywishlist,
            string validateUri,
            string invitationCode)
        {
            try
            {
                IDictionary<string, object> propertyValues;
                bool invited = false;

                email = email.ToLower().Trim();
                userName = userName.ToLower().Trim();

                basicUserRepository.DbContext.BeginTransaction();

                // HACK: Added to allow to be easy to register without invitation codes on DEBUG
                if (!string.IsNullOrEmpty(invitationCode))
                {
                    propertyValues = new Dictionary<string, object>();
                    propertyValues.Add("Code", invitationCode);

                    InvitationCode ic = invitationCodeRepository.FindOne(propertyValues);
                    if (ic == null || ic.IsUsed ||
                        (ic.EmailAddress != null && ic.EmailAddress.ToLower() != email))
                        throw new InvalidInvitationCodeException();

                    ic.MarkUsed();
                    invitationCodeRepository.SaveOrUpdate(ic);
                }

                RegisteredUser user = new RegisteredUser();
                user.UserName = userName;
                user.EmailAddress = email.ToLower();
                user.Size = userSize;
                user.FirstName = string.Empty;
                user.LastName = string.Empty;
                user.PhoneNumber = string.Empty;
                user.MembershipUserId = membershipUserId;
                user.RegistrationCode = Guid.NewGuid().ToString();  // Used for email verification purposes.
                user.ChangeZipCode(zipCode);
                user.SetFlavors(userFlavors);

                if (eventTypes != null)
                    foreach (EventType eventType in eventTypes)
                        user.AddEventType(eventType);

                // Create Closet
                Closet closet = new Closet();
                closet.User = user;
                closet.PrivacyLevel = PrivacyLevel.Private;

                if (mygarments != null)
                {
                    foreach (Garment garment in mygarments)
                        closet.AddGarment(garment);
                }
                user.Closet = closet;

                // Check if the user does not exist with that mail
                propertyValues = new Dictionary<string, object>();
                propertyValues.Add("EmailAddress", email);
                BasicUser bu = basicUserRepository.FindOne(propertyValues);

                // HACK: We need to change the mail of the invited user to be able to add the new registered user.
                if (bu != null && bu is InvitedUser)
                {
                    InvitedUser iu = bu as InvitedUser;
                    iu.EmailAddressReplaced = iu.EmailAddress;
                    bu.EmailAddress += new Random().Next().ToString();

                    basicUserRepository.SaveOrUpdate(iu);

                    closetRepository.SaveOrUpdate(closet);
                    basicUserRepository.SaveOrUpdate(user);

                    propertyValues = new Dictionary<string, object>();
                    propertyValues.Add("User", bu);
                    IList<Friend> lst = friendRepository.FindAll(propertyValues);
                    if (lst.Count > 0)
                    {
                        foreach (Friend f in lst)
                        {
                            Friend newFriend = new Friend();
                            newFriend.BasicUser = user;
                            newFriend.User = f.BasicUser;
                            newFriend.Status = FriendStatus.Pending;
                            friendRepository.SaveOrUpdate(newFriend);
                        }
                    }

                    invited = true;
                }
                else
                {
                    closetRepository.SaveOrUpdate(closet);
                    basicUserRepository.SaveOrUpdate(user);
                }

                // Create wishlist even if no items been selected for further use.
                WishList wl = new WishList();
                wl.User = user;

                // Save Wish List
                if (mywishlist != null && mywishlist.Count > 0)
                {
                    foreach (Garment wishlist in mywishlist)
                        wl.AddGarment(wishlist);
                }
                wishListRepository.SaveOrUpdate(wl);

                // Send Email Confirmation Mail
                SendValidationCode(user, validateUri);

                // Commit Transaction
                basicUserRepository.DbContext.CommitTransaction();

                new FashionAde.Utils.OutfitEngineService.OutfitEngineServiceClient().CreateOutfits(closet.Id);

                if (invited)
                    basicUserRepository.MigrateInvited(bu as InvitedUser, user);

            }
            catch(Exception ex) 
            {
                try { basicUserRepository.DbContext.RollbackTransaction(); }
                catch { }

                throw ex;
            }

        }

        public void SendValidationCode(RegisteredUser user, string validateUri)
        {
            string confirmUrl = string.Format("{0}?userid={1}&code={2}", validateUri, user.MembershipUserId, user.RegistrationCode);

            RegisterData rd = new RegisterData();
            rd.ConfimationCode = user.RegistrationCode;
            rd.ConfirmUrl = confirmUrl;
            rd.UserName = user.UserName;

            messageSenderService.SendWithTemplate("confirmemail", user, rd, user.EmailAddress);
        }
    }

    public class InvalidInvitationCodeException : Exception { }
}
