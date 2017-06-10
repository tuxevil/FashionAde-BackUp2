using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.Accounts;

namespace FashionAde.Core.DataInterfaces
{
    public interface IContentPublishedRepository : IRepository<ContentPublished>
    {
        ContentPublished Get(ContentCategory cc, ContentType ct);

        IList<ContentPublished> List(ContentCategory cc, ContentType ct);
        IList<ContentPublished> ListByFlavors(ContentCategory cc, IList<FashionFlavor> flavors);

        IList<ContentPublishedSection> ListSections(ContentCategory cc, ContentType ct);
        IList<ContentPublishedSection> ListSectionsByFlavors(ContentCategory cc, IList<FashionFlavor> flavors);
    }
}
