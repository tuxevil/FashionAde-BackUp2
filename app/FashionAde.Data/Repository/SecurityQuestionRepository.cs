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
    public class SecurityQuestionRepository : Repository<SecurityQuestion>, ISecurityQuestionRepository 
    {
        public SecurityQuestion GetByDescription(string description)
        {
            ICriteria crit = Session.CreateCriteria(typeof(SecurityQuestion));
            crit.Add(Expression.Eq("Description", description));
            return crit.UniqueResult<SecurityQuestion>();
        }

    }
}
