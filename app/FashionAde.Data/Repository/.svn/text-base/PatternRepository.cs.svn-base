using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class PatternRepository : Repository<Pattern>, IPatternRepository
    {
        public Pattern GetSolid()
        {
            ICriteria crit = Session.CreateCriteria(typeof(Pattern));
            crit.Add(Expression.Eq("Description", "Solid"));
            return crit.UniqueResult<Pattern>();
        }

        public override IList<Pattern>  GetAll()
        {
            ICriteria crit = Session.CreateCriteria(typeof(Pattern));
            crit.AddOrder(new Order("Description", true));
            return crit.List<Pattern>();
        }
    }
}
