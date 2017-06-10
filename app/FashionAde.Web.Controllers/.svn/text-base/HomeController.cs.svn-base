using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core.DataInterfaces;
using System.Collections.Generic;
using FashionAde.Core;
using FashionAde.ApplicationServices;
using FashionAde.Core.MVCInteraction;
using FashionAde.OutfitUpdaterImportation.Controllers;
using FashionAde.Web.Controllers.MVCInteraction;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        private IClosetOutfitRepository closetOutfitRepository;
        private IClosetRepository closetRepository;
        private IContentService contentService;
        private IUserGarmentRepository userGarmentRepository;

        public HomeController(IClosetOutfitRepository closetOutfitRepository, IClosetRepository closetRepository, IUserGarmentRepository userGarmentRepository, IContentService contentService)
        {
            this.closetOutfitRepository = closetOutfitRepository;
            this.closetRepository = closetRepository;
            this.userGarmentRepository = userGarmentRepository;
            this.contentService = contentService;
        }

        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if (user != null)
                return RedirectToAction("RegisteredUser");

            return View();
        }
        
        [Authorize]
        public ActionResult RegisteredUser()
        {
            IList<ClosetOutfitView> lst = closetOutfitRepository.GetTopRatedOutfits(this.UserId);
            foreach (ClosetOutfitView cov in lst)
                cov.ShowAddToMyCloset = false;
            
            HomeRegisteredUserInfo info = new HomeRegisteredUserInfo();
            info.TopRatedOutfits = lst;
            info.UserName = ViewData["UserName"].ToString();
            info.FashionFlavors = (IList<FashionFlavor>) ViewData["fashionFlavors"];
            info.RecentlyUploadedGarments = userGarmentRepository.GetRecentlyUploaded(this.ProxyLoggedUser);
            info.StyleAlerts = contentService.GetRandomStyleAlerts(info.FashionFlavors);
            info.HaveBeenRated = closetOutfitRepository.HaveBeenRated(ClosetId);
            
            //return View(closetRepository.Get(this.ClosetId));
            return View(info);
        }
    }
}
