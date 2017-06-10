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
using SharpArch.Web.NHibernate;
using System.Collections.Specialized;
using FashionAde.Utils.OutfitEngineService;

namespace FashionAde.Web.Controllers.BuildYourCloser
{
    [HandleError]
    [Authorize]
    public class FlavorChangeController : BuildYourClosetController
    {
        private IRegisteredUserRepository registeredUserRepository;
        private IFashionFlavorRepository fashionFlavorRepository;
        private IClosetRepository closetRepository;
        
        public FlavorChangeController(IRegisteredUserRepository registeredUserRepository, IClosetRepository closetRepository, IFashionFlavorRepository fashionFlavorRepository)
        {
            this.closetRepository = closetRepository;            
            this.fashionFlavorRepository = fashionFlavorRepository;
            this.registeredUserRepository = registeredUserRepository;            
        }

        /// <summary>
        /// Se ejecuta cuando el usuario termino de seleccionar sus Flavors a través del asistente.
        /// </summary>
        /// <param name="Flavor1Id">Id del primer flavor que dio como resultado el Quiz.</param>
        /// <param name="Flavor2Id">Id del segundo flavor que dio como resultado el Quiz.</param>
        /// <returns>Si el asistente devuelve:
        ///             - 1 Flavor:  Actualiza los flavos con el flavor resultante.
        ///             - 2 Flavors: Rediecciona a la página de FlavorWeight.
        /// <returns>
        public ActionResult QuizCompleted(string Flavor1Id, string Flavor2Id)
        {
            List<FashionFlavor> selectedFF = new List<FashionFlavor>();

            selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor1Id)));
            if (!string.IsNullOrEmpty(Flavor2Id))
                selectedFF.Add(fashionFlavorRepository.Get(Convert.ToInt32(Flavor2Id)));

            ClosetState.SetFlavors(selectedFF);

            if (selectedFF.Count < 2)
            {
                IList<UserFlavor> userFlavors = new List<UserFlavor>();
                if (selectedFF != null)
                {
                    userFlavors.Add(new UserFlavor(selectedFF[0], Convert.ToDecimal(100)));
                    ClosetState.SetUserFlavors(userFlavors);
                }

                Session["previousUrl"] = "FlavorSelect";
                return RedirectToAction("UpdateUserFlavors", new { Flavor1Weight = 100 });
            }

            return RedirectToAction("Index", "FlavorWeight");
        
        }

        /// <summary>
        /// Actualiza los UserFlavors del usuario por los nuevos seleccionados. 
        /// </summary>
        /// <param name="Flavor1Weight">Peso del primer Fashion Flavor seleccionado.</param>
        /// <param name="Flavor2Weight">Peso del segudno Fashion Falvor determinado.</param>
        /// <param name="values">Coleccion de objetos del formulario</param>
        /// <returns>Actualiza los flavors seleccionados y redirecciona a la Home.</returns>
        public ActionResult UpdateUserFlavors(string Flavor1Weight, string Flavor2Weight, string submit)
        {
            bool previous = (submit != null && submit.ToLower() == "previous");
            if (previous) 
                return RedirectToAction("Index", "FlavorSelect");

            IList<FashionFlavor> flavors = ClosetState.Flavors;
            IList<UserFlavor> userFlavors = new List<UserFlavor>();
            if (flavors != null)
            {
                if (!string.IsNullOrEmpty(Flavor1Weight))
                    userFlavors.Add(new UserFlavor(flavors[0], Convert.ToDecimal(Flavor1Weight)));
                if (!string.IsNullOrEmpty(Flavor2Weight))
                    userFlavors.Add(new UserFlavor(flavors[1], Convert.ToDecimal(Flavor2Weight)));
            }

            RegisteredUser user = registeredUserRepository.Get(this.UserId);
            
            List<int> flavorsIds = new List<int>();
            List<int> myGarmentsIds = new List<int>();

            foreach (FashionFlavor ff in flavors)
                flavorsIds.Add(ff.Id);

            IList<ClosetGarment> closetGarments = closetRepository.GetGarmentsForUser(user);
            foreach (ClosetGarment closetGarment in closetGarments)
                myGarmentsIds.Add(closetGarment.Garment.Id);

            if (!new OutfitEngineServiceClient().HasValidCombinations(myGarmentsIds, flavorsIds))
                return RedirectToAction("FlavorChangeResult", "FlavorSelect", new { flavorsChanged = false });

            user.SetFlavors(userFlavors);

            //Update UserFlavors
            registeredUserRepository.DbContext.BeginTransaction();
            registeredUserRepository.SaveOrUpdate(user);
            registeredUserRepository.DbContext.CommitTransaction();

            new OutfitEngineServiceClient().CreateOutfits(user.Closet.Id);
            UserDataHelper.LoadFromDatabase();

            return RedirectToAction("FlavorChangeResult", "FlavorSelect", new { flavorsChanged = true });
            
        }
    }
}