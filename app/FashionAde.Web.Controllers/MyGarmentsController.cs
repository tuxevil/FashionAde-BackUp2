using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.UserCloset;
using FashionAde.Web.Controllers.MVCInteraction;
using System.Web;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using SharpArch.Web.NHibernate;
using FashionAde.Web.Common;
using FashionAde.ApplicationServices;
using FashionAde.Core.ContentManagement;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    [Authorize]
    public class MyGarmentsController : BaseController
    {
        private ICategoryRepository categoryRepository;
        private IClosetRepository closetRepository;
        private IContentService contentService;
        
        public MyGarmentsController(ICategoryRepository categoryRepository, IClosetRepository closetRepository, IContentService contentService)
        {            
            this.categoryRepository = categoryRepository;            
            this.closetRepository = closetRepository;
            this.contentService = contentService;
        }

        /// <summary>
        /// Shows the default page for My Garments screen.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            LoadSeasons(); 

            IList<Category> categories = categoryRepository.GetAll();
            foreach (Category category in categories)
                ViewData["Category" + category.Description] = category.Id;

            List<WebClosetGarment> webClosetGarments =  closetRepository.GetWebClosetGarments(this.ProxyLoggedUser);            

            ViewData["pants_jeans"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryPants"] || record.CatId == (int)ViewData["CategoryJeans"])
                {
                    return true;
                }
                return false;
            });

            ViewData["skirts_shorts"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryShorts"] || record.CatId == (int)ViewData["CategorySkirts"])
                {
                    return true;
                }
                return false;
            });

            ViewData["dresses"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryDresses"])
                {
                    return true;
                }
                return false;
            });

            ViewData["jackets"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryJackets"] || record.CatId == (int)ViewData["CategoryCoats"] || record.CatId == (int)ViewData["CategoryCardigan"])
                {
                    return true;
                }
                return false;
            });

            ViewData["tops"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryShirts"])
                {
                    return true;
                }
                return false;
            });

            ViewData["accesories"] = webClosetGarments.FindAll(delegate(WebClosetGarment record)
            {
                if (record.CatId == (int)ViewData["CategoryJewelry"] || record.CatId == (int)ViewData["CategoryBelts"] || record.CatId == (int)ViewData["CategoryShoes"] || record.CatId == (int)ViewData["CategoryBags"])
                {
                    return true;
                }
                return false;
            });

            //ViewData["styleAlerts"] = contentService.GetRandomStyleAlerts((IList<FashionFlavor>)ViewData["fashionFlavors"], 1);
            
            return View();
        }

        /// <summary>
        /// Removes a garment from the closet, and any related outfits.
        /// </summary>
        /// <param name="garmentSelected"></param>
        /// <returns></returns>
        [ObjectFilter(Param = "garmentSelected", RootType = typeof(int))]
        [Transaction]
        public ActionResult RemoveGarmentFromCloset(int garmentSelected)
        {
            Closet closet = closetRepository.Get(this.ClosetId);
            ClosetGarment cg = new List<ClosetGarment>(closet.Garments).Find(e => e.Id.Equals(garmentSelected));
            Garment g = cg.Garment;
            closet.RemoveGarment(cg);
            closetRepository.SaveOrUpdate(closet);
            closetRepository.RemoveGarmentFromCloset(g.Id, closet.Id);
            return Json(new {Success= true});
        }

        /// <summary>
        /// Load Seasons
        /// </summary>
        private void LoadSeasons()
        {
            List<SelectListItem> lstSeasons = new List<SelectListItem>();
            SelectListItem season = new SelectListItem();
            season.Text = Season.Fall.ToString();
            season.Value = ((int)Season.Fall).ToString();
            lstSeasons.Add(season);
            season = new SelectListItem();
            season.Text = Season.Spring.ToString();
            season.Value = ((int)Season.Spring).ToString();
            lstSeasons.Add(season);
            season = new SelectListItem();
            season.Text = Season.Summer.ToString();
            season.Value = ((int)Season.Summer).ToString();
            lstSeasons.Add(season);
            season = new SelectListItem();
            season.Text = Season.Winter.ToString();
            season.Value = ((int)Season.Winter).ToString();
            lstSeasons.Add(season);
            season = new SelectListItem();
            season.Text = Season.All.ToString();
            season.Value = ((int)Season.All).ToString();
            lstSeasons.Add(season);
            ViewData["Seasons"] = lstSeasons;
        }
    }
}
