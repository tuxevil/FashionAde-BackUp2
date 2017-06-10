using System.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using FashionAde.Core.ContentManagement;
using FashionAde.Data.Repository;
using FashionAde.Core.DataInterfaces;
using FashionAde.ApplicationServices;
using FashionAde.Core;
using SharpArch.Web.NHibernate;
using MvcContrib.Pagination;
using System.Web.Security;

namespace FashionAde.WebAdmin.Controllers.CMS
{
    [HandleError]
    public class GridController : BaseController
    {
        private IContentManagerService contentManagerService;
        private IContentCategoryRepository contentCategoryRepository;        

        #region Constructor

        public GridController(IContentManagerService contentManagerService, IContentCategoryRepository contentCategoryRepository)
        {
            this.contentManagerService = contentManagerService;
            this.contentCategoryRepository = contentCategoryRepository;            
        }

        #endregion

        #region Action Results

        public ActionResult Index(int? page, string text, int? catId, int? typeId)
        {
            Load(catId, typeId);
            ViewData["searchText"] = text;
            IList<Content> lstContent = new List<Content>();
            if(RolePermissionService.IsAdmin())
               ViewData["contents"] = contentManagerService.Search(text, catId, typeId).AsPagination(page ?? 1, 10);
            else
               ViewData["contents"] = contentManagerService.Search(text, catId, typeId, (int)Membership.GetUser().ProviderUserKey).AsPagination(page ?? 1, 10);
             
            return View(ViewData["contents"]);
        }

        public ActionResult Search(string searchText, string category, string contentType)
        {
            return RedirectToAction("Index", new { text = searchText, catId = category, typeId = contentType });
        }

        #endregion

        #region LogicMethods

        private void Load(int? catId, int? typeId)
        {
            List<SelectListItem> lstCategories = new List<SelectListItem>();
            IList<ContentCategory> categories = contentCategoryRepository.GetAll();

            foreach (ContentCategory cc in categories)
            {
                SelectListItem item = new SelectListItem();
                item.Text = cc.Name;
                item.Value = cc.Id.ToString();
                if (catId.HasValue && catId.Value == cc.Id)
                    item.Selected = true;
                lstCategories.Add(item);
            }

            Array typeValues = Enum.GetValues(typeof(ContentType));
            List<SelectListItem> lstContentTypes = new List<SelectListItem>();
            foreach (ContentType ct in typeValues)
            {
                SelectListItem contentType = new SelectListItem();
                contentType.Text = ct.ToString();
                contentType.Value = ((int)ct).ToString();
                if (typeId.HasValue && typeId == (int)ct)
                    contentType.Selected = true;

                lstContentTypes.Add(contentType);
            }

            ViewData["contentTypes"] = lstContentTypes;
            ViewData["contentCategories"] = lstCategories;
        }

        #endregion
    }
}
