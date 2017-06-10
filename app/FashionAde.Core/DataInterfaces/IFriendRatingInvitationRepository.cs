using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.UserCloset;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IFriendRatingInvitationRepository : IRepository<FriendRatingInvitation>
    {
        FriendRatingInvitation GetByKey(string key);
    }
}
