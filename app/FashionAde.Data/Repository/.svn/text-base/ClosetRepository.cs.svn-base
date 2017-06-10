using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.MVCInteraction;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using System.IO;
using NHibernate.Transform;

namespace FashionAde.Data.Repository
{
    public class ClosetRepository : Repository<Closet>, IClosetRepository
    {
        public List<WebClosetGarment> GetWebClosetGarments(RegisteredUser registeredUser)
        {
            ICriteria crit = Session.CreateCriteria(typeof (ClosetGarment));
            ICriteria closet = crit.CreateCriteria("Closet");
            ICriteria garment = crit.CreateCriteria("Garment", "garment");
            ICriteria category = garment.CreateCriteria("Tags").CreateCriteria("Silouhette").CreateCriteria("Category", "category");
            
            closet.Add(Expression.Eq("Id", registeredUser.Closet.Id));
            crit.SetProjection(Projections.ProjectionList().Add(Projections.Property("Id"))
                 .Add(Projections.Property("garment.Title"))
                 .Add(Projections.Property("garment.ImageUri"))
                 .Add(Projections.Property("category.Id"))
                );

            category.AddOrder(new Order("Id", true));

            crit.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(WebClosetGarment).GetConstructors()[1]));
            return crit.List<WebClosetGarment>() as List<WebClosetGarment>;
        }

        public ClosetGarment GetClosetGarment(int id)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ClosetGarment));
            crit.Add(Expression.Eq("Id", id));
            return crit.UniqueResult<ClosetGarment>();
        }

        public ClosetGarment SaveClosetGarment(ClosetGarment o)
        {
            NHibernateSession.Current.SaveOrUpdate(o);
            return o;
        }

        public IList<ColorFamily> GetColorFamilyList()
        {
            ICriteria crit = Session.CreateCriteria(typeof(ColorFamily));
            return crit.List<ColorFamily>();
        }

        public void GenerateCloset(RegisteredUser user)
        {
            int closetId = user.Closet.Id;
            int flavor1Id = user.UserFlavors[0].Flavor.Id;
            int flavor2Id = 0;
            if(user.UserFlavors.Count > 1)
                flavor2Id = user.UserFlavors[1].Flavor.Id;

            IQuery q = NHibernateSession.Current.CreateSQLQuery("call uspCreateClosetOutfits(" + closetId + "," + flavor1Id + ", " + flavor2Id + ");");
            q.ExecuteUpdate();
        }

        public void CompleteClosetCreation(int closetId)
        {
            string query = string.Format("call uspProcessPendingCloset({0});",closetId);
            IQuery q = NHibernateSession.Current.CreateSQLQuery(query);
            q.ExecuteUpdate();
        }

        public void ProcessClosetFile(string fileName)
        {
            string query = "LOAD DATA INFILE '{0}' INTO TABLE proc_closetoutfits ";
            query+= "FIELDS TERMINATED BY ','";
            query+= "LINES TERMINATED BY '\r\n' STARTING BY '' ";
            query+= "(ClosetId, FashionFlavorId,";
            query+= "Garment1Id, PreGarment1Id, Garment2Id, PreGarment2Id, Garment3Id, PreGarment3Id, Garment4Id, PreGarment4Id, Garment5Id, PreGarment5Id,";
            query+= "Garment6Id, PreGarment6Id, Garment7Id, PreGarment7Id, Garment8Id, PreGarment8Id, Garment9Id, PreGarment9Id, Garment10Id, PreGarment10Id,";
            query+= "Garment11Id, PreGarment11Id,Garment12Id, PreGarment12Id,Garment13Id, PreGarment13Id,";
            query+= "Seasons, EventTypes, Rating_EditorRating);";

            query = string.Format(query, fileName.Replace(@"\",@"\\"));

            IQuery q = NHibernateSession.Current.CreateSQLQuery(query);
            q.ExecuteUpdate();
        }

        public void RemoveGarmentFromCloset(int closetId, int garmentId)
        {
            IQuery q = NHibernateSession.Current.CreateSQLQuery("call uspRemoveGarmentFromOutfits(" + closetId + ", " + garmentId + ");");
            q.ExecuteUpdate();
        }

        public IList<Closet> Search(int userId, string searchText, int? styleType, int pageSize, int pageNumber, out int totalCount)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT DISTINCT C FROM Closet C JOIN C.User U LEFT JOIN U.friends F");
            
            if (styleType.HasValue)
                query.Append(" JOIN U.userFlavors UF");

            query.Append(" WHERE C.FavoriteOutfit IS NOT NULL");
            query.Append(" AND (C.PrivacyLevel = :PrivacyLevelFav OR C.PrivacyLevel = :PrivacyLevelFull");
            
            if (userId != 0) {
                query.Append(" OR (C.PrivacyLevel = :PrivacyLevelFriends AND C.User.Id IN (SELECT User.Id FROM Friend WHERE BasicUser.Id = :UserId) )");
            }

            query.Append(")");

            if (!string.IsNullOrEmpty(searchText))
                query.Append(" AND (U.FirstName LIKE '%" + searchText + "%' OR U.LastName LIKE '%" + searchText + "%' OR U.UserName LIKE '%" + searchText + "%' OR U.ZipCode LIKE '%" + searchText + "%')");

            if (styleType.HasValue)
                query.Append(" AND UF.Flavor.Id = :StyleType");

            if (userId != 0)
                query.Append(" AND U.Id != :UserId");

            IQuery q = Session.CreateQuery(query.ToString());

            q.SetEnum("PrivacyLevelFav", PrivacyLevel.FavoriteOutfit);
            q.SetEnum("PrivacyLevelFull", PrivacyLevel.FullCloset);
            if (userId != 0)
            {
                q.SetEnum("PrivacyLevelFriends", PrivacyLevel.Friends);
                q.SetInt32("UserId", userId);
            }

            if (styleType.HasValue)
                q.SetInt32("StyleType", styleType.Value);

            totalCount = q.List<Closet>().Count;

            if (pageSize != 0 && pageNumber != 0)
            {
                q.SetMaxResults(pageSize);
                q.SetFirstResult(pageSize * (pageNumber - 1));
            }
            return q.List<Closet>();
        }

        public IList<ClosetGarment> GetGarmentsForUser(RegisteredUser user)
        {
            ICriteria crit = Session.CreateCriteria(typeof(ClosetGarment));
            crit.SetFetchMode("Garment", FetchMode.Join);
            ICriteria closet = crit.CreateCriteria("Closet");
            closet.Add(Expression.Eq("User", user));
            return crit.List<ClosetGarment>();
        }

    }
}
