using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using SharpArch.Core.DomainModel;
using System;

namespace FashionAde.Data.NHibernateMaps.Conventions
{
    public class ReferenceConvention : IReferenceConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IManyToOneInstance instance)
        {
            instance.Column(instance.Property.Name + "Id");

            //if (Attribute.IsDefined(instance.State, typeof(DomainSignatureAttribute), false))
            //    instance.UniqueKey("DomainSignature");
            //else
            //    instance.Index(instance.State.Name + "Index");
        }
    }
}
