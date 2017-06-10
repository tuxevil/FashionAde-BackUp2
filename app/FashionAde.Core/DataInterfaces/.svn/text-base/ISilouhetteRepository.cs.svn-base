using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface ISilouhetteRepository : IRepository<Silouhette>
    {
        IList<Silouhette> GetByFlavors(IList<FashionFlavor> flavors, IList<EventType> types);
    }
}
