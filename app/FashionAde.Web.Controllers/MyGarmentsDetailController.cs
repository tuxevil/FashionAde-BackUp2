using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FashionAde.Utils;
using FashionAde.Web.Controllers.MVCInteraction;
using System.Web.Mvc;
using FashionAde.Core;
using SharpArch.Web.NHibernate;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Common;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    [Authorize]
    public class MyGarmentsDetailController : BaseController
    {
        private IClosetRepository closetRepository;

        public MyGarmentsDetailController(IClosetRepository closetRepository)
        {
            this.closetRepository = closetRepository;
        }

        [ObjectFilter(Param = "garmentSelected", RootType = typeof(int))]
        public ActionResult GetClosetGarmentDetails(int garmentSelected)
        {
            ClosetGarment garment = closetRepository.GetClosetGarment(garmentSelected);
            if (garment.Details == null)
            {
                return Json(new
                {
                    PurchasedAt = string.Empty,
                    PurchasedOn = string.Empty,
                    MadeBy = string.Empty,
                    MadeOf = string.Empty,
                    IsTailored = string.Empty,
                    CareConditions = string.Empty,
                    StoreConditions = string.Empty
                });
            }
            string date = "";
            if (garment.Details.PurchasedOn != null)
                date = Convert.ToDateTime(garment.Details.PurchasedOn).ToShortDateString();
            return Json(new
            {
                PurchasedAt = garment.Details.PurchasedAt,
                PurchasedOn = date,
                MadeBy = garment.Details.MadeBy,
                MadeOf = garment.Details.MadeOf,
                IsTailored = garment.Details.IsTailored.ToString(),
                CareConditions = garment.Details.CareConditions,
                StoreConditions = garment.Details.StoreConditions
            });
        }

        [ObjectFilter(Param = "detail", RootType = typeof(WebGarmentDetails))]
        [Transaction]
        public ActionResult SaveDetails(WebGarmentDetails detail)
        {
            ClosetGarment garment = closetRepository.GetClosetGarment(detail.ClosetGarmentId);
            if (garment.Details == null)
                garment.Details = new GarmentDetails();
            
            garment.Details.CareConditions = detail.CareConditions;
            garment.Details.IsTailored = false;
            if (detail.IsTailored == "True")
                garment.Details.IsTailored = true;
            garment.Details.MadeBy = detail.MadeBy;
            garment.Details.MadeOf = detail.MadeOf;
            garment.Details.PurchasedAt = detail.PurchasedAt;
            if (StringHelper.IsDateTime(detail.PurchasedOn))
            {
                IFormatProvider formatProvider = new CultureInfo("en-US");
                garment.Details.PurchasedOn = Convert.ToDateTime(detail.PurchasedOn, formatProvider);
            }
            garment.Details.StoreConditions = detail.StoreConditions;

            closetRepository.SaveClosetGarment(garment);

            return Json(new { Success = true });
        }
    }
}
