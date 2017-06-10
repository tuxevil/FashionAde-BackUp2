using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.ApplicationServices;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using FashionAde.Web.Controllers;

namespace FashionAde.Web.Controllers
{
    public class OutfitUpdatersController : BaseController
    {
        private IOutfitUpdaterRepository outfitUpdaterRepository;
        private IContentService contentService;

        public OutfitUpdatersController(IOutfitUpdaterRepository outfitUpdaterRepository, IContentService contentService)
        {
            this.outfitUpdaterRepository = outfitUpdaterRepository;
            this.contentService = contentService;
        }

        [AcceptVerbs(HttpVerbs.Get)]        
        public ActionResult Index(int id)
        {
            return View(GetList(id, 1));
        }

        private IList<OutfitUpdater> GetList(int id, int page)
        {
            ViewData["styleAlerts"] = contentService.GetRandomStyleAlerts();
            int totalCount;
            IList<OutfitUpdater> lst = outfitUpdaterRepository.GetFor(new PreCombination(id), page, 24, out totalCount);
            ViewData["Pages"] = Common.Paging(totalCount, page, 24, 6);
            ViewData["Id"] = id;
            return lst;
        }

        [AcceptVerbs(HttpVerbs.Post)]        
        public ActionResult ChangePage(int Id, int Page)
        {
            return View("Index", GetList(Id, Page));
        }
    }
}
