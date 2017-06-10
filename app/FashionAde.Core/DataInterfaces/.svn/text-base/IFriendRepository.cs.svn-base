using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IFriendRepository : IRepository<Friend>
    {
        IList<Friend> Search(RegisteredUser user, string friend, FriendStatus status, int limit);
        Friend GetInverse(int friendId);
    }
}
