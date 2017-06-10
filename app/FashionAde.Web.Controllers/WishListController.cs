using System.Collections.Generic;
using System.Web.Mvc;
using FashionAde.ApplicationServices;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Common;
using FashionAde.Web.Controllers.MVCInteraction;
using SharpArch.Web.NHibernate;

namespace FashionAde.Web.Controllers
{
    [Authorize]
    public class WishListController : BaseController 
    {
        private IClosetRepository closetRepository;
        private IWishListRepository wishListRepository;                        
        private IContentService contentService;
        
        public WishListController(IWishListRepository wishListRepository, IClosetRepository closetRepository, IContentService contentService)
        {
            this.wishListRepository = wishListRepository;
            this.closetRepository = closetRepository;
            this.contentService = contentService;
        }

        public ActionResult List()
        {
            WishList wl = wishListRepository.GetForUser(this.ProxyLoggedUser);            
            if (wl == null)            
                wl = new WishList();

            ViewData["styleAlerts"] = contentService.GetRandomStyleAlerts(this.ProxyFlavors);
            return View(wl);
        }


        [ObjectFilter(Param = "wls", RootType = typeof(WishListSelection))]
        public JsonResult AddToCloset(WishListSelection wls)
        {
            closetRepository.DbContext.BeginTransaction();
            // Remove from Wish List
            Garment g = new MasterGarment(wls.GarmentId);
            WishList wl = wishListRepository.GetForUser(this.ProxyLoggedUser);
            wl.RemoveWishGarment(new WishGarment(wls.WishListId));

            // Add to closet
            Closet c = closetRepository.Get(this.ClosetId);
            c.AddGarment(g);
            closetRepository.DbContext.CommitTransaction();

            List<int> ids = new List<int>();
            ids.Add(wls.GarmentId);
            // Update the closet combinations
            (new FashionAde.Utils.OutfitEngineService.OutfitEngineServiceClient()).AddOutfits(this.ClosetId, ids);
            
            return Json(new { Success = true });
        }

        [Transaction]
        [ObjectFilter(Param = "wishGarmentId", RootType = typeof(int))]
        public JsonResult Remove(int wishGarmentId)
        {
            // Remove from Wish List
            WishList wl = wishListRepository.GetForUser(this.ProxyLoggedUser);
            wl.RemoveWishGarment(new WishGarment(wishGarmentId));

            return Json(new { Success = true });
        }
    }
}
