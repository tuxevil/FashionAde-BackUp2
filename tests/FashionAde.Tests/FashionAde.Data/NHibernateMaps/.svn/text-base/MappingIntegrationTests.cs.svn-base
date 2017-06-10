 using System.Collections.Generic;
 using FashionAde.Core.DataInterfaces;
 using FashionAde.Data.Repository;
 using NHibernate;
 using NHibernate.Criterion;
 using NHibernate.Metadata;
using NUnit.Framework;
using FashionAde.Data.NHibernateMaps;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using System;

namespace Tests.FashionAde.Data.NHibernateMaps
{
    /// <summary>
    /// Provides a means to verify that the target database is in compliance with all mappings.
    /// Taken from http://ayende.com/Blog/archive/2006/08/09/NHibernateMappingCreatingSanityChecks.aspx.
    /// 
    /// If this is failing, the error will likely inform you that there is a missing table or column
    /// which needs to be added to your database.
    /// </summary>
    [TestFixture]
    [Category("DB Tests")]
    public class MappingIntegrationTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            log4net.Config.XmlConfigurator.Configure();

            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            configuration = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                       new AutoPersistenceModelGenerator().Generate(),
                       "../../../../tests/FashionAde.Tests/Hibernate.cfg.xml");
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }

        [Test]
        public void CanConfirmDatabaseMatchesMappings()
        {
            CreateDatabaseSchema();

            var allClassMetadata = NHibernateSession.GetDefaultSessionFactory().GetAllClassMetadata();

            foreach (var entry in allClassMetadata)
            {
                NHibernateSession.Current.CreateCriteria(entry.Value.GetMappedClass(EntityMode.Poco))
                     .SetMaxResults(0).List();
            }
        }

        /// <summary>
        /// Generates and outputs the database schema SQL to the console
        /// </summary>
        [Test]
        public void CanGenerateDatabaseSchema()
        {
            var session = NHibernateSession.GetDefaultSessionFactory().OpenSession();
            new SchemaExport(configuration).Execute(true, false, false, session.Connection, null);
        }

        [Test]
        public void CreateDatabaseSchema()
        {
            var session = NHibernateSession.GetDefaultSessionFactory().OpenSession();
            session.BeginTransaction();
            SchemaExport ex = new SchemaExport(configuration);
            new SchemaExport(configuration).Execute(true, true, false);
            session.Transaction.Commit();
        }

        private Configuration configuration;
    }
}
