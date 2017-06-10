using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class WishListRepository : Repository<WishList>, IWishListRepository
    {
        public WishList GetForUser(RegisteredUser user)
        {
            ICriteria crit = Session.CreateCriteria(typeof (WishList));
            ICriteria wishGarments = crit.SetFetchMode("Garments", FetchMode.Join);
            crit.Add(Expression.Eq("User", user));
            return crit.UniqueResult<WishList>();
        }
    }
}
