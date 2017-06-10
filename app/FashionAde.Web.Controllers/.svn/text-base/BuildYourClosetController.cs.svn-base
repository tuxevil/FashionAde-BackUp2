using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using Castle.Components.Validator;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Services;
using FashionAde.Data.Repository;
using FashionAde.Web.Controllers.MVCInteraction;
using System.Collections.Specialized;
using SharpArch.Web.NHibernate;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class BuildYourClosetController : Controller
    {
        private IFashionFlavorRepository fashionFlavorRepository;
        private IStylePhotographRepository stylePhotographRepository;
        private IBrandSetRepository brandSetRepository;
        private IWordingRepository wordingRepository;
        private IEventTypeRepository eventTypeRepository;
        private ISilouhetteRepository silouhetteRepository;
        private IPatternRepository patternRepository;
        private IFabricRepository fabricRepository;
        private IGarmentRepository garmentRepository;
        private IRegisteredUserRepository registeredUserRepository;
        private IWishListRepository wishListRepository;
        private IClosetRepository closetRepository;
        private IUserSizeRepository userSizeRepository;
        private ISecurityQuestionRepository securityQuestionRepository;
        private IZipCodeRepository zipCodeRepository;
        
        public BuildYourClosetController(IFashionFlavorRepository repository, IStylePhotographRepository stylePhotographRepository, IBrandSetRepository brandSetRepository, IWordingRepository wordingRepository, IEventTypeRepository eventTypeRepository, ISilouhetteRepository silouhetteRepository, IPatternRepository patternRepository, IGarmentRepository garmentRepository, IRegisteredUserRepository registeredUserRepository, IWishListRepository wishListRepository, IClosetRepository closetRepository, IUserSizeRepository userSizeRepository, IFabricRepository fabricRepository, ISecurityQuestionRepository securityQuestionRepository, IZipCodeRepository zipCodeRepository)
        {
            this.fashionFlavorRepository = repository;
            this.stylePhotographRepository = stylePhotographRepository;
            this.brandSetRepository = brandSetRepository;
            this.wordingRepository = wordingRepository;
            this.eventTypeRepository = eventTypeRepository;
            this.silouhetteRepository = silouhetteRepository;
            this.patternRepository = patternRepository;
            this.garmentRepository = garmentRepository;
            this.registeredUserRepository = registeredUserRepository;
            this.wishListRepository = wishListRepository;
            this.closetRepository = closetRepository;
            this.userSizeRepository = userSizeRepository;
            this.fabricRepository = fabricRepository;
            this.securityQuestionRepository = securityQuestionRepository;
            this.zipCodeRepository = zipCodeRepository;
        }

        #region Index View
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["FashionFlavorSelected"] != null)
            {
                ViewData["FashionFlavorSelected"] = Session["FashionFlavorSelected"];
                Session["FashionFlavorSelected"] = null;
            }

            GetDataForIndex();
            return View(fashionFlavorRepository.GetAll());
        }

        private void GetDataForIndex()
        {
            ViewData["StylePhotograph"] = stylePhotographRepository.GetAll();
            ViewData["BrandSet"] = brandSetRepository.GetAll();
            ViewData["Wording"] = wordingRepository.GetAll();
            ViewData["EventType"] = eventTypeRepository.GetAll();
        }

        public RedirectToRouteResult SelectFashionFlavor(FormCollection values)
        {
            List<FashionFlavor> selectedFF = new List<FashionFlavor>();
            
            foreach (var value in values)
            {
                object o = values[value.ToString()];
                if (o.ToString().Contains("true"))
                    selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(value.ToString().Split('_')[2])));
            }
            
            Session["FashionFlavorSelected"] = selectedFF;

            if (selectedFF.Count < 2)
            {
                IList<UserFlavor> userFlavors = new List<UserFlavor>();
                if (selectedFF != null)
                {
                    userFlavors.Add(new UserFlavor(selectedFF[0], Convert.ToDecimal(100)));
                    Session["UserFlavorSelected"] = userFlavors;
                }

                Session["previousUrl"] = "Index";
                return RedirectToAction("EventTypeSelector");
            }

            return RedirectToAction("FlavorWeight");
        }
        #endregion

        #region Flavor Weight
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult FlavorWeight()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["FashionFlavorSelected"] == null)
            {
                GetDataForIndex();
                return View("Index", fashionFlavorRepository.GetAll());
            }

            if (Session["UserFlavorSelected"] != null)
            {
                ViewData["UserFlavorSelected"] = Session["UserFlavorSelected"];
                Session["UserFlavorSelected"] = null;
            }

            IList<FashionFlavor> flavors = Session["FashionFlavorSelected"] as List<FashionFlavor>;            
            return View(flavors);
        }

        public RedirectToRouteResult SetWeight(string Flavor1Weight, string Flavor2Weight, FormCollection values)
        {
            IList<FashionFlavor> flavors = Session["FashionFlavorSelected"] as List<FashionFlavor>;
            IList<UserFlavor> userFlavors = new List<UserFlavor>();
            if(flavors != null)
            {
                if(!string.IsNullOrEmpty(Flavor1Weight))
                    userFlavors.Add(new UserFlavor(flavors[0], Convert.ToDecimal(Flavor1Weight)));
                if (!string.IsNullOrEmpty(Flavor2Weight))
                    userFlavors.Add(new UserFlavor(flavors[1], Convert.ToDecimal(Flavor2Weight)));

                Session["UserFlavorSelected"] = userFlavors;
            }
            Session["previousUrl"] = "FlavorWeight";

            bool previous = ((NameValueCollection)(values)).AllKeys[2].ToLower().Contains("previous");
            if (previous)
                return RedirectToAction("Index");

            return RedirectToAction("EventTypeSelector");
        }

        #endregion

        #region EventTypeSelector View
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult EventTypeSelector()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["UserFlavorSelected"] == null)
            {
                GetDataForIndex();
                return View("Index", fashionFlavorRepository.GetAll());
            }

            if (Session["EventTypeSelected"] != null)
            {
                ViewData["EventTypeSelected"] = Session["EventTypeSelected"];
                Session["EventTypeSelected"] = null;
            }
            
            ViewData["previousUrl"] = Session["previousUrl"];
            return View(eventTypeRepository.GetAll());
        }

        public RedirectToRouteResult SelectEventType(FormCollection values)
        {
            List<EventType> selectedET = new List<EventType>();

            foreach (var value in values)
            {
                object o = values[value.ToString()];
                if (o.ToString().Contains("true"))
                    selectedET.Add(eventTypeRepository.Get(Convert.ToInt32(value.ToString().Split('_')[2])));
            }

            Session["EventTypeSelected"] = selectedET;

            bool previous = ((NameValueCollection)(values)).AllKeys[6].ToLower().Contains("previous");
            if (previous)
                return RedirectToAction(Session["previousUrl"].ToString()); 

            return RedirectToAction("GarmentSelector");;
        }

        #endregion

        #region Final Results
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult FinalResults()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["EventTypeSelected"] == null)
            {
                GetDataForIndex();
                return View("Index", fashionFlavorRepository.GetAll());
            }
            IList<FashionFlavor> flavors = Session["FashionFlavorSelected"] as List<FashionFlavor>;
            IList<EventType> eventTypes = Session["EventTypeSelected"] as List<EventType>;
            FinalResult finalResult = new FinalResult(flavors, eventTypes);

            return View(finalResult);
        }

        public RedirectToRouteResult GoToGarmentSelector(FormCollection values)
        {
            return RedirectToAction("GarmentSelector"); ;
        }

        #endregion
        
        #region GarmentSelector View
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult GarmentSelector()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["FashionFlavorSelected"] == null)
            {
                GetDataForIndex();
                return View("Index", fashionFlavorRepository.GetAll());
            }
            List<FashionFlavor> selectedFF = Session["FashionFlavorSelected"] as List<FashionFlavor>;
            List<EventType> selectedET = (List<EventType>)Session["EventTypeSelected"];

            if (selectedFF != null)
            {
                List<Silouhette> silouhettes = silouhetteRepository.GetByFlavors(selectedFF, selectedET) as List<Silouhette>;
                if (silouhettes != null)
                {
                    IList<Fabric> fabrics = fabricRepository.GetForSilouhette(silouhettes[0], selectedET);
                    Pattern solid = patternRepository.GetSolid();
                    List<Garment> garments = garmentRepository.GetBySelection(silouhettes[0], fabrics[0], solid, selectedET) as List<Garment>;
                    ViewData["Silouhettes"] = silouhettes;
                    ViewData["Garments"] = garments;
                    ViewData["SilouhetteId"] = silouhettes[0].Id;
                    ViewData["PatternId"] = solid.Id;
                    ViewData["Patterns"] = patternRepository.GetAll();
                    ViewData["Fabrics"] = fabrics;
                    ViewData["FabricId"] = fabrics[0].Id;
                }
            }
            

            return View(selectedFF);
        }

        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult Search(Selection selection)
        {
            List<EventType> selectedET = (List<EventType>) Session["EventTypeSelected"];
            List<jsonGarment> finalResult = new List<jsonGarment>();
            
            List<Garment> result = garmentRepository.GetBySelection(silouhetteRepository.Get(selection.SilouhetteId), new Fabric(selection.FabricId), new Pattern(selection.PatternId), selectedET) as List<Garment>;
            if(result != null)
                foreach (Garment garment in result)
                    finalResult.Add(new jsonGarment(garment));

            return Json(finalResult);
        }

        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult GetSilouhette(Selection selection)
        {
            Silouhette silouhette = silouhetteRepository.Get(selection.SilouhetteId);
            List<EventType> selectedET = (List<EventType>)Session["EventTypeSelected"];
            IList<Fabric> fabrics = fabricRepository.GetForSilouhette(silouhette, selectedET);
            SilouhetteSelection silouhetteSelection = new SilouhetteSelection(silouhette.AvailablePatterns, fabrics);
            silouhetteSelection.FabricId = fabricRepository.GetForSilouhette(silouhette, selectedET)[0].Id;

            return Json(silouhetteSelection);
        }

        public RedirectToRouteResult GoToRegistration(string myGarmentsItems, string myWishListItems)
        {
            string[] myGarmentsArray = myGarmentsItems.Split(',');
            string[] myWishListArray = myWishListItems.Split(',');

            List<Garment> mygarments = new List<Garment>();
            List<Garment> mywishlist = new List<Garment>();

            if(myGarmentsArray.Length > 0)
                foreach (string garment in myGarmentsArray)
                    if(!string.IsNullOrEmpty(garment))
                        mygarments.Add(garmentRepository.Get(Convert.ToInt32(garment)));
            if(myWishListArray.Length > 0)
                foreach (string garment in myWishListArray)
                    if (!string.IsNullOrEmpty(garment))
                        mywishlist.Add(garmentRepository.Get(Convert.ToInt32(garment)));
            
            Session["MyGarments"] = mygarments;
            Session["MyWishList"] = mywishlist;

            return RedirectToAction("Registration");
        }

        #endregion

        #region Registration View
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Registration()
        {
            if (IsLogged())
                return View("Redirect");

            if (Session["MyGarments"] == null && Session["MyWishList"] == null)
            {
                GetDataForIndex();
                return View("Index", fashionFlavorRepository.GetAll());
            }
            ViewData["UserFlavor"] = Session["UserFlavorSelected"];
            ViewData["MyGarments"] = Session["MyGarments"] as List<Garment>;
            ViewData["MyWishList"] = Session["MyWishList"] as List<Garment>;
            GetRegistrationInfo();

            return View(new UserRegistration());
        }

        private void GetRegistrationInfo()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            IList<SecurityQuestion> questions = securityQuestionRepository.GetAll();
            foreach (SecurityQuestion question in questions)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = question.Description;
                sl.Value = question.Id.ToString();
                lst.Add(sl);    
            }
            ViewData["securityQuestions"] = lst;

            List<SelectListItem> results = new List<SelectListItem>();
            IList<UserSize> userSizes = userSizeRepository.GetAll();
            foreach (UserSize userSize in userSizes)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = userSize.Description;
                sli.Value = userSize.Id.ToString();
                results.Add(sli);
            }
            ViewData["UserSizes"] = results;
        }

        [Transaction]
        public ViewResult Register(UserRegistration userRegistration)
        {
            var errors = userRegistration.Validate();
            if (errors == null)
            {
                IList<UserFlavor> userFlavors = Session["UserFlavorSelected"] as List<UserFlavor>;
                IList<EventType> eventTypes = Session["EventTypeSelected"] as List<EventType>;
                IList<Garment> mygarments = Session["MyGarments"] as List<Garment>;
                IList<Garment> mywishlist = Session["MyWishList"] as List<Garment>;

                PublicUser user = new PublicUser();
                user.EmailAddress = userRegistration.Email;
                user.ChangeZipCode(userRegistration.ZipCode);
                user.SetFlavors(userFlavors);
                user.Size = new UserSize(Convert.ToInt32(userRegistration.UserSize));

                //TODO: Get the UserId from ASP.NET Membership
                MembershipCreateStatus status;
                MembershipUser mu = Membership.CreateUser(userRegistration.UserName, userRegistration.Password, userRegistration.Email, securityQuestionRepository.Get(Convert.ToInt32(userRegistration.SecurityQuestion)).Description, userRegistration.SecurityAnswer, true, out status);
                if (status != MembershipCreateStatus.Success)
                {
                    errors = new ErrorSummary();
                    errors.RegisterErrorMessage("MembershipUser", status.ToString());
                    return RegistrationError(userRegistration, errors.ErrorMessages);
                }
                user.MembershipUserId = Convert.ToInt32(mu.ProviderUserKey);
                user.FirstName = string.Empty;
                user.LastName = string.Empty;
                user.PhoneNumber = string.Empty;
                
                if (eventTypes != null)
                    foreach (EventType eventType in eventTypes)
                        user.AddEventType(eventType);

                registeredUserRepository.SaveOrUpdate(user);
                Closet closet = new Closet();
                closet.User = user;
                closet.PrivacyLevel = PrivacyLevel.Private;

                closetRepository.SaveOrUpdate(closet);
                if (mygarments != null)
                {
                    foreach (Garment garment in mygarments)
                        closet.AddGarment(garment);
                    closetRepository.SaveOrUpdate(closet);
                }
                user.Closet = closet;
                
                registeredUserRepository.SaveOrUpdate(user);

                if (mywishlist != null && mywishlist.Count > 0)
                {
                    WishList wl = new WishList();
                    wl.User = user;
                    foreach (Garment wishlist in mywishlist)
                    {
                        wl.AddGarment(wishlist);
                    }
                    wishListRepository.SaveOrUpdate(wl);
                }
                closetRepository.GenerateCloset(user);

                Session.Abandon();
                Session["UserRegistration"] = mu;
                return View("RegistrationFinish", userRegistration);

            }

            return RegistrationError(userRegistration, errors.ErrorMessages);
        }

        private ViewResult RegistrationError(UserRegistration userRegistration, string[] result)
        {
            ViewData["Errors"] = result;
            GetRegistrationInfo();
            return View("Registration", userRegistration);
        }

        [ObjectFilter(Param = "email", RootType = typeof(string))]
        public ActionResult CheckEmail(string email)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (regex.IsMatch(email))
            {
                if (registeredUserRepository.GetByMail(email) == null)
                    return Json(new
                                    {
                                        Exist = false,
                                        RegExError = false,
                                        Email = email
                                    });
                return Json(new
                                {
                                    Exist = true,
                                    RegExError = false,
                                    Email = email
                                });
            }
            return Json(new
            {
                Exist = true,
                RegExError = true,
                Email = email
            });
        }

        [ObjectFilter(Param = "username", RootType = typeof(string))]
        public ActionResult CheckUsername(string username)
        {
            if(IsReserved(username))
                return Json(new
                {
                    Exist = false,
                    Reserved = true,
                    Username = username
                });
            string user = CheckUser(username);
            if (user == username)
                return Json(new
                {
                    Exist = false,
                    Reserved = false,
                    Username = username
                });


            return Json(new
            {
                Exist = true,
                Username = username,
                Recommended = user
            });
        }

        [ObjectFilter(Param = "code", RootType = typeof(string))]
        public ActionResult CheckZipCode(string code)
        {
            ZipCode zipcode = zipCodeRepository.GetByCode(code);
            if (zipcode != null)
                return Json(new
                {
                    Exist = true                    
                });


            return Json(new
            {
                Exist = false,
                ZipCode = code
            });
        }

        private string CheckUser(string username)
        {
            if (Membership.GetUser(username) != null)
            {
                Random r = new Random();
                return CheckUser(username + r.Next(99)); 
            }
            return username;
        }

        private bool IsReserved(string username)
        {
            foreach (string reservedUsername in ReservedUsernames)
                if(reservedUsername.ToLower() == username.ToLower())
                    return true;
            return false;
        }

        private string[] ReservedUsernames
        {
            get { return "Administrator,Administrador,Administra,Administro,Admin,Adm".Split(','); }
        }

        #endregion

        #region Registration Finish
        [AcceptVerbs(HttpVerbs.Get)]
        public RedirectToRouteResult RegistrationFinish()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Redirect
        [AcceptVerbs(HttpVerbs.Get)]
        public RedirectToRouteResult Redirect()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Quiz
        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult GetResult(Selection selection)
        {
            //Convert the object from the page in a string array
            string[] selectedCheckBoxes = selection.Ids;

            List<int> stylePhotographList = new List<int>();
            List<int> brandSetList = new List<int>();
            List<int> wordingList = new List<int>();
            //Work on every checkbox that the page sent to us
            for (int i = 0; i < selectedCheckBoxes.Length; i++)
            {
                //Clean the string
                selectedCheckBoxes[i] = selectedCheckBoxes[i].Replace("chb_", string.Empty);
                //Split the name of the checkbox to know wich type it is and its Id
                string[] temp = selectedCheckBoxes[i].Split('_');
                
                //For each type we act in different way
                switch (temp[0])
                {
                    //For StylePhotograph
                    case "SP":
                        //We get the fashionflavor of the StylePhotograph selected
                        stylePhotographList.Add(Convert.ToInt32(temp[1]));
                        break;
                    //For BrandSet
                    case "BS":
                        //We get the fashionflavor of the BrandSet selected
                        brandSetList.Add(Convert.ToInt32(temp[1]));
                        break;
                    //For Wording
                    case "W":
                        //We get the fashionflavor of the BrandSet selected
                        wordingList.Add(Convert.ToInt32(temp[1]));
                        break;
                }
            }

            QuizController qc = new QuizController(fashionFlavorRepository, stylePhotographRepository, brandSetRepository, wordingRepository, new FlavorSelectionService());
            List<FashionFlavor> result = qc.DetermineFlavors(stylePhotographList, brandSetList, wordingList) as List<FashionFlavor>;
            
            if(result.Count == 2)
                return Json(new
                                {
                                    Single = false,
                                    FashionFlavor1Id = result[0].Id,
                                    FashionFlavor1Name = result[0].Name,
                                    FashionFlavor1Image = result[0].Image,
                                    FashionFlavor1Result = 50,
                                    FashionFlavor2Id = result[1].Id,
                                    FashionFlavor2Name = result[1].Name,
                                    FashionFlavor2Image = result[1].Image,
                                    FashionFlavor2Result = 50
                                });
            return Json(new
            {
                Single = true,
                FashionFlavor1Id = result[0].Id,
                FashionFlavor1Name = result[0].Name,
                FashionFlavor1Image = result[0].Image,
                FashionFlavor1Result = 50
            });
        }

        public RedirectToRouteResult ContinueBuilding(string Flavor1Id, string Flavor2Id)
        {
            List<FashionFlavor> selectedFF = new List<FashionFlavor>();
            
            selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor1Id)));
            if(!string.IsNullOrEmpty(Flavor2Id))
                selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor2Id)));

            Session["FashionFlavorSelected"] = selectedFF;
            
            if (selectedFF.Count < 2)
            {
                IList<UserFlavor> userFlavors = new List<UserFlavor>();
                if (selectedFF != null)
                {
                    userFlavors.Add(new UserFlavor(selectedFF[0], Convert.ToDecimal(100)));
                    Session["UserFlavorSelected"] = userFlavors;
                }

                Session["previousUrl"] = "Index";
                return RedirectToAction("EventTypeSelector");
            }

            return RedirectToAction("FlavorWeight");
        }

        #endregion

        #region CheckIfLogged

        private bool IsLogged()
        {
            if(Membership.GetUser() != null)
                return true;
            return false;
        }

        #endregion
    }
}
