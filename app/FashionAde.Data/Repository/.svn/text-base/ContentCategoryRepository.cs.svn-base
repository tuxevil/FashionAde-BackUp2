using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using SharpArch.Data.NHibernate;
using FashionAde.Core.ContentManagement;
using NHibernate;
using NHibernate.Criterion;

namespace FashionAde.Data.Repository
{
    public class ContentCategoryRepository : Repository<ContentCategory>, IContentCategoryRepository
    {
        public IList<ContentCategory> ListByContentType(int typeId)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ContentCategory));
            crit.Add(Expression.Eq("Type",(ContentType)typeId)) ;
            return crit.List<ContentCategory>();
        }
    }
}
