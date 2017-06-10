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
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.Accounts;

namespace FashionAde.Data.Repository
{
    public class UserGarmentRepository : Repository<UserGarment>, IUserGarmentRepository
    {
        public List<WebClosetGarment> GetWebClosetGarments(RegisteredUser registeredUser)
        {
            ICriteria crit = Session.CreateCriteria(typeof(UserGarment), "garment");
            ICriteria user = crit.CreateCriteria("User");
            ICriteria category = crit.CreateCriteria("Tags").CreateCriteria("Silouhette").CreateCriteria("Category", "category");

            user.Add(Expression.Eq("Id", registeredUser.Id));
            crit.SetProjection(Projections.ProjectionList().Add(Projections.Property("Id"))
                 .Add(Projections.Property("garment.Title"))
                 .Add(Projections.Property("garment.ImageUri"))
                 .Add(Projections.Property("category.Id"))
                );

            crit.SetResultTransformer(new NHibernate.Transform.AliasToBeanConstructorResultTransformer(typeof(WebClosetGarment).GetConstructors()[1]));
            return crit.List<WebClosetGarment>() as List<WebClosetGarment>;
        }


        public IList<UserGarment> GetRecentlyUploaded(RegisteredUser user)
        {
            UserGarment u = new UserGarment();
            ICriteria crit = Session.CreateCriteria(typeof(UserGarment));
            
            crit.Add(Expression.Eq("User", user));
            crit.AddOrder(new Order("Id", false));
            crit.SetMaxResults(2);
            
            return crit.List<UserGarment>();
        }
    }
}
