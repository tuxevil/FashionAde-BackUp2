using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Controllers.MVCInteraction;
using FashionAde.Web.Common;
using SharpArch.Web.NHibernate;
using System.Web.Security;
using FashionAde.Utils;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class GarmentSelectorController : BuildYourClosetController
    {
        #region Constructors

        private ISilouhetteRepository silouhetteRepository;
        private IPatternRepository patternRepository;
        private IFabricRepository fabricRepository;
        private IGarmentRepository garmentRepository;
        private IRegisteredUserRepository registeredUserRepository;
        private IWishListRepository wishListRepository;
        private IClosetRepository closetRepository;
        private IFashionFlavorRepository fashionFlavorRepository;
        private IEventTypeRepository eventTypeRepository;

        public GarmentSelectorController(IEventTypeRepository eventTypeRepository, ISilouhetteRepository silouhetteRepository, IPatternRepository patternRepository, IFabricRepository fabricRepository, IGarmentRepository garmentRepository, IRegisteredUserRepository registeredUserRepository, IWishListRepository wishListRepository, IClosetRepository closetRepository, IFashionFlavorRepository fashionFlavorRepository)
        {
            this.silouhetteRepository = silouhetteRepository;
            this.patternRepository = patternRepository;
            this.garmentRepository = garmentRepository;
            this.fabricRepository = fabricRepository;
            this.registeredUserRepository = registeredUserRepository;
            this.wishListRepository = wishListRepository;
            this.closetRepository = closetRepository;
            this.fashionFlavorRepository = fashionFlavorRepository;
            this.eventTypeRepository = eventTypeRepository;
        }

        #endregion

        #region Access Methods

        [Authorize]
        public ActionResult AddGarments()
        {
            return RedirectToAction("Index");
        }

        public ViewResult Index(GarmentsListData gld)
        {
            return GarmentsListForExistingUser();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return GarmentsListForNewUser();
            return GarmentsListForExistingUser();
        }


        private ViewResult GarmentsListForExistingUser()
        {
            MembershipUser mu = Membership.GetUser();
            RegisteredUser user = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
            GarmentsListData gld = new GarmentsListData();
            foreach (UserFlavor userFlavor in user.UserFlavors)
                gld.FashionFlavors.Add(userFlavor.Flavor);
            GetGarmentsListData(user, gld);
            return View(gld);
        }

        private ViewResult GarmentsListForNewUser()
        {
            if (ClosetState.Flavors == null)
                Response.Redirect(Url.RouteUrl(new { controller = "FlavorSelect", action = "Index" }));

            GarmentsListData gld = GetGarmentsListDataForNewUser();
            return View(gld);
        }

        private GarmentsListData GetGarmentsListDataForNewUser()
        {
            GarmentsListData gld = new GarmentsListData();
            gld.Controller = "GarmentSelector";
            gld.Action = "GoToRegistration";
            gld.FashionFlavors = fashionFlavorRepository.GetByIds((from f in ClosetState.Flavors select f.Id).ToList<int>()).ToList<FashionFlavor>();
            gld.EventTypes = eventTypeRepository.GetByIds((from e in ClosetState.EventTypes select e.Id).ToList<int>());

            if (gld.FashionFlavors != null)
            {
                gld.Silouhettes = silouhetteRepository.GetByFlavors(gld.FashionFlavors, gld.EventTypes) as List<Silouhette>;
                if (gld.Silouhettes != null)
                {
                    gld.Fabrics = fabricRepository.GetForSilouhette(gld.Silouhettes[0], gld.EventTypes);
                    gld.FabricId = gld.Fabrics.FirstOrDefault().Id;
                    gld.SilouhetteId = gld.Silouhettes[0].Id;
                    Pattern solid = patternRepository.GetSolid();
                    gld.Garments = garmentRepository.GetBySelection(gld.Silouhettes[0], gld.Fabrics.FirstOrDefault(), solid, gld.EventTypes, new List<ClosetGarment>(), gld.MyWishGarments);
                    gld.PatternId = solid.Id;
                    gld.Patterns = patternRepository.GetAll();
                }
            }
            return gld;
        }

        #endregion

        #region Selector Page
        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ViewResult Selector()
        {
            return View();
        }

        #endregion

        #region Change Flavor

        [Authorize]
        public ViewResult ChangeFlavor(string FashionFlavorAlternative, string NewGarmentsItems, string NewWishListItems)
        {
            MembershipUser mu = Membership.GetUser();
            RegisteredUser user = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
            GarmentsListData gld = new GarmentsListData();
            gld.FashionFlavors.Add(fashionFlavorRepository.Get(Convert.ToInt32(FashionFlavorAlternative)));
            gld.NewGarmentsIds = NewGarmentsItems;
            gld.NewWishGarmentsIds = NewWishListItems;
            GetGarmentsListData(user, gld);
            return View("Index", gld);
        }

        #endregion

        #region View Data Creation
        private void GetGarmentsListData(RegisteredUser user, GarmentsListData gld)
        {
            ViewData["UserName"] = User.Identity.Name;
            gld.ForEdit = true;
            gld.Controller = "GarmentSelector";
            gld.Action = "GoToMyGarments";
            gld.EventTypes = user.EventTypes;
            foreach (ClosetGarment closetGarment in closetRepository.GetGarmentsForUser(user))
            {
                gld.MyGarmentsIds += closetGarment.Garment.Id + ",";
                gld.MyGarments.Add(closetGarment);
            }
            if (!string.IsNullOrEmpty(gld.MyGarmentsIds))
                gld.MyGarmentsIds = gld.MyGarmentsIds.TrimEnd(',');

            WishList wl = wishListRepository.GetForUser(user);
            if (wl != null)
            {
                foreach (WishGarment wishGarment in wl.Garments)
                {
                    gld.MyWishGarmentsIds += wishGarment.Garment.Id + ",";
                    gld.MyWishGarments.Add(wishGarment);
                }
                if (gld.MyGarmentsIds.Length > 0)
                    gld.MyGarmentsIds = gld.MyGarmentsIds.TrimEnd(',');
            }

            if(!string.IsNullOrEmpty(gld.NewGarmentsIds))
            {
                List<int> newGarmentsIds = new List<int>();
                foreach (string id in gld.NewGarmentsIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    if(!string.IsNullOrEmpty(id) && id != " ")
                        newGarmentsIds.Add(Convert.ToInt32(id));
                gld.NewGarments = garmentRepository.GetByIds(newGarmentsIds);
            }

            if (!string.IsNullOrEmpty(gld.NewWishGarmentsIds))
            {
                List<int> newWishGarmentsIds = new List<int>();
                foreach (string id in gld.NewWishGarmentsIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    if (!string.IsNullOrEmpty(id) && id != " ")
                        newWishGarmentsIds.Add(Convert.ToInt32(id));
                gld.NewWishGarments = garmentRepository.GetByIds(newWishGarmentsIds);
            }
            
            if (gld.FashionFlavors != null)
            {
                gld.Silouhettes = silouhetteRepository.GetByFlavors(gld.FashionFlavors, gld.EventTypes) as List<Silouhette>;
                if (gld.Silouhettes != null)
                {
                    gld.Fabrics = fabricRepository.GetForSilouhette(gld.Silouhettes[0], gld.EventTypes);
                    gld.FabricId = gld.Fabrics.FirstOrDefault().Id ;
                    gld.SilouhetteId = gld.Silouhettes[0].Id;
                    Pattern solid = patternRepository.GetSolid();
                    gld.Garments = garmentRepository.GetBySelection(gld.Silouhettes[0], gld.Fabrics.FirstOrDefault(), solid, gld.EventTypes, gld.MyGarments, gld.MyWishGarments);
                    gld.PatternId = solid.Id;
                    gld.Patterns = patternRepository.GetAll();
                }
            }
            IList<FashionFlavor> flavors = fashionFlavorRepository.GetAll();
            foreach (FashionFlavor flavor in flavors)
                if(!gld.FashionFlavors.Exists(delegate(FashionFlavor record) { if (record.Id == flavor.Id) { return true; } return false; }))
                    gld.FashionFlavorsAlternative.Add(flavor);
        }
        #endregion

        #region Ajax Methods

        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult Search(Selection selection)
        {
            IList<EventType> selectedET;
            IList<ClosetGarment> closetGarments = new List<ClosetGarment>();
            IList<WishGarment> wishGarments = new List<WishGarment>();

            MembershipUser mu = Membership.GetUser();
            if (mu == null)
                selectedET = eventTypeRepository.GetByIds((from e in ClosetState.EventTypes select e.Id).ToList<int>());
            else
            {
                WishList wl = wishListRepository.GetForUser(this.ProxyLoggedUser);
                if (wl != null)
                    wishGarments = wishListRepository.GetForUser(this.ProxyLoggedUser).Garments;
                closetGarments = closetRepository.GetGarmentsForUser(this.ProxyLoggedUser);
                selectedET = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey)).EventTypes;
            }
            HashSet<jsonGarment> finalResult = new HashSet<jsonGarment>();

            HashSet<Garment> result = new HashSet<Garment>(
                garmentRepository.GetBySelection(silouhetteRepository.Get(selection.SilouhetteId),
                new Fabric(selection.FabricId),
                new Pattern(selection.PatternId),
                selectedET,
                closetGarments,
                wishGarments));
            if (result != null)
                foreach (Garment garment in result)
                    finalResult.Add(new jsonGarment(garment));

            return Json(finalResult);
        }

        [ObjectFilter(Param = "myGarmentsItems", RootType = typeof(string))]
        public JsonResult HasValidCombinations(string myGarmentsItems)
        {
            string[] myGarmentsArray = myGarmentsItems.Split(',');            

            List<Garment> mygarments = new List<Garment>();
            List<int> mygarmentsids = new List<int>();
            List<int> fashionflavorsids = new List<int>();

            if (myGarmentsArray.Length > 0)
                foreach (string garment in myGarmentsArray)
                    if (!string.IsNullOrEmpty(garment))
                    {
                        mygarments.Add(garmentRepository.Get(Convert.ToInt32(garment)));
                        mygarmentsids.Add(Convert.ToInt32(garment));
                    }

            IList<FashionFlavor> lstSelectedFlavors = (Membership.GetUser() != null)
                                                          ? ProxyFlavors
                                                          : (IList<FashionFlavor>) ClosetState.Flavors;

            foreach (FashionFlavor ff in lstSelectedFlavors)
                fashionflavorsids.Add(ff.Id);

            if (new FashionAde.Utils.OutfitEngineService.OutfitEngineServiceClient().HasValidCombinations(mygarmentsids, fashionflavorsids))
                return Json(new { HasValidCombinations = true });

            return Json(new { HasValidCombinations = false });
        }

        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult GetSilouhette(Selection selection)
        {
            SilouhetteSelection silouhetteSelection = new SilouhetteSelection();
            try
            {
                Silouhette silouhette = silouhetteRepository.Get(selection.SilouhetteId);
                IList<EventType> selectedET;
                MembershipUser mu = Membership.GetUser();
                if (mu == null)
                    selectedET = eventTypeRepository.GetByIds((from e in ClosetState.EventTypes select e.Id).ToList<int>());
                else
                    selectedET = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey)).EventTypes;
                HashSet<Fabric> fabrics = new HashSet<Fabric>(fabricRepository.GetForSilouhette(silouhette, selectedET));
                silouhetteSelection.Patterns = new HashSet<Pattern>(silouhette.AvailablePatterns);
                silouhetteSelection.Fabrics = fabrics;
                silouhetteSelection.Success = true;
            }
            catch (Exception)
            {
                silouhetteSelection.Success = false;
                //throw;
            }
            return Json(silouhetteSelection);
        }

        #endregion

        #region Save Methods

        [Authorize]
        public RedirectToRouteResult GoToMyGarments(string myGarmentsItems, string myWishListItems)
        {
            string[] myGarmentsArray = myGarmentsItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] myWishListArray = myWishListItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            IList<Garment> myGarments = new List<Garment>();
            List<int> myGarmentsIds = new List<int>();
            IList<Garment> myWishGarments = new List<Garment>();
            List<int> myWishGarmentsIds = new List<int>();

            if (myGarmentsArray.Length > 0)
                foreach (string garment in myGarmentsArray)
                    if (!string.IsNullOrEmpty(garment) && StringHelper.IsNumber(garment))
                        myGarmentsIds.Add(Convert.ToInt32(garment));
            if (myWishListArray.Length > 0)
                foreach (string garment in myWishListArray)
                    if (!string.IsNullOrEmpty(garment) && StringHelper.IsNumber(garment))
                        myWishGarmentsIds.Add(Convert.ToInt32(garment));

            garmentRepository.DbContext.BeginTransaction();
            myGarments = garmentRepository.GetByIds(myGarmentsIds);
            myWishGarments = garmentRepository.GetByIds(myWishGarmentsIds);
            List<int> newGarmentsIds = new List<int>();
            RegisteredUser user = registeredUserRepository.Get(this.UserId);
            if (myGarments.Count > 0)
            {
                foreach (Garment garment in myGarments)
                    if(!user.Closet.HasGarment(garment))
                    {
                        user.Closet.AddGarment(garment);
                        newGarmentsIds.Add(garment.Id);
                    }
                registeredUserRepository.SaveOrUpdate(user);
            }

            if (myWishGarments.Count > 0)
            {
                WishList wl = wishListRepository.GetForUser(this.ProxyLoggedUser);
                if (wl == null)
                {
                    wl = new WishList();
                    wl.User = this.ProxyLoggedUser;
                }
                foreach (Garment garment in myWishGarments)
                    wl.AddGarment(garment);
                wishListRepository.SaveOrUpdate(wl);
            }

            garmentRepository.DbContext.CommitTransaction();

            if (newGarmentsIds.Count > 0)
                // Update the closet combinations
                new FashionAde.Utils.OutfitEngineService.OutfitEngineServiceClient().AddOutfits(user.Closet.Id, newGarmentsIds);

            return RedirectToAction("Index", "MyGarments");
        }

        public ActionResult GoToRegistration(string myGarmentsItems, string myWishListItems)
        {
            string[] myGarmentsArray = myGarmentsItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] myWishListArray = myWishListItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            ClosetState.SetAddGarments((from s in myGarmentsArray select Convert.ToInt32(s)).ToList<int>());
            ClosetState.SetWishGarments((from s in myWishListArray select Convert.ToInt32(s)).ToList<int>());

            return RedirectToAction("Index", "Registration");
        }

        #endregion
    }
}
