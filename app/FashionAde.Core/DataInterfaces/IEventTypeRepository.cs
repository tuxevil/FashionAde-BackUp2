using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IEventTypeRepository : IRepository<EventType>
    {
        IList<EventType> GetByIds(IList<int> eventTypeIds);
        IList<EventType> GetByCode(IList<int> eventTypeCodes);
    }
}
