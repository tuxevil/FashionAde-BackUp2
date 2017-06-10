using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.ContentManagement;
using FashionAde.ApplicationServices;
using FashionAde.Data.Repository;
using FashionAde.Core.Accounts;
using System.Web.Security;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class ContentController : BaseController
    {
        IContentService contentService;

        #region Constructors

        public ContentController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        #endregion

        #region ActionResults

        public ActionResult TrendAlerts()
        {
            IList<ContentPublished> lst = contentService.List(new ContentCategory(1) , ContentType.Blog);
            ViewData["contents"] = lst;
            return View();
        }

        public ActionResult Organizing()
        {
            IList<ContentPublished> lst = contentService.List(new ContentCategory(2), ContentType.Blog);
            ViewData["contents"] = lst;
            return View();
        }

        public ActionResult GarmentCare()
        {
            IList<ContentPublished> lst = contentService.List(new ContentCategory(3), ContentType.Blog);
            ViewData["contents"] = lst;
            return View();
        }

        public ActionResult AboutUs()
        {
            ContentPublished c = contentService.Get(new ContentCategory(4), ContentType.Private);
            return View("Private", c);
        }

        public ActionResult Recommendations()
        {
            ContentPublished c = contentService.Get(new ContentCategory(13), ContentType.Private);
            return View("Private", c);
        }

        public ActionResult TermsOfUse()
        {
            ContentPublished c = contentService.Get(new ContentCategory(10), ContentType.Private);
            return View("Private", c);
        }

        public ActionResult Privacy()
        {
            ContentPublished c = contentService.Get(new ContentCategory(11), ContentType.Private);
            return View("Private", c);
        }

        public ActionResult SiteMap()
        {
            ContentPublished c = contentService.Get(new ContentCategory(12), ContentType.Private);
            return View("Private", c);
        }
        
        public ActionResult StyleAlerts()
        {
            return View();
        }

        public ActionResult Content(int id)
        {
            IList<ContentPublished> lst = new List<ContentPublished>();
            lst.Add(contentService.Get(id));
            ViewData["contents"] = lst;
            return View();
        }

        #endregion

        #region Logic Methods

        public void Create(int categoryid)
        { }

        private RegisteredUser GetUser()
        {
            RegisteredUserRepository rur = new RegisteredUserRepository();

            MembershipUser mu = Membership.GetUser();
            if (mu != null)
                ViewData["UserName"] = mu.UserName;
            
            return rur.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
        }


        #endregion
    }
}