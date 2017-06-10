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

namespace FashionAde.Data.Repository
{
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        #region IContentRepository Members

        private bool IsNumber(string text)
        {
            Array chars = text.ToCharArray();
            foreach (char c in chars)
                if (!char.IsNumber(c))
                    return false;

            return true;
        }

        public IList<Content> Search(string text, ContentCategory cc, int userId)
        {
            ICriteria crit = GetSearchCriteria(text, cc, userId);
            return crit.List<Content>();
        }

        public IList<Content> Search(string text, ContentCategory cc, ContentType ct, int userId)
        {
            ICriteria crit = GetSearchCriteria(text, cc, userId);
            crit.Add(Expression.Eq("Type", ct));
            return crit.List<Content>();
        }

        private ICriteria GetSearchCriteria(string text, ContentCategory cc, int userId)
        {
            ICriteria crit = Session.CreateCriteria(typeof(Content));
            
            if (cc != null)
                crit.Add(Expression.Eq("Category", cc));

            if (userId != 0)
            {
                Disjunction d = new Disjunction();
                d.Add(Restrictions.Eq("AssignedTo", userId));
                d.Add(Restrictions.Eq("EditedBy", userId));

                // Allow to see published records for any user.
                d.Add(Restrictions.Eq("Status", ContentStatus.Published));
                crit.Add(d);
            }
            
            if (!string.IsNullOrEmpty(text))
            {
                if (IsNumber(text))
                    crit.Add(Expression.Eq("Id", Convert.ToInt32(text)));
                else
                    crit.Add(Expression.InsensitiveLike("Title", text, MatchMode.Anywhere));
            }

            crit.Add(Restrictions.Not(Restrictions.Eq("Status", ContentStatus.Disabled)));

            crit.AddOrder(new Order("Status", true));
            crit.AddOrder(new Order("CreatedOn", false));
            return crit;
        }

        #endregion
    }
}
