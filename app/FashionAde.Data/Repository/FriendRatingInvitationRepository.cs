using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.UserCloset;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class FriendRatingInvitationRepository : Repository<FriendRatingInvitation>, IFriendRatingInvitationRepository
    {
        public FriendRatingInvitation GetByKey(string key)
        {
            ICriteria crit = Session.CreateCriteria(typeof(FriendRatingInvitation));
            crit.Add(Expression.Eq("KeyCode", key));
            return crit.UniqueResult<FriendRatingInvitation>();
        }
    }
}
