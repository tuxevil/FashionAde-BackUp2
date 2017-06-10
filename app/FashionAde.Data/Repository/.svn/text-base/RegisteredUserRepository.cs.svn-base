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
    public class RegisteredUserRepository : Repository<RegisteredUser>, IRegisteredUserRepository
    {
        public RegisteredUser GetByMembershipId(int id)
        {
            ICriteria crit = Session.CreateCriteria(typeof(RegisteredUser));
            crit.Add(Expression.Eq("MembershipUserId", id));
            return crit.UniqueResult<RegisteredUser>();
        }

        public RegisteredUser GetByMail(string email)
        {
            ICriteria crit = Session.CreateCriteria(typeof(RegisteredUser));
            crit.Add(Expression.Eq("EmailAddress", email));
            return crit.UniqueResult<RegisteredUser>();
        }        
    }
}
