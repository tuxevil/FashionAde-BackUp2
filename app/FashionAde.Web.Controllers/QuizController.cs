using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.FlavorSelection;
using FashionAde.Web.Controllers.MVCInteraction;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class QuizController : BaseController
    {
        private IStylePhotographRepository stylePhotographRepository;
        private IBrandSetRepository brandSetRepository;
        private IWordingRepository wordingRepository;
        private IFlavorSelectionService flavorSelectionService;

        public QuizController(IFashionFlavorRepository fashionFlavorRepository, IStylePhotographRepository stylePhotographRepository, IBrandSetRepository brandSetRepository, IWordingRepository wordingRepository, IFlavorSelectionService flavorSelectionService)
        {
            this.stylePhotographRepository = stylePhotographRepository;
            this.brandSetRepository = brandSetRepository;
            this.wordingRepository = wordingRepository;
            this.flavorSelectionService = flavorSelectionService;
        }

        public IList<FashionFlavor> DetermineFlavors(List<int> stylePhotographList, List<int> brandSetList, List<int> wordingList)
        {
            foreach (int id in stylePhotographList)
                flavorSelectionService.StylePhotographs.Add(stylePhotographRepository.Get(id));

            foreach (int id in brandSetList)
                flavorSelectionService.BrandSets.Add(brandSetRepository.Get(id));

            foreach (int id in wordingList)
                flavorSelectionService.Wordings.Add(wordingRepository.Get(id));
            
            return flavorSelectionService.DetermineFlavors();
        }
        
        public IList<StylePhotograph> StylePhotographs
        {
            get { return stylePhotographRepository.GetAll(); }
        }

        public IList<BrandSet> BrandSets
        {
            get { return brandSetRepository.GetAll(); }
        }

        public IList<Wording> Wordings
        {
            get { return wordingRepository.GetAll(); }
        }
    }
}
