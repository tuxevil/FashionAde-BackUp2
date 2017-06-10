using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FashionAde.Core.Clothing;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;

namespace FashionAde.Data.NHibernateMaps.Conventions
{
    public class SubclassConvention : ISubclassConvention, ISubclassConventionAcceptance
    {
        #region IConvention<ISubclassInspector,ISubclassInstance> Members

        public void Apply(ISubclassInstance instance)
        {
            if (instance.Name == typeof(MasterGarment).AssemblyQualifiedName)
                instance.DiscriminatorValue(1);

            if (instance.Name == typeof(UserGarment).AssemblyQualifiedName)
                instance.DiscriminatorValue(2);

        }

        #endregion

        #region IConventionAcceptance<ISubclassInspector> Members

        public void Accept(IAcceptanceCriteria<ISubclassInspector> criteria)
        {
            criteria.Expect(subclass => Type.GetType(subclass.Name).BaseType == typeof(Garment));
        }

        #endregion
    }

}
