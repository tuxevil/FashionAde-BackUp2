using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using SharpArch.Data.NHibernate;
using FashionAde.Core.ContentManagement;
using FashionAde.Core;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.Accounts;
using FashionAde.Utils;

namespace FashionAde.Data.Repository
{
    public class ContentPublishedRepository : Repository<ContentPublished>, IContentPublishedRepository
    {
        #region Content Published Methods

        public ContentPublished Get(ContentCategory cc, ContentType ct)
        {
            ICriteria crit = GetCriteria(cc, ct);
            crit.SetMaxResults(1);
            return crit.UniqueResult<ContentPublished>();
        }

        public IList<ContentPublished> List(ContentCategory cc, ContentType ct)
        {
            ICriteria crit = GetCriteria(cc, ct);
            return crit.List<ContentPublished>();
        }

        public IList<ContentPublished> ListByFlavors(ContentCategory cc, IList<FashionFlavor> flavors)
        {
            ICriteria crit = GetCriteria(cc, ContentType.Blog);

            if (flavors.Count > 0)
            {
                Disjunction ds = new Disjunction();
                foreach (FashionFlavor fv in flavors)
                    ds.Add(Restrictions.Eq("Flavor", fv.Id));
                crit.Add(ds);
            }

            return crit.List<ContentPublished>();
        }

        #endregion

        #region Content Section Methods

        public IList<ContentPublishedSection> ListSections(ContentCategory cc, ContentType ct)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ContentPublishedSection));
            ICriteria critContent = crit.CreateCriteria("ContentPublished");
            FilterContentPublishedCriteria(critContent, cc, ct);
            return crit.List<ContentPublishedSection>();
        }

        public IList<ContentPublishedSection> ListSectionsByFlavors(ContentCategory cc, IList<FashionFlavor> flavors)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ContentPublishedSection));
            ICriteria critContent = crit.CreateCriteria("ContentPublished");
            FilterContentPublishedCriteria(critContent, cc, ContentType.Blog);

            if (flavors.Count > 0)
            {
                Disjunction ds = new Disjunction();
                foreach (FashionFlavor fv in flavors)
                    ds.Add(Restrictions.Eq("FashionFlavor", fv));
                crit.Add(ds);
            }

            return crit.List<ContentPublishedSection>();
        }

        #endregion

        #region Criteria Helpers

        private ICriteria GetCriteria(ContentCategory cc, ContentType ct)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ContentPublished));
            FilterContentPublishedCriteria(crit, cc, ct);
            return crit;
        }

        private void FilterContentPublishedCriteria(ICriteria crit, ContentCategory cc, ContentType ct)
        {
            crit.Add(Expression.Eq("Category", cc));
            crit.Add(Expression.Eq("Type", ct));
            crit.Add(Expression.Eq("Status", ContentPublishedStatus.Enabled));

            Conjunction c1 = new Conjunction();
            c1.Add(Expression.Ge("ScheduleFrom", DateTime.Today));
            c1.Add(Expression.IsNull("ScheduleTo"));

            Conjunction c2 = new Conjunction();
            c2.Add(Expression.IsNull("ScheduleFrom"));
            c2.Add(Expression.IsNull("ScheduleTo"));

            Conjunction c3 = new Conjunction();
            c3.Add(Expression.Ge("ScheduleFrom", DateTime.Today));
            c3.Add(Expression.Le("ScheduleTo", DateTime.Today));

            Disjunction d = new Disjunction();
            d.Add(c1);
            d.Add(c2);
            d.Add(c3);

            crit.Add(d);

            crit.AddOrder(new Order("CreatedOn", false));
            crit.AddOrder(new Order("ScheduleFrom", false));
        }

        #endregion
    }
}
