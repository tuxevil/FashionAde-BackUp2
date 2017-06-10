using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;
using NHibernate.Validator.Constraints;

namespace FashionAde.Core.ContentManagement
{
    public class ContentSectionBase : Entity
    {
        [NotNull]
        public virtual FashionFlavor FashionFlavor { get; set; }

        [NotNullNotEmpty]
        public virtual string Title { get; set; }

        [NotNullNotEmpty]
        public virtual string Body { get; set; }

        public virtual string Summary { get; set; }
    }

    public class ContentSection : ContentSectionBase
    {
        public virtual Content Content { get; set; }
    }

    public class ContentPublishedSection : ContentSectionBase
    {
        public virtual ContentPublished ContentPublished { get; set; }
    }

}
