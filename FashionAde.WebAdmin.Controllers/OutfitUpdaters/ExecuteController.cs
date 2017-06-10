using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FashionAde.WebAdmin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ExecuteController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string data)
        {
            new OutfitUpdaters.OutfitUpdaterServiceClient().UpdateFeeds();
            TempData["status"] = "Process started succesfully.";
            return RedirectToAction("Index");
        }

    }
}
