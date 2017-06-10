using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.DataInterfaces;
using SharpArch.Data.NHibernate;
using NHibernate;

namespace FashionAde.Data.Repository
{
    public class BasicUserRepository : Repository<BasicUser>, IBasicUserRepository
    {
        public void MigrateInvited(InvitedUser user, RegisteredUser registeredUser)
        {
            IQuery q = NHibernateSession.Current.CreateSQLQuery("call uspMigrateInvitedUser(" + user.Id + "," + registeredUser.Id + ");");
            q.ExecuteUpdate();
        }

    }
}
