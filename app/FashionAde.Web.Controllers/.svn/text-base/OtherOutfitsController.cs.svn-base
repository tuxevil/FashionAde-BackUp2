using System.Web.Mvc;
using System.Web.Security;
using FashionAde.ApplicationServices;
using FashionAde.Core;
using System.Collections.Generic;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Services;
using System.Linq;
using FashionAde.Core.Accounts;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class OtherOutfitsController : BaseController
    {
        private IContentService contentService;        
        private IClosetRepository closetRepository;        
        private IFashionFlavorRepository fashionFlavorRepository;
        private IRegisteredUserRepository registeredUserRepository;

        public OtherOutfitsController(IRegisteredUserRepository registeredUserRepository, IContentService contentService, IClosetRepository closetRepository, IFashionFlavorRepository fashionFlavorRepository)
        {
            this.contentService = contentService;
            this.closetRepository = closetRepository;
            this.fashionFlavorRepository = fashionFlavorRepository;
            this.registeredUserRepository = registeredUserRepository;
        }

        /// <summary>
        /// Muestra la pantalla principal
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]  
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        /// <summary>
        /// Busca los closet especificados.
        /// </summary>
        /// <param name="searchText">Texto a buscar. Puede ser el nombre de usuario o el Zip Code.</param>
        /// <param name="styleType">Opcional. Id del Flavor a buscar.</param>
        /// <param name="pageNumber">Número de página actual.</param>
        /// <returns>Una colección de los closets correspondientes al criterio de búsqueda.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string searchText, int? styleType, int pageNumber)
        {
            LoadData(searchText, styleType, pageNumber);
            return View();
        }

        #region Load Data

        private void LoadData()
        {
            LoadData(string.Empty, null, 1);
        }

        private void LoadData(string searchText, int? styleType, int pageNumber)
        {
            int totalCount;

            LoadFlavors();

            IList<Closet> lstClosets = closetRepository.Search(this.UserId, searchText, styleType, 10, pageNumber, out totalCount);

            ViewData["closets"] = lstClosets;
            ViewData["totalClosets"] = totalCount;
            ViewData["styleAlerts"] = contentService.GetRandomStyleAlerts();
            ViewData["Pages"] = Common.Paging(totalCount, 1, 10, 6);
        }

        /// <summary>
        /// Carga el combo de Fashion Flavors.
        /// </summary>
        private void LoadFlavors()
        {
            List<SelectListItem> lstItems = new List<SelectListItem>();
            IList<FashionFlavor> lstFlavors = fashionFlavorRepository.GetAll();
            
            foreach (FashionFlavor ff in  lstFlavors)
            {
                SelectListItem item = new SelectListItem();
                item.Text = ff.Name;
                item.Value = ff.Id.ToString();
                lstItems.Add(item);
            }
            ViewData["FilterBy"] = lstItems;
        }

        #endregion
    }
}
