using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;
using FashionAde.Core;
using FashionAde.Core.Accounts;

namespace FashionAde.ApplicationServices
{
    public interface IContentService
    {
        ContentPublished Get(int contentId);
        ContentPublished Get(ContentCategory cc, ContentType ct);
        IList<ContentPublished> List(ContentCategory cc, ContentType ct);
        IList<ContentPublishedSection> GetRandomStyleAlerts();
        IList<ContentPublishedSection> GetRandomStyleAlerts(IList<FashionFlavor> flavors);
        IList<ContentPublishedSection> GetRandomStyleAlerts(IList<FashionFlavor> flavors, int quantity);
    }
}
