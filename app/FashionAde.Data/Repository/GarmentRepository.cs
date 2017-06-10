using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using FashionAde.Core.OutfitCombination;
using System.Collections;
using NHibernate.Mapping;
using FashionAde.Core.MVCInteraction;

namespace FashionAde.Data.Repository
{
    public class GarmentRepository : Repository<Garment>, IGarmentRepository
    {
        private IFabricRepository fabricRepository;
        public Garment Find(Category category, Silouhette silouhette, Color color, Pattern n)
        {
            throw new NotImplementedException();
        }

        public IList<Garment> GetBySilouhette(Silouhette silouhette)
        {
            ICriteria crit = Session.CreateCriteria(typeof(Garment)).CreateCriteria("Tags");
            ICriteria silouhettes = crit.CreateCriteria("Silouhette");
            silouhettes.Add(Expression.Eq("Id", silouhette.Id));
            return crit.List<Garment>();
        }

        public IList<Garment> GetBySelection(Silouhette silouhette, Fabric fabric, Pattern pattern, IList<EventType> selectedET, IList<ClosetGarment> closetGarments, IList<WishGarment> wishGarments)
        {
            fabricRepository = new FabricRepository();
            if (fabric == null || fabric.Id == 0)
                fabric = fabricRepository.GetForSilouhette(silouhette, selectedET)[0];
            if (pattern == null || pattern.Id == 0)
                pattern = silouhette.AvailablePatterns[0];
            List<int> eventTypesIds = new List<int>();
            foreach (EventType eventType in selectedET)
                eventTypesIds.Add(eventType.Id);

            ICriteria crit = Session.CreateCriteria(typeof(Garment));
                            
            ICriteria tags = crit.CreateCriteria("Tags");            
            ICriteria silouhettes = tags.CreateCriteria("Silouhette");
            silouhettes.Add(Expression.Eq("Id", silouhette.Id));
            
            ICriteria fabrics = tags.CreateCriteria("Fabric");
            fabrics.Add(Expression.Eq("Id", fabric.Id));
        
            ICriteria patterns = tags.CreateCriteria("Pattern");
            patterns.Add(Expression.Eq("Id", pattern.Id));

            int sum = 0;
            foreach (EventType et in selectedET)
                sum += et.BinaryNumber;

            crit.Add(Restrictions.Gt(Projections.SqlProjection("(this_.EventCode & " + sum + ") as bitWiseResult2", new[] { "bitWiseResult" }, new IType[] { NHibernateUtil.Int32 }), 0));

            foreach (ClosetGarment closetGarment in closetGarments)
                crit.Add(Expression.Not(Expression.Eq("Id", closetGarment.Garment.Id)));

            foreach (WishGarment wishGarment in wishGarments)
                crit.Add(Expression.Not(Expression.Eq("Id", wishGarment.Garment.Id)));
        
            return crit.List<Garment>();
        }


        public IList<LayerResponse> GetLayers(System.Array garmentIds)
        {
            IList lst = new List<LayerResponse>();
            for (int i = 0; i < garmentIds.Length; i++)
			{
			    Garment g = this.Get(int.Parse(garmentIds.GetValue(i).ToString()));
                lst.Add(new LayerResponse(g.Id, g.Tags.Silouhette.Layers[0], g.ImageUri));                 
			}
            return lst as IList<LayerResponse>;
        }

        public IList<Garment> GetForProcess(int closetId)
        {
            ICriteria crit = Session.CreateCriteria(typeof(Garment));
            crit.SetFetchMode("PreGarment", FetchMode.Join);
            crit.SetFetchMode("PreGarment.Silouhette", FetchMode.Join);
            crit.SetFetchMode("Tags.Fabric", FetchMode.Join);
            crit.SetFetchMode("Tags.Pattern", FetchMode.Join);
            crit.SetFetchMode("Tags.Silohuette", FetchMode.Join);
            crit.SetFetchMode("Tags.Silohuette.Structure", FetchMode.Join);
            crit.SetFetchMode("Tags.Silohuette.Shape", FetchMode.Join);
            crit.SetFetchMode("Tags.Silohuette.Layers", FetchMode.Join);
            crit.SetFetchMode("Tags.DefaultColor", FetchMode.Join);
            crit.SetFetchMode("Tags.DefaultColor.Family", FetchMode.Join);
            crit.CreateCriteria("ClosetGarments").Add(Expression.Eq("Closet", new Closet(closetId)));
            return crit.List<Garment>();
        }

        public IList<Garment> GetByIds(IList<int> Ids)
        {
            if(Ids == null)
                return null;
            List<int> ids = new List<int>(Ids);
            ICriteria crit = Session.CreateCriteria(typeof (Garment));
            crit.Add(Expression.In("Id", ids));
            return crit.List<Garment>();
        }
    }
}
