using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        public IList<Friend> Search(RegisteredUser user, string friend, FriendStatus status, int limit)
        {
            ICriteria crit = NHibernateSession.Current.CreateCriteria(typeof(Friend));
            crit.SetFetchMode("User", FetchMode.Join);
            crit.Add(Restrictions.Eq("BasicUser.Id", user.Id));

            crit.Add(Expression.Not(Expression.Eq("Status", FriendStatus.Denied)));

            if(status != FriendStatus.All)
                crit.Add(Expression.Eq("Status", status));

            Disjunction d = new Disjunction();
            if (!string.IsNullOrEmpty(friend))
            {
                ICriteria critUser = crit.CreateCriteria("User");
                d.Add(Expression.Like("FirstName", friend, MatchMode.Anywhere));
                d.Add(Expression.Like("LastName", friend, MatchMode.Anywhere));
                d.Add(Expression.Like("EmailAddress", friend, MatchMode.Anywhere));
                critUser.Add(d);
            }

            if (limit > 0)
                crit.SetMaxResults(limit);

            return crit.List<Friend>();
        }

        public Friend GetInverse(int friendId)
        {
            Friend f = this.Get(friendId);
            if (f == null)
                return null;

            ICriteria crit = NHibernateSession.Current.CreateCriteria(typeof(Friend));
            crit.Add(Restrictions.Eq("BasicUser.Id", f.User.Id));
            crit.Add(Restrictions.Eq("User.Id", f.BasicUser.Id));
            crit.SetMaxResults(1);
            return crit.UniqueResult<Friend>();
        }
    }
}
