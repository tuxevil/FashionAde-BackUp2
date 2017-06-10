using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IFabricRepository : IRepository<Fabric>
    {
        Fabric GetGeneric();
        IList<Fabric> GetForSilouhette(Silouhette silouhette, IList<EventType> eventTypes);
    }
}
