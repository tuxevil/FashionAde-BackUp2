using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class SilouhetteRepository : Repository<Silouhette>, ISilouhetteRepository
    {
        public IList<Silouhette> GetByFlavors(IList<FashionFlavor> flavors, IList<EventType> types)
        {
            string flavorids = "";
            string eventtypeids = "";
            foreach (FashionFlavor fashionFlavor in flavors)
                flavorids += fashionFlavor.Id + ",";
            flavorids = flavorids.TrimEnd(',');

            foreach (EventType eventType in types)
                eventtypeids += eventType.Id + ",";
            eventtypeids = eventtypeids.TrimEnd(',');

            string query = "select distinct S from Silouhette S join S.FashionFlavors FF join S.EventTypes ET where FF.Id in(" + flavorids + ") and ET.Id in (" + eventtypeids + ") order by S.Category";
            IQuery q = NHibernateSession.Current.CreateQuery(query);

            return q.List<Silouhette>();
        }
    }
}
