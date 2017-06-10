using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FashionAde.Data.NHibernateMaps.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
            instance.GeneratedBy.Identity();
        }
    }
}
