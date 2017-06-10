using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.FlavorSelection;
using System.IO;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class InternalPromotion
    {
        private string headline;
        private string link;
        
        public string Headline
        {
            get { return headline; }
            set { headline = value; }        
        }

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

    }
}


