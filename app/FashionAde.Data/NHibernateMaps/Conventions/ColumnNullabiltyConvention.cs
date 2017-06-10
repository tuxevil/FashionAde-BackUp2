using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using SharpArch.Core.DomainModel;

namespace FashionAde.Data.NHibernateMaps.Conventions
{
    public class ColumnNullabilityConvention
        : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, 
                Is.Not.Set);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }

    public class StringColumnLengthConvention: IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(string))
                .Expect(x => x.Length == 0);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Length(100);
        }
    }

    public class ForeignKeyConstraintNameConvention
    : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(String.Format("{0}_{1}_FK",
            instance.Member.Name, instance.EntityType.Name));
        }
    }

    public class DomainSignatureConvention : AttributePropertyConvention<DomainSignatureAttribute>
    {
        protected override void Apply(DomainSignatureAttribute attribute, IPropertyInstance instance)
        {
            instance.UniqueKey(instance.EntityType.Name + "DomainSignature");
        }
    }


}
