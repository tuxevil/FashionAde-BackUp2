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
    public class FriendProviderRepository : Repository<FriendProvider>, IFriendProviderRepository 
    {
        public FriendProvider GetByName(string name)
        {
            ICriteria crit = NHibernateSession.Current.CreateCriteria(typeof (FriendProvider));
            crit.Add(Expression.Eq("Name", name));
            return crit.UniqueResult<FriendProvider>();
        }
    }
}
