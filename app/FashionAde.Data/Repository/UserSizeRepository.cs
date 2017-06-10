using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class UserSizeRepository: Repository<UserSize>, IUserSizeRepository
    {
        public IList<UserSize> ListInOrder()
        {
            throw new NotImplementedException();
        }
    }
}
