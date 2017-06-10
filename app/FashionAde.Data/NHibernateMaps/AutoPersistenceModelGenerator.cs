using System;
using System.Linq;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FashionAde.Core;
using FashionAde.Data.NHibernateMaps.Conventions;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ContentManagement;

namespace FashionAde.Data.NHibernateMaps
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            var mappings = new AutoPersistenceModel();
            mappings.AddEntityAssembly(typeof(EventType).Assembly).Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.Setup(GetSetup());
            mappings.IgnoreBase<Entity>();
            mappings.IncludeBase<BasicUser>();
            mappings.IncludeBase<Garment>();
            mappings.IgnoreBase<ContentBase>();
            mappings.IgnoreBase<ContentSectionBase>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;

        }

        #endregion

        private Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
            {
                c.FindIdentity = type => type.Name == "Id";
                c.IsComponentType = type => (type == typeof(GarmentTags) || type == typeof(ColorBlendingRules) || type == typeof(Rating));
                c.GetComponentColumnPrefix = type => type.Name + "_";
                c.IsDiscriminated = type => type == typeof(Garment);
                c.DiscriminatorColumn = type => "Type";
                c.SubclassStrategy = type => (type == typeof(Garment))
                    ? SubclassStrategy.Subclass
                    : SubclassStrategy.JoinedSubclass;

            };
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.ForeignKeyConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.HasManyConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.HasManyToManyConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.ManyToManyTableNameConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.PrimaryKeyConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.ReferenceConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.TableNameConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.EnumConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.ColumnNullabilityConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.StringColumnLengthConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.DomainSignatureConvention>();
                c.Add<FashionAde.Data.NHibernateMaps.Conventions.SubclassConvention>();
                
            };
        }

        /// <summary>
        /// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
        /// </summary>

        private bool GetAutoMappingFilter(Type t)
        {
            return t.GetInterfaces().Any(x =>
                                         x.IsGenericType &&
                                         x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
        }
    }
}
