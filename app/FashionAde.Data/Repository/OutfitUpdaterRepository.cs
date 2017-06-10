using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class OutfitUpdaterRepository : Repository<OutfitUpdater>, IOutfitUpdaterRepository
    {
        public OutfitUpdater Get(string externalId)
        {
            ICriteria crit = Session.CreateCriteria(typeof(OutfitUpdater));
            crit.Add(Expression.Eq("ExternalId", externalId));
            return crit.UniqueResult<OutfitUpdater>();
        }

        public IList<OutfitUpdater> GetValidsFor(Partner partner)
        {
            ICriteria crit = Session.CreateCriteria(typeof(OutfitUpdater));
            crit.Add(Expression.Eq("Partner.Id", partner.Id));
            crit.Add(Expression.Eq("Status", OutfitUpdaterStatus.Valid));
            return crit.List<OutfitUpdater>();
        }

        public IList<OutfitUpdater> GetFor(Partner partner)
        {
            ICriteria crit = Session.CreateCriteria(typeof(OutfitUpdater));
            crit.Add(Expression.Eq("Partner.Id", partner.Id));
            return crit.List<OutfitUpdater>();
        }

        public IList<OutfitUpdater> GetFor(PreCombination preCombination, int pageNumber, int pageSize, out int totalCount)
        {
            ICriteria crit = Session.CreateCriteria(typeof(OutfitUpdater)).CreateCriteria("PreCombinations");
            crit.Add(Expression.Eq("Id", preCombination.Id));
            totalCount = crit.SetProjection( Projections.ProjectionList().Add(Projections.Count("Id"))).UniqueResult<int>();

            crit = Session.CreateCriteria(typeof(OutfitUpdater)).CreateCriteria("PreCombinations");
            crit.Add(Expression.Eq("Id", preCombination.Id));
            if(pageNumber != 0 && pageSize != 0)
            {
                crit.SetMaxResults(pageSize);
                crit.SetFirstResult((pageNumber - 1) * pageSize);
            }
            return crit.List<OutfitUpdater>();
        }

        public IList<OutfitUpdater> GetOnly(params OutfitUpdaterStatus[] status)
        {
            ICriteria crit = Session.CreateCriteria(typeof(OutfitUpdater));

            Disjunction d = new Disjunction();
            foreach (OutfitUpdaterStatus ous in status)
                d.Add(Expression.Eq("Status", ous));

            crit.Add(d);

            // Fetch everything we can to avoid select on the loops.
            crit.SetFetchMode("Silouhette", FetchMode.Join);
            crit.SetFetchMode("Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("ColorFamily", FetchMode.Join);
            crit.SetFetchMode("ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Pattern", FetchMode.Join);

            return crit.List<OutfitUpdater>();
        }

        public void ProcessOutfitUpdatersByPreCombinationsFile(string fileName)
        {
            string query = "LOAD DATA INFILE '{0}' INTO TABLE outfitupdaterbyprecombinations ";
            query += "FIELDS TERMINATED BY ','";
            query += "LINES TERMINATED BY '\r\n' STARTING BY '' ";
            query += "(OutfitUpdaterId, PreCombinationId);";

            query = string.Format(query, fileName.Replace(@"\", @"\\"));

            IQuery q = NHibernateSession.Current.CreateSQLQuery(query);
            q.ExecuteUpdate();
        }

        public void ChangeOutfitUpdatersStatus()
        {
            string query = "call uspUpdateOutfitUpdaterStatus();";
            IQuery q = NHibernateSession.Current.CreateSQLQuery(query);
            q.ExecuteUpdate();
        }

        public OutfitUpdater GetRandomOutfitUpdaterFor(PreCombination preCombination)
        {
            string query = string.Format("SELECT outfitupdaterid FROM outfitupdaters WHERE status > 0 and outfitupdaterid >= (SELECT FLOOR( MAX(outfitupdaterid) * RAND()) FROM outfitupdaterbyprecombinations where precombinationid = {0} ) ORDER BY outfitupdaterid LIMIT 1;", preCombination.Id);
            IQuery q = Session.CreateSQLQuery(query);
            return new OutfitUpdaterRepository().Get(q.UniqueResult<int>());
        }
    }
}
