using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class EventTypeRepository : Repository<EventType>, IEventTypeRepository
    {
        public override IList<EventType> GetAll()
        {
            ICriteria crit = Session.CreateCriteria(typeof(EventType));
            crit.AddOrder(new Order("Description", true));
            return crit.List<EventType>();
        }

        public IList<EventType> GetByIds(IList<int> eventTypeIds)
        {
            ICriteria crit = Session.CreateCriteria(typeof(EventType));
            Disjunction d = new Disjunction();
            foreach (int id in eventTypeIds)
                d.Add(Restrictions.Eq("Id", id));
            crit.Add(d);
            crit.AddOrder(new Order("Description", true));
            return crit.List<EventType>();
        }

        public IList<EventType> GetByCode(IList<int> eventTypeCodes)
        {
            ICriteria crit = Session.CreateCriteria(typeof(EventType));
            Disjunction d = new Disjunction();
            foreach (int id in eventTypeCodes)
                d.Add(Restrictions.Eq("BinaryNumber", id));
            crit.Add(d);
            crit.AddOrder(new Order("Id", true));
            return crit.List<EventType>();
        }
    }
}
