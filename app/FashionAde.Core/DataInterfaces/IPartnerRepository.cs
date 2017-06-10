using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Partner GetByCode(string code);
    }
}
