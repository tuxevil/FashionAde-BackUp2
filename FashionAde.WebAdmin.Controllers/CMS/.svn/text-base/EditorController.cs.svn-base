using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using FashionAde.ApplicationServices;
using FashionAde.Core;
using FashionAde.Core.ContentManagement;
using FashionAde.Core.DataInterfaces;
using FashionAde.WebAdmin.Controllers.MVCInteraction;
using MvcContrib.Pagination;
using SharpArch.Web.NHibernate;
using FashionAde.Web.Common;
using System.Web.Security;
using FashionAde.Core.Accounts;

namespace FashionAde.WebAdmin.Controllers.CMS
{
    [HandleError]
    public class EditorController : BaseController
    {
        private IContentManagerService contentManagerService;
        private IRegisteredUserRepository registeredUserRepository;
        private IFashionFlavorRepository fashionFlavorRepository;
        private IContentCategoryRepository contentCategoryRepository;
        private EditorViewData viewData = new EditorViewData();

        #region Constructor

        public EditorController(IContentManagerService contentManagerService, IContentCategoryRepository contentCategoryRepository, IRegisteredUserRepository registeredUserRepository, IFashionFlavorRepository fashionFlavorRepository)
        {
            this.contentManagerService = contentManagerService;
            this.registeredUserRepository = registeredUserRepository;
            this.fashionFlavorRepository = fashionFlavorRepository;
            this.contentCategoryRepository = contentCategoryRepository;
        }
        
        #endregion

        #region Action Methods

        public ActionResult Create()
        {
            ContentView c = new ContentView();
            c.Type = Convert.ToInt32(ContentType.Blog);
            c.Status = ContentStatus.Draft;
            c.Sections.Add(new ContentViewSection());
            FillViewData(c);
            return View("Editor", viewData);
        }

        public ActionResult Edit(int id)
        {
            Content c = contentManagerService.Get(id);

            if (!RolePermissionService.CanAccess(c))
                throw new Exception("You do not have permission to access this content.");

            FillViewData(TransformToView(c),c);
            return View("Editor", viewData);
        }

        [Transaction]
        [ObjectFilter(Param = "id", RootType = typeof(int))]
        public ActionResult Delete(int id)
        {
            contentManagerService.Delete(id);
            return Json(new { Success = true });
        }

        [Transaction]
        [ObjectFilter(Param = "schedule", RootType = typeof(Schedule))]
        public ActionResult Schedule(Schedule schedule)
        {
            DateTime from = Convert.ToDateTime(schedule.From);
            DateTime? to = null;
            if (!string.IsNullOrEmpty(schedule.To))
                to = Convert.ToDateTime(schedule.To);

            contentManagerService.ScheduleContent(schedule.ContentId, from, to);
            return Json(new { Success = true });
        }

        [ObjectFilter(Param = "typeId", RootType = typeof(int))]
        public JsonResult ListCategoriesByType(int typeId)
        {
            return Json(contentCategoryRepository.ListByContentType(typeId));
        }

        [Transaction]
        [ValidateInput(false)]
        [ObjectFilter(Param = "assign", RootType = typeof(Assign))]
        public ActionResult Assign(Assign assign)
        {
            MembershipUser mu = Membership.GetUser(assign.PublisherId);

            if (mu != null)
            {
                if (Roles.IsUserInRole(mu.UserName,"editor"))
                    contentManagerService.SendToReview(assign.ContentId, assign.PublisherId);

                if (Roles.IsUserInRole(mu.UserName, "publisher"))
                    contentManagerService.SendToPublish(assign.ContentId, assign.PublisherId);

                if (Roles.IsUserInRole(mu.UserName, "author"))
                    contentManagerService.SendToVerify(assign.ContentId, assign.PublisherId);
            }

            return Json(new { Success = true });
        }

        [Transaction]
        [ValidateInput(false)]
        [ObjectFilter(Param = "id", RootType = typeof(int))]
        public ActionResult Disable(int id)
        {
            contentManagerService.Disable(id);
            return Json(new { Success = true });
        }

        [Transaction]
        [ValidateInput(false)]
        [ObjectFilter(Param = "id", RootType = typeof(int))]
        public ActionResult Enable(int id)
        {
            contentManagerService.Enable(id);
            return Json(new { Success = true });
        }

        [Transaction]
        [ValidateInput(false)]
        public ActionResult Approve(ContentView content, int[] flavors)
        {
            if (ModelState.IsValid)
            {
                SaveData(content, flavors);
                contentManagerService.Approve(content.Id);
                return RedirectToAction("Index", "Grid");
            }

            FillViewData(content);
            return View("Editor", viewData);
        }

        [ValidateInput(false)]
        public ActionResult AddSection(ContentView content)
        {
            content.Sections.Add(new ContentViewSection());
            FillViewData(content);
            return View("Editor", viewData);
        }

        [Transaction]
        [ValidateInput(false)]
        public ActionResult Save(ContentView content, int[] flavors)
        {
            if (ModelState.IsValid)
            {
                SaveData(content, flavors);
                return RedirectToAction("Index", "Grid");
            }

            FillViewData(content);
            return View("Editor", viewData);
        }

        #endregion

        #region Save Data 
        private void SaveData(ContentView content, int[] flavors)
        {
            IList<FashionFlavor> lstFlavors = new List<FashionFlavor>();

            if (flavors != null)
            {
                for (int i = 0; i < flavors.Length; i++)
                    lstFlavors.Add(fashionFlavorRepository.Get(flavors[i]));
            }

            IList<ContentSection> lstSections = new List<ContentSection>();
            foreach (ContentViewSection cs in content.Sections)
                lstSections.Add(new ContentSection { Body = cs.Body, Title = cs.Title, FashionFlavor = cs.FashionFlavor });

            if (content.Id == 0)
                contentManagerService.Create(content.Title, content.Body, content.Keywords, content.PromotionalText, new ContentCategory(content.Category.Value), Convert.ToInt32(Membership.GetUser().ProviderUserKey), lstFlavors, lstSections);
            else
            {
                contentManagerService.Edit(content.Id, content.Title, content.Body, content.Keywords, content.PromotionalText, new ContentCategory(content.Category.Value), (ContentType)content.Type, lstFlavors, lstSections);

                // If a editor/author wants to edit a publish content, it should be Send To Verify.
                if (content.Status == ContentStatus.Published && RolePermissionService.CanCreate())
                    contentManagerService.SendToVerify(content.Id, Convert.ToInt32(Membership.GetUser().ProviderUserKey));

            }
        }

        private ContentView TransformToView(Content c)
        {
            ContentView cv = new ContentView();
            cv.Body = c.Body;
            cv.Category = Convert.ToInt32(c.Category.Id);
            cv.Id = c.Id;
            cv.Keywords = c.Keywords;
            cv.PromotionalText = c.PromotionalText;
            cv.Title = c.Title;
            cv.Type = Convert.ToInt32(c.Type);
            cv.Status = c.Status;

            foreach (ContentSection cs in c.Sections)
                cv.Sections.Add(new ContentViewSection { Id = cs.Id, Body = cs.Body, FashionFlavor = cs.FashionFlavor, Title = cs.Title });

            return cv;
        }

        #endregion

        #region Logic Methods

        private void FillViewData(ContentView cv)
        {
            FillViewData(cv, null);
        }

        private void FillViewData(ContentView cv, Content c)
        {
            IList<ContentCategory> categories = contentCategoryRepository.GetAll();
            
            List<SelectListItem> lstPublishers = new List<SelectListItem>();
            string[] users = null;
            MembershipUserCollection membershipUsers = new MembershipUserCollection();

            if (RolePermissionService.IsAuthor())
            {
                users = Roles.GetUsersInRole("editor");
                foreach (string editorName in users)
                    membershipUsers.Add(Membership.GetUser(editorName));
            }

            if (RolePermissionService.IsEditor())
            {
                users = Roles.GetUsersInRole("author");
                foreach (string authorName in users)
                    membershipUsers.Add(Membership.GetUser(authorName));

                users = Roles.GetUsersInRole("publisher");
                foreach (string publisherName in users)
                    membershipUsers.Add(Membership.GetUser(publisherName));
            }

            if (RolePermissionService.IsPublisher())
            {
                users = Roles.GetUsersInRole("editor");
                foreach (string editorName in users)
                    membershipUsers.Add(Membership.GetUser(editorName));
            }
                                    
            foreach (MembershipUser mu in membershipUsers)
            {
                SelectListItem item = new SelectListItem();
                item.Text = mu.UserName;
                item.Value = mu.ProviderUserKey.ToString();
                lstPublishers.Add(item);
            }

            Array typeValues = Enum.GetValues(typeof(ContentType));
            List<SelectListItem> lstContentTypes = new List<SelectListItem>();

            if (c == null && cv.Id != 0)
                c = contentManagerService.Get(cv.Id);
            
            ContentType type = ContentType.Blog;
            if (c != null)
                type = c.Type;

            SelectListItem itemType = new SelectListItem();
            itemType.Text = type.ToString();
            itemType.Value = ((int)type).ToString();
            lstContentTypes.Add(itemType);

            IList<FashionFlavor> flavors = fashionFlavorRepository.GetAll();

            if (c == null && cv.Id != 0)
                c = contentManagerService.Get(cv.Id);
            
            if (c != null)
            {
                viewData.CanApprove = RolePermissionService.CanApprove(c);
                viewData.CanAssign = RolePermissionService.CanAssignTo(c);
            }

            viewData.Content = cv;
            viewData.Flavors = flavors;
            viewData.Publishers = lstPublishers;
            viewData.ContentTypes = lstContentTypes;
            viewData.ContentCategories = categories;
        }

        #endregion
    }
}
