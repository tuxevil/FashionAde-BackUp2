using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public override IList<Color>  GetAll()
        {
            ICriteria crit = Session.CreateCriteria(typeof(Color));
            crit.AddOrder(new Order("Description", true));
            return crit.List<Color>();            
        }
    }
}
