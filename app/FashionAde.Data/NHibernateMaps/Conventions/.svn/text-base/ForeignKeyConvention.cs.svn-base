using System.Reflection;
using System;

namespace FashionAde.Data.NHibernateMaps.Conventions
{
    public class ForeignKeyConvention : FluentNHibernate.Conventions.ForeignKeyConvention
    {
        protected override string GetKeyName(FluentNHibernate.Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";

            return property.Name + "Id";
        }
    }
}
