using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.ContentManagement;

namespace FashionAde.Core.DataInterfaces
{
    public interface IContentCategoryRepository : IRepository<ContentCategory>
    {
        IList<ContentCategory> ListByContentType(int typeId);  
    }
}
