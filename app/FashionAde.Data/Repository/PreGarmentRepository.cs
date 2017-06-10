using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.OutfitEngine;
using SharpArch.Data.NHibernate;
using NHibernate;

namespace FashionAde.Data.Repository
{
    public class PreGarmentRepository : Repository<PreGarment>, IPreGarmentRepository 
    {
        public IList<PreGarment> GetFetched()
        {
            ICriteria crit = Session.CreateCriteria(typeof(PreGarment)).SetFetchMode("PreSilouhette", NHibernate.FetchMode.Join);
            return crit.List<PreGarment>();
        }
    }
}
