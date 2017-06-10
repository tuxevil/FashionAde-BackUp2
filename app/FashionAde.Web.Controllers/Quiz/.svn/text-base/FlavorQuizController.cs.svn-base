using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Services;
using FashionAde.Web.Common;
using FashionAde.Web.Controllers.MVCInteraction;

namespace FashionAde.Web.Controllers.Quiz
{
    public class FlavorQuizController : BuildYourClosetController
    {
        private IFashionFlavorRepository fashionFlavorRepository;
        private IStylePhotographRepository stylePhotographRepository;
        private IBrandSetRepository brandSetRepository;
        private IWordingRepository wordingRepository;

        public FlavorQuizController(IFashionFlavorRepository repository, IStylePhotographRepository stylePhotographRepository, IBrandSetRepository brandSetRepository, IWordingRepository wordingRepository)
        {
            this.fashionFlavorRepository = repository;
            this.stylePhotographRepository = stylePhotographRepository;
            this.brandSetRepository = brandSetRepository;
            this.wordingRepository = wordingRepository;
        }

        [ObjectFilter(Param = "selection", RootType = typeof(Selection))]
        public ActionResult GetResult(Selection selection)
        {
            //Convert the object from the page in a string array
            string[] selectedCheckBoxes = selection.Ids;

            List<int> stylePhotographList = new List<int>();
            List<int> brandSetList = new List<int>();
            List<int> wordingList = new List<int>();
            //Work on every checkbox that the page sent to us
            for (int i = 0; i < selectedCheckBoxes.Length; i++)
            {
                //Clean the string
                selectedCheckBoxes[i] = selectedCheckBoxes[i].Replace("chb_", string.Empty);
                //Split the name of the checkbox to know wich type it is and its Id
                string[] temp = selectedCheckBoxes[i].Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //For each type we act in different way
                switch (temp[0])
                {
                    //For StylePhotograph
                    case "SP":
                        //We get the fashionflavor of the StylePhotograph selected
                        stylePhotographList.Add(Convert.ToInt32(temp[1]));
                        break;
                    //For BrandSet
                    case "BS":
                        //We get the fashionflavor of the BrandSet selected
                        brandSetList.Add(Convert.ToInt32(temp[1]));
                        break;
                    //For Wording
                    case "W":
                        //We get the fashionflavor of the BrandSet selected
                        wordingList.Add(Convert.ToInt32(temp[1]));
                        break;
                }
            }

            QuizController qc = new QuizController(fashionFlavorRepository, stylePhotographRepository, brandSetRepository, wordingRepository, new FlavorSelectionService());
            List<FashionFlavor> result = qc.DetermineFlavors(stylePhotographList, brandSetList, wordingList) as List<FashionFlavor>;

            if (result.Count == 2)
                return Json(new
                {
                    Single = false,
                    FashionFlavor1Id = result[0].Id,
                    FashionFlavor1Name = result[0].Name,
                    FashionFlavor1Result = 50,
                    FashionFlavor2Id = result[1].Id,
                    FashionFlavor2Name = result[1].Name,
                    FashionFlavor2Result = 50
                });
            return Json(new
            {
                Single = true,
                FashionFlavor1Id = result[0].Id,
                FashionFlavor1Name = result[0].Name,
                FashionFlavor1Result = 50
            });
        }

        public RedirectToRouteResult ContinueBuilding(string Flavor1Id, string Flavor2Id)
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
                return RedirectToAction("EventTypeSelector");
            }

            return RedirectToAction("Index", "FlavorWeight");
        }
        
    }
}
