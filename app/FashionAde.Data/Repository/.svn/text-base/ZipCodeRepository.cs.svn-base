using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class ZipCodeRepository : Repository<ZipCode>, IZipCodeRepository
    {
        public ZipCode GetByCode(string code)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ZipCode));
            crit.Add(Expression.Eq("Code", code));
            return crit.UniqueResult<ZipCode>();
        }
    }
}
