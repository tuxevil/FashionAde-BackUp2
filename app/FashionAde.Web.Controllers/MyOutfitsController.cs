using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Accounts;
using FashionAde.Core;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using FashionAde.Utils;
using FashionAde.Web.Controllers.MVCInteraction;
using SharpArch.Web.NHibernate;
using FashionAde.Core.UserCloset;
using FashionAde.Core.Clothing;
using FashionAde.Core.MVCInteraction;
using FashionAde.Web.Common;
using FashionAde.ApplicationServices;
using System.Globalization;
using System.Web.Security;
using FashionAde.Web.Extensions;
using FashionAde.Core.Services;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class MyOutfitsController : BaseController
    {
        private IFriendRepository friendRepository;
        private IClosetOutfitRepository closetOutfitRepository;
        private IClosetRepository closetRepository;
        private IFriendRatingInvitationRepository friendRatingInvitationRepository;
        private IRegisteredUserRepository registeredUserRepository;
        private IContentService contentService;
        private IMessageSenderService messageSenderService;
        private IOutfitUpdaterRepository outfitUpdaterRepository;
        
        public MyOutfitsController(IFriendRepository friendRepository, IClosetOutfitRepository closetOutfitRepository, IFriendRatingInvitationRepository friendRatingInvitationRepository, IRegisteredUserRepository registeredUserRepository, IContentService contentService, IClosetRepository closetRepository, IMessageSenderService messageSenderService, IOutfitUpdaterRepository outfitUpdaterRepository)
        {
            this.friendRepository = friendRepository;
            this.friendRatingInvitationRepository = friendRatingInvitationRepository;
            this.closetOutfitRepository = closetOutfitRepository;
            this.registeredUserRepository = registeredUserRepository;
            this.closetRepository = closetRepository;
            this.messageSenderService = messageSenderService;
            this.contentService = contentService;
            this.outfitUpdaterRepository = outfitUpdaterRepository;
        }

        [Authorize]
        public ActionResult GetFriends(string q, int limit)
        {
            RegisteredUser user = this.ProxyLoggedUser;
            IList<Friend> lst = friendRepository.Search(user, q, FriendStatus.Accepted, limit);
            string[] tmp = new string[lst.Count];
            int i = 0;
            foreach (Friend friend in lst)
            {
                tmp[i] = friend.User.FirstName + " " + friend.User.LastName + "|" + friend.User.EmailAddress;
                i++;
            }

            JsonResult jr = Json(string.Join("\n", tmp));
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        private bool IsSameUser(int userId)
        {
            MembershipUser mu = Membership.GetUser();
            RegisteredUser user;
            if (mu != null)
            {
                user = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
                if (user != null)
                    return user.Id == userId;
            }
            return false;
        }

        public ActionResult OutfitResume(int outfitId)
        {
            ClosetOutfitView closetOutfitView = closetOutfitRepository.GetByClosetOutfitId(outfitId);
            OutfitResume or = new OutfitResume();
            or.OutfitView = closetOutfitView;
            if(UserId == closetOutfitView.ClosetOutfit.Closet.User.Id)
                or.GetRatings();

            if (or.OutfitView == null)
                return RedirectToAction("Index", "Home");

            BasicUser user = null;
            if (User.Identity.IsAuthenticated)
                user = registeredUserRepository.Get(this.UserId);

            if (!AccessClosetService.CanViewClosetOutfit(user, or.OutfitView.ClosetOutfit))
                throw new NotPublicClosetException();

            or.OutfitView.Disabled = (this.ProxyCloset == null || or.OutfitView.ClosetOutfit.Closet.Id != this.ProxyCloset.Id);

            or.UserClosetUrl = (or.OutfitView.Disabled)
                                        ? Url.Action("PublicCloset", "MyOutfits", new { username = or.OutfitView.ClosetOutfit.Closet.User.UserName })
                                        : Url.Action("Index", "MyOutfits");

            bool isFavoriteOutfit = (closetOutfitView.ClosetOutfit.Closet.FavoriteOutfit != null 
                                    && closetOutfitView.ClosetOutfit.Closet.FavoriteOutfit.Id == outfitId) 
                                        ? true 
                                        : false;

            or.OutfitView.ClosetOutfit.IsFavouriteOutfit = isFavoriteOutfit;
            
            if(user!= null)
            {
                or.OutfitView.Disabled = true;
                or.OutfitView.ShowAddToMyCloset = !IsSameUser(closetOutfitView.ClosetOutfit.Closet.User.Id);
            }
            return View(or);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult Index()
        {
            string search = "Type a color, name or function to filter the results";
            return BuildOutfitView("1", ref search, "1", SeasonHelper.CurrentSeasonId, null);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Index(string SortBy, string Search, string Page, string Season)
        {
            return BuildOutfitView(SortBy, ref Search, 1.ToString(), Season, null);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult ChangePage()
        {
            string search = "Type a color, name or function to filter the results";
            return BuildOutfitView("1", ref search, "1", SeasonHelper.CurrentSeasonId, null);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult ChangePage(string SortBy, string Search, string Page, string Season)
        {
            return BuildOutfitView(SortBy, ref Search, Page, Season, null);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PublicCloset(string userName)
        {
            if (!IsPublicCloset(userName))
                throw new NotPublicClosetException();

            int? userId = GetUserId(userName);
            if (IsUserCloset(userId))
                return RedirectToAction("Index");

            string search = "Type a color, name or function to filter the results";
            return BuildOutfitView("1", ref search, "1", SeasonHelper.CurrentSeasonId, userId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PublicCloset(string SortBy, string Search, string Page, string Season, string userName)
        {
            if (!IsPublicCloset(userName))
                throw new NotPublicClosetException();
 
            int? userId = GetUserId(userName);
            return BuildOutfitView(SortBy, ref Search, Page, Season, userId.Value);
        }

        private bool ShowAsPublicCloset(int? userId)
        {
            if (userId.HasValue)
            {
                RegisteredUser user = registeredUserRepository.Get(userId.Value);
                MembershipUser mu = Membership.GetUser();
                if (mu != null && Convert.ToInt32(mu.ProviderUserKey) == user.MembershipUserId)
                    return false;

                return true;
            }
            return false;
        }


        private ActionResult BuildOutfitView(string SortBy, ref string Search, string Page, string Season, int? userId)
        {
            int totalCount;
            string originalSearch = Search;

            bool showAsPublicCloset = ShowAsPublicCloset(userId);
            RegisteredUser user = (showAsPublicCloset)
                                ? registeredUserRepository.Get(userId.Value)
                                : registeredUserRepository.Get(this.ProxyLoggedUser.Id);

            BasicUser currentUser = null;
            if (User.Identity.IsAuthenticated)
                currentUser = registeredUserRepository.Get(this.UserId);

            if (!AccessClosetService.CanViewCloset(currentUser, user.Closet))
                throw new NotPublicClosetException();

            if (originalSearch == "Type a color, name or function to filter the results")
                Search = string.Empty;

            GetOutfitsInfo();

            IList<ClosetOutfitView> lstResults = new List<ClosetOutfitView>();
            lstResults = this.Search(out totalCount, SortBy, Search, Page, Season, user);
            string userName = Membership.GetUser(user.MembershipUserId).UserName;

            OutfitView ov = new OutfitView();
            if (showAsPublicCloset)
            {
                ov.StyleAlerts = contentService.GetRandomStyleAlerts();
                foreach (ClosetOutfitView cov in lstResults)
                {
                    cov.Disabled = true;
                    cov.ShowAddToMyCloset = true;
                }
                userName += "'s Outfits";
            }
            else
                ov.StyleAlerts = contentService.GetRandomStyleAlerts((IList<FashionFlavor>)ViewData["fashionFlavors"]);

            ov.FilterText = originalSearch;
            ov.Closet = user.Closet;
            ov.UserName = userName;
            ov.Outfits = lstResults;
            ov.TotalOutfits = totalCount;
            ov.Season = Season;
            ov.PrivacyLevel = user.Closet.PrivacyLevel.ToString();
            ov.CurrentPage = Page;
            ov.ShowAsPublicCloset = showAsPublicCloset;
            ov.FavoriteOutfit = (user.Closet.FavoriteOutfit == null)
                                ? "None Selected"
                                : user.Closet.FavoriteOutfit.Id.ToString();

            ViewData["userId"] = user.Id;
            ViewData["closetUserId"] = user.MembershipUserId;
            ViewData["Pages"] = Common.Paging(totalCount, Convert.ToInt32(Page), 10, 6);

            foreach (ClosetOutfitView cov in lstResults)
                if (cov.OutfitUpdater == null)
                    cov.OutfitUpdater = outfitUpdaterRepository.Get(ConfigurationManager.AppSettings["DefaultOU"]);

            return View("Index", ov);
        }

        [ObjectFilter(Param = "outfitSelected", RootType = typeof(int))]
        [Transaction]
        [Authorize]
        public ActionResult RemoveOutfitFromCloset(int outfitSelected)
        {
            ClosetOutfit outfit = closetOutfitRepository.Get(outfitSelected);
            outfit.SendToColdStorage();
            closetOutfitRepository.SaveOrUpdate(outfit);

            if (outfit.Closet.FavoriteOutfit == outfit)
                outfit.Closet.ClearFavoriteOutfit();
            closetRepository.SaveOrUpdate(outfit.Closet);

            return Json(new { Success = true });
        }

        [ObjectFilter(Param = "outfitNotate", RootType = typeof(OutfitNotate))]
        [Transaction]
        [Authorize]
        public ActionResult AddNotateToCloset(OutfitNotate outfitNotate)
        {
            ClosetOutfit outfit = closetOutfitRepository.Get(outfitNotate.OutfitSelected);
            if (StringHelper.IsDateTime(outfitNotate.WornDate))
            {
                IFormatProvider formatProvider = new CultureInfo("en-US");
                outfit.Notate(outfitNotate.Location, Convert.ToDateTime(outfitNotate.WornDate, formatProvider));
                closetOutfitRepository.SaveOrUpdate(outfit);
            }

            return Json(new { Success = true });
        }

        [ObjectFilter(Param = "inviteFriends", RootType = typeof(InviteFriends))]
        [Authorize]
        public JsonResult InviteFriends(InviteFriends inviteFriends)
        {
            string[] contacts = inviteFriends.FriendsEmails.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            RegisteredUser user = registeredUserRepository.Get(this.UserId);

            ClosetOutfit co = closetOutfitRepository.Get(Convert.ToInt32(inviteFriends.OutfitId));

            foreach (string contact in contacts)
            {
                if (string.IsNullOrEmpty(contact) || contact == " ")
                    continue;

                string email = contact;
                if (contact.IndexOf("[") != -1 && contact.IndexOf("]") != -1)
                    email = contact.Split('[')[1].Split(']')[0];

                string key = Guid.NewGuid().ToString();


                EmailData data = new EmailData
                {
                    Components = co.Components,
                    KeyCode = key.ToString(),
                    Comments = inviteFriends.Message,
                    InvitationLink = "/outfit/rating/index/" + key + "/default.aspx",
                    GarmentsUri = Resources.GetGarmentLargePath(),
                    OutfitUpdater = outfitUpdaterRepository.GetRandomOutfitUpdaterFor(co.PreCombination)
                };

                messageSenderService.SendWithTemplate("invite2rate", user, data, email);

                FriendRatingInvitation invitation = new FriendRatingInvitation();
                invitation.FriendEmail = email;
                invitation.InvitationSentOn = DateTime.Now;
                invitation.FriendRateOn = invitation.InvitationSentOn;
                invitation.KeyCode = key;
                invitation.Message = inviteFriends.Message;
                invitation.User = user;
                invitation.ClosetOutfit = co;
                friendRatingInvitationRepository.DbContext.BeginTransaction();
                friendRatingInvitationRepository.SaveOrUpdate(invitation);
                friendRatingInvitationRepository.DbContext.CommitTransaction();
            }

            if (inviteFriends.SendMe)
            {
                EmailCopyData data = new EmailCopyData
                {
                    Components = co.Components,
                    Comments = inviteFriends.Message,
                    GarmentsUri = Resources.GetGarmentLargePath(),
                    OutfitUpdater = outfitUpdaterRepository.GetRandomOutfitUpdaterFor(co.PreCombination)
                };

                messageSenderService.SendWithTemplate("copyinvite2rate", user, data, user.EmailAddress);
            }

            return Json(new { Success = true });
        }

        public class EmailData
        {
            public string Comments { get; set; }
            public string KeyCode { get; set; }
            public string InvitationLink { get; set; }
            public IList<Garment> Components { get; set; }
            public string GarmentsUri { get; set; }
            public OutfitUpdater OutfitUpdater { get; set; }
        }
        public class EmailCopyData
        {
            public string Comments { get; set; }
            public IList<Garment> Components { get; set; }
            public string GarmentsUri { get; set; }
            public OutfitUpdater OutfitUpdater { get; set; }
        }

        private IList<ClosetOutfitView> Search(out int totalCount, string SortBy, string Search, string Page, string Season, RegisteredUser user)
        {
            return closetOutfitRepository.Search(out totalCount, 10, Convert.ToInt32(Page), Convert.ToInt32(Season), Common.RemoveExtraSpaces(Search), SortBy, user);
        }

        private void GetOutfitsInfo()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem sl = new SelectListItem();
            sl.Text = "Editor Rating";
            sl.Value = 1.ToString();
            lst.Add(sl);
            sl = new SelectListItem();
            sl.Text = "My Rating";
            sl.Value = 2.ToString();
            lst.Add(sl);
            sl = new SelectListItem();
            sl.Text = "InvitedUser Rating";
            sl.Value = 3.ToString();
            lst.Add(sl);
            sl = new SelectListItem();
            sl.Text = "Last Worn Date";
            sl.Value = 4.ToString();
            lst.Add(sl);
            ViewData["SortBy"] = lst;
        }

        [ObjectFilter(Param = "outfitRate", RootType = typeof(OutfitRate))]
        [Transaction]
        [Authorize]
        public ActionResult RateOutfit(OutfitRate outfitRate)
        {
            ClosetOutfit closetOutfit = closetOutfitRepository.Get(outfitRate.ClosetOutfitId);
            if (closetOutfit.Rating == null)
                closetOutfit.Rating = new Rating();
            closetOutfit.Rating.Rate(closetOutfit, outfitRate.Rate);

            if (outfitRate.Rate == 5)
            {
                if (closetOutfit.Closet.FavoriteOutfit != null)
                {
                    closetOutfitRepository.SaveOrUpdate(closetOutfit);
                    return Json(new { Success = true, ReplaceFavorite = true });
                }

                return SetFavorite(outfitRate);
            }
            if (closetOutfit.Closet.FavoriteOutfit != null && closetOutfit.Closet.FavoriteOutfit.Id == closetOutfit.Id)
                return Json(new { Success = true, RemoveFavorite = true });

            closetOutfitRepository.SaveOrUpdate(closetOutfit);
            return Json(new { Success = true });
        }

        [ObjectFilter(Param = "closetoutfitid", RootType = typeof(int))]
        [Transaction]
        [Authorize]
        public ActionResult ClearFavorite(int closetoutfitid)
        {
            ClosetOutfit closetOutfit = closetOutfitRepository.Get(closetoutfitid);
            closetOutfit.Closet.ClearFavoriteOutfit();
            closetOutfitRepository.SaveOrUpdate(closetOutfit);
            return Json(new { Success = true });
        }


        [ObjectFilter(Param = "outfitRate", RootType = typeof(OutfitRate))]
        [Transaction]
        [Authorize]
        public ActionResult SetFavorite(OutfitRate outfitRate)
        {
            ClosetOutfit closetOutfit = closetOutfitRepository.Get(outfitRate.ClosetOutfitId);
            closetRepository.DbContext.BeginTransaction();
            Closet c = closetRepository.Get(this.ClosetId);
            c.SetFavoriteOutfit(closetOutfit);
            closetRepository.SaveOrUpdate(c);
            closetRepository.DbContext.CommitTransaction();
            return Json(new { Success = true, SetFavorite = true });
        }


        private bool UserHasOutfit(int outfitId)
        {
            MembershipUser mu = Membership.GetUser();
            RegisteredUser ru = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
            return ru.Closet.Outfits.Contains(closetOutfitRepository.Get(outfitId));
        }

        private bool IsUserCloset(int? userId)
        {
            if (userId.HasValue)
            {
                RegisteredUser user = registeredUserRepository.Get(userId.Value);
                MembershipUser mu = Membership.GetUser();
                if (mu != null && Convert.ToInt32(mu.ProviderUserKey) == user.MembershipUserId)
                    return true;
            }
            return false;
        }
        
        private int? GetUserId(string userName)
        {
            RegisteredUser registeredUser = GetRegisteredUser(userName);
            if (registeredUser != null)
                return registeredUser.Id;

            return null;
        }

        private bool IsPublicCloset(string userName)
        {
            RegisteredUser registeredUser = GetRegisteredUser(userName);
            if (registeredUser != null && registeredUser.Closet != null)
                return (registeredUser.Closet.PrivacyLevel == PrivacyLevel.FullCloset);
            
            return false;
        }
        
        private RegisteredUser GetRegisteredUser(string userName)
        {
            MembershipUser closetUser = Membership.GetUser(userName);
            if (closetUser != null)
            {
                int membershipId = Convert.ToInt32(closetUser.ProviderUserKey);
                return registeredUserRepository.GetByMembershipId(membershipId);
            }
            return null;
        }
    }
}
