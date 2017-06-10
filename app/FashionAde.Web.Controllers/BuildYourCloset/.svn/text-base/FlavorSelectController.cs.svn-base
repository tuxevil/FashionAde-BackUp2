using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Services;
using FashionAde.Web.Controllers.MVCInteraction;
using FashionAde.Web.Common;
using System.Web.Security;

namespace FashionAde.Web.Controllers.BuildYourCloser
{
    [HandleError]
    public class FlavorSelectController : BuildYourClosetController
    {
        private IFashionFlavorRepository fashionFlavorRepository;
        private IStylePhotographRepository stylePhotographRepository;
        private IBrandSetRepository brandSetRepository;
        private IWordingRepository wordingRepository;
        private IEventTypeRepository eventTypeRepository;
        
        public FlavorSelectController(IFashionFlavorRepository repository, IStylePhotographRepository stylePhotographRepository, IBrandSetRepository brandSetRepository, IWordingRepository wordingRepository, IEventTypeRepository eventTypeRepository)
        {
            this.fashionFlavorRepository = repository;
            this.stylePhotographRepository = stylePhotographRepository;
            this.brandSetRepository = brandSetRepository;
            this.wordingRepository = wordingRepository;
            this.eventTypeRepository = eventTypeRepository;            
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            bool updateFlavors = Membership.GetUser() != null;            
            if (updateFlavors)
                ViewData["FashionFlavorSelected"] = this.ProxyFlavors;
            else
            {
                ViewData["FashionFlavorSelected"] = ClosetState.Flavors;
                //if (Session["FashionFlavorSelected"] != null)
                //{
                //    ViewData["FashionFlavorSelected"] = Session["FashionFlavorSelected"];
                //    Session["FashionFlavorSelected"] = null;
                //}
            }

            ViewData["updateFlavors"] = updateFlavors;
            Session["updateFlavors"] = updateFlavors;            
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
                    selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(value.ToString().Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[2])));
            }

            ClosetState.SetFlavors(selectedFF);
//          Session["FashionFlavorSelected"] = selectedFF;

            if (selectedFF.Count < 2)
            {
                IList<UserFlavor> userFlavors = new List<UserFlavor>();
                if (selectedFF != null)
                {
                    userFlavors.Add(new UserFlavor(selectedFF[0], Convert.ToDecimal(100)));
                    ClosetState.SetUserFlavors(userFlavors);
//                    Session["UserFlavorSelected"] = userFlavors;
                }

                Session["previousUrl"] = "FlavorSelect";

                return ((bool)Session["updateFlavors"])
                    ? RedirectToAction("UpdateUserFlavors", "FlavorChange", new { Flavor1Weight = 100 })
                    : RedirectToAction("Index", "EventTypeSelector");
            }

            return RedirectToAction("Index", "FlavorWeight");
        }

        public ActionResult QuizCompleted(string Flavor1Id, string Flavor2Id)
        {
            List<FashionFlavor> selectedFF = new List<FashionFlavor>();
            
            selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor1Id)));
            if (!string.IsNullOrEmpty(Flavor2Id))
                selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor2Id)));

            //Session["FashionFlavorSelected"] = selectedFF;
            ClosetState.SetFlavors(selectedFF);

            if (selectedFF.Count == 1)
            {
                IList<UserFlavor> userFlavors = new List<UserFlavor>();
                if (selectedFF != null)
                {
                    userFlavors.Add(new UserFlavor(selectedFF[0], Convert.ToDecimal(100)));
                    //Session["UserFlavorSelected"] = userFlavors;
                    ClosetState.SetUserFlavors(userFlavors);
                }

                return RedirectToAction("Index", "EventTypeSelector");
            }
            else
                return RedirectToAction("Index", "FlavorWeight");
        }

        [Authorize]
        public ActionResult FlavorChangeResult(bool flavorsChanged)
        {
            ViewData["flavorsChanged"] = flavorsChanged;
            return View();
        }
    }
}
