using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.ApplicationServices;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.DataInterfaces;
using FashionAde.Data.Repository;
using SharpArch.Testing.NHibernate;
using FashionAde.Data.NHibernateMaps;
using SharpArch.Data.NHibernate;
using Tests.FashionAde.ApplicationServices;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.Accounts;
using FashionAde.Services;

namespace Tests.FashionAde.Services
{
    [TestFixture]
    [Category("DB Tests")]
    public class OutfitEngineProcessorTest
    {
        #region Setup
        private NHibernate.Cfg.Configuration configuration;

        [SetUp]
        public virtual void SetUp()
        {
            log4net.Config.XmlConfigurator.Configure();

            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            configuration = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                       new AutoPersistenceModelGenerator().Generate(),
                       "../../../../tests/FashionAde.Tests/Hibernate.Test.cfg.xml");
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }
        #endregion

        [Test]
        public void CanRetrieveValidCombination()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr,cr);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "457546,1358386,2144626,2570491";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanCombine()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            service.CreateOutfits(1);
        }

        [Test]
        public void CreateInvitationCodes()
        {
            InvitationCodeRepository icr = new InvitationCodeRepository();

            icr.DbContext.BeginTransaction();

            for (int i = 0; i < 1000; i++)
            {
                InvitationCode ic = new InvitationCode();
                ic.Code = Guid.NewGuid().ToString();
                icr.SaveOrUpdate(ic);
            }

            icr.DbContext.CommitTransaction();
        }
    }
}
