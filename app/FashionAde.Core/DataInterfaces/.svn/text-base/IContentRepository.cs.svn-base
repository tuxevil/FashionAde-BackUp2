using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IContentRepository : IRepository<Content>
    {
        IList<Content> Search(string text, ContentCategory cc, int userId);
        IList<Content> Search(string text, ContentCategory cc, ContentType ct, int userId);
    }
}
