using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;
using FashionAde.Core;
using System.Web.Mvc;
using NHibernate.Validator.Constraints;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.DomainModel;
using FashionAde.Web.Common;

namespace FashionAde.WebAdmin.Controllers.CMS
{
    public class EditorViewData
    {
        public ContentView Content { get; set; }
        public IList<FashionFlavor> Flavors { get; set; }
        public IList<SelectListItem> ContentTypes { get; set; }
        public IList<ContentCategory> ContentCategories { get; set; }
        public IList<SelectListItem> Publishers { get; set; }
        public bool CanApprove {get; set;}
        public bool CanAssign {get; set;}
    }

    public class ContentView : ValidatableView
    {
        public int Id { get; set; }

        [NotNullNotEmpty]
        [Length(Max = 90)]
        public string Title { get; set; }

        [NotNullNotEmpty]
        public string Body { get; set; }

        [Length(Max = 140)]
        public string PromotionalText { get; set; }

        [Length(Max = 140)]
        public string Keywords { get; set; }

        [NotNull]
        public int? Category { get; set; }

        [NotNull]
        public int? Type { get; set; }

        public ContentStatus Status { get; set; }

        [NotNull]
        private IList<ContentViewSection> _sections = new List<ContentViewSection>();
        public IList<ContentViewSection> Sections
        {
            get { return _sections; }
            set { _sections = value; } 
        }
    }

    public class ContentViewSection : ValidatableView
    {
        public int Id {get; set;}

        [NotNullNotEmpty]
        [Length(Max=128)]
        public string Title { get; set; }

        [NotNullNotEmpty]
        [Length(Max = 3000)]
        public string Body { get; set; }
        
        [NotNull]
        public FashionFlavor FashionFlavor { get; set; }
    }
}
