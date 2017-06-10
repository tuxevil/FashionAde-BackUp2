using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class Pager
    {
        private int totalPages;
        private List<SelectListItem> pages = new List<SelectListItem>();

        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        public List<SelectListItem> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
    }
}
