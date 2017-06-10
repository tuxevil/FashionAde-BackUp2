using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class OutfitSearch
    {
        private string sortBy;
        private string search;
        private string page;
        private string season;

        public virtual string SortBy
        {
            get { return sortBy; }
            set { sortBy = value; }
        }

        public virtual string Search
        {
            get { return search; }
            set { search = value; }
        }

        public virtual string Page
        {
            get { return page; }
            set { page = value; }
        }

        public virtual string Season
        {
            get { return season; }
            set { season = value; }
        }
    }
}
