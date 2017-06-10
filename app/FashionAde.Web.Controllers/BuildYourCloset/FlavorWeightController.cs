using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;

namespace FashionAde.Web.Controllers.BuildYourCloser
{
    public class FlavorWeightController : BuildYourClosetController
    {
        private readonly IFashionFlavorRepository repFlavors;

        public FlavorWeightController(IFashionFlavorRepository repFlavors)
        {
            this.repFlavors = repFlavors;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index(string updateFlavors)
        {
            if (ClosetState.Flavors == null)
                Response.Redirect(Url.RouteUrl(new { controller = "FlavorSelect", action = "Index" }));

            IList<FashionFlavor> lstFlavors = new List<FashionFlavor>();  
            if (ClosetState.UserFlavors != null)
            {
                ViewData["UserFlavorSelected"] = ClosetState.UserFlavors;
            }

            IList<FashionFlavor> flavors = repFlavors.GetByIds((from f in ClosetState.Flavors select f.Id).ToList<int>());
            return View(flavors);
        }

        public RedirectToRouteResult SetWeight(string Flavor1Weight, string Flavor2Weight, FormCollection values)
        {
            IList<FashionFlavor> flavors = ClosetState.Flavors;
            IList<UserFlavor> userFlavors = new List<UserFlavor>();
            if (flavors != null)
            {
                if (!string.IsNullOrEmpty(Flavor1Weight))
                    userFlavors.Add(new UserFlavor(flavors[0], Convert.ToDecimal(Flavor1Weight)));
                if (!string.IsNullOrEmpty(Flavor2Weight))
                    userFlavors.Add(new UserFlavor(flavors[1], Convert.ToDecimal(Flavor2Weight)));

                ClosetState.SetUserFlavors(userFlavors);
            }

            Session["previousUrl"] = "FlavorWeight";

            bool previous = ((NameValueCollection)(values)).AllKeys[2].ToLower().Contains("previous");
            if (previous)
                Response.Redirect(Url.RouteUrl(new { controller = "FlavorSelect", action = "Index" }));

            return RedirectToAction("Index", "EventTypeSelector");
        }
    }
}
