using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.Accounts;

namespace FashionAde.Core.DataInterfaces
{
    public interface IUserGarmentRepository : IRepository<UserGarment>
    {
        List<WebClosetGarment> GetWebClosetGarments(RegisteredUser registeredUser);
        IList<UserGarment> GetRecentlyUploaded(RegisteredUser registeredUser);  
    }
}
