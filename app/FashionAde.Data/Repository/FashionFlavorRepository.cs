using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class FashionFlavorRepository : Repository<FashionFlavor>, IFashionFlavorRepository
    {
        public IList<FashionFlavor> GetByIds(IList<int> Ids)
        {
            if (Ids == null)
                return null;
            List<int> ids = new List<int>(Ids);
            ICriteria crit = Session.CreateCriteria(typeof(FashionFlavor));
            crit.Add(Expression.In("Id", ids));
            return crit.List<FashionFlavor>();
        }
    }
}
