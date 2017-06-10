using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.OutfitEngine;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;

namespace FashionAde.Data.Repository
{
    public class PreCombinationRepository : Repository<PreCombination>, IPreCombinationRepository
    {
        public PreCombination GetByGarments(IList<Garment> garments, FashionFlavor fashionFlavor)
        {
            ICriteria crit = Session.CreateCriteria(typeof(PreCombination));
            for (int i = 0; i < garments.Count; i++)
            {
                // Make sure it is in any position to avoid duplicates.
                Disjunction dis = new Disjunction();
                for (int j = 0; j < garments.Count; j++)
                    dis.Add(Restrictions.Eq(string.Format("Garment{0}", i + 1), garments[j].PreGarment));
                crit.Add(dis);
            }
            
            crit.SetMaxResults(1);
            PreCombination pc = crit.UniqueResult<PreCombination>();
            return pc;
        }

        public IList<PreCombination> GetFirsts(int maxResults)
        {
            ICriteria crit = Session.CreateCriteria(typeof(PreCombination));
            crit.SetMaxResults(maxResults);
            return crit.List<PreCombination>();
        }

        public IList<PreCombination> GetNews()
        {
            ICriteria crit = Session.CreateCriteria(typeof(PreCombination));
            crit.Add(Expression.Eq("Status", PreCombinationStatus.New));
            return crit.List<PreCombination>();
        }

        public IList<PreCombination> GetNewsFor(Closet closet)
        {
            // We want to get Precombinations
            ICriteria crit = Session.CreateCriteria(typeof(PreCombination));

            // We need to reduce that set with the Precombination of the User Closet
            DetachedCriteria precombinations = DetachedCriteria.For(typeof(ClosetOutfit))
                .SetProjection(Projections.Property("PreCombination.Id"))
                .Add(Restrictions.Eq("Closet", closet));
            crit.Add(Subqueries.PropertyIn("Id", precombinations));

            // Make sure we only get non processed
            crit.Add(Restrictions.Eq("Status", PreCombinationStatus.New));

            // Fetch everything we can to avoid select on the loops.
            crit.SetFetchMode("Garment1", FetchMode.Join);
            crit.SetFetchMode("Garment1.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Garment1.Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Garment1.Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("Garment1.ColorFamily", FetchMode.Join);
            crit.SetFetchMode("Garment1.ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("Garment1.ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("Garment1.ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Garment1.PatternType", FetchMode.Join);

            crit.SetFetchMode("Garment2", FetchMode.Join);
            crit.SetFetchMode("Garment2.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Garment2.Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Garment2.Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("Garment2.ColorFamily", FetchMode.Join);
            crit.SetFetchMode("Garment2.ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("Garment2.ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("Garment2.ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Garment2.PatternType", FetchMode.Join);

            crit.SetFetchMode("Garment3", FetchMode.Join);
            crit.SetFetchMode("Garment3.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Garment3.Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Garment3.Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("Garment3.ColorFamily", FetchMode.Join);
            crit.SetFetchMode("Garment3.ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("Garment3.ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("Garment3.ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Garment3.PatternType", FetchMode.Join);

            crit.SetFetchMode("Garment4", FetchMode.Join);
            crit.SetFetchMode("Garment4.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Garment4.Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Garment4.Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("Garment4.ColorFamily", FetchMode.Join);
            crit.SetFetchMode("Garment4.ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("Garment4.ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("Garment4.ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Garment4.PatternType", FetchMode.Join);

            crit.SetFetchMode("Garment5", FetchMode.Join);
            crit.SetFetchMode("Garment5.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Garment5.Silouhette.Structure", FetchMode.Join);
            crit.SetFetchMode("Garment5.Silouhette.Shape", FetchMode.Join);
            crit.SetFetchMode("Garment5.ColorFamily", FetchMode.Join);
            crit.SetFetchMode("Garment5.ColorFamily.AnalogousFamily", FetchMode.Join);
            crit.SetFetchMode("Garment5.ColorFamily.AnalogousFamily2", FetchMode.Join);
            crit.SetFetchMode("Garment5.ColorFamily.ComplimentaryFamily", FetchMode.Join);
            crit.SetFetchMode("Garment5.PatternType", FetchMode.Join);



            return crit.List<PreCombination>();
        }

        public IList<PreCombination> GetOnly(PreCombinationStatus status)
        {
            ICriteria crit = Session.CreateCriteria(typeof(PreCombination));
            crit.Add(Expression.Eq("Status", status));
            return crit.List<PreCombination>();
        }

        public void ChangePreCombinationsStatus(Closet closet)
        {
            string query;
            if (closet != null)
                query = string.Format("call uspUpdatePrecombinationStatus({0});", closet.Id);
            else
                query = "call uspUpdateAllPrecombinationStatus();";
            IQuery q = NHibernateSession.Current.CreateSQLQuery(query);
            q.ExecuteUpdate();
        }
    }
}
