using System.Collections.Generic;
using FashionAde.Core.Accounts;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IUserSizeRepository : IRepository<UserSize>
    {
        IList<UserSize> ListInOrder();
    }
}