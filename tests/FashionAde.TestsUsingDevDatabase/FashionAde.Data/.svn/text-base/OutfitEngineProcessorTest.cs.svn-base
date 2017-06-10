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
                       "../../../../tests/FashionAde.TestsUsingDevDatabase/Hibernate.Test.cfg.xml");
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }
        #endregion
        
        [Test]
        [TestCase(1)]
        public void CanRetrieveSmallClosetCombination(int flavorId)
        {
            IList<int> lst = new List<int>();

            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            string values = "195391,2079106,1325626,2652391";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(flavorId);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanRetrieveModerateClosetCombination()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "195391,180181,181741,182521,186421,187591,457546,459106,460666,464956,469246,688756,693436,692266,687586,689536,694996,698896,698116,1162996,1164166,1168456,1169236,1172746,1170016,1165336,1169626,1171186,1407526,1408696,1409866,1418446,1418056,1407917,1409087,1413377,1418447,1414937,966827,970727,972677,976187,966052,970342,972292,969952,2177386,2178556,2183626,2188696,2150086,2155156,2153986,2150476,2067406,2064286,2069746,2073646,2070526,2072476,2228866,2226916,20281,21841,25351,170041,169261,2979991,2981161,2570491,2571661,2572441,2636011,2636791,2637571";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(6);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanRetrieveClassicWithTrendy()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr,cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "244531,1325626,2062726,2619631";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(8);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanCreateWithOtherAccessories()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr,cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "244531,1309246,2226526,2227306,2472211,2472991,178231,168091,176281,2979991,2980771,3029131,3029911,246481,244531,1309636,1311196,2226916,2228476,2980381,2981941";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            cr.DbContext.BeginTransaction();
            Closet c = cr.Get(2);
            foreach (int i in lst)
                c.AddGarment(new MasterGarment(i));
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            //lstFlavors.Add(6);

            service.AddOutfits(2, lst);

            //IList<int> lstFlavors = new List<int>();
            //lstFlavors.Add(1);

            //Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanRetrieveWithPatterns()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr,cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "44531,244532,246092,261302,260911,262861,267151,1326796,1330306,1326019,1327969,1325629,1476949,1474219,1342006,1343176,1347856,2062726,2064286,2068576,2063118,2065458,2068188,2072868,2063898,2065068,2619631,2620801,2625481,2622361,2979991,2981161,2986231,2990521,2586871,2588821,2592331,2593501,2594671";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(8);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanRetrieveClassicWithAccessories() 
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr,cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            
            IList<int> lst = new List<int>();
            string values = "179011,179401,179791,180181,180961,181741,182911,187981,184081,187591,1359166,1358776,1358386,1361116,1365016,1369306,1365406,1366966,1367746,2144626,2145016,2145406,2146186,2146966,2146576,2145796,2151646,2570491,2571271,2570881,2572441,2573611,2574391,2575561,2573221,2578681,2579461,2578291,2572831,2575171,2576341,2577121,2577901,2574001,2576731,2579071,2581021,2583751,2574781";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);

            Assert.IsTrue(service.HasValidCombinations(lst, lstFlavors));
        }

        [Test]
        public void CanCreateSummerCombinationWithCoats()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);

            IList<int> lst = new List<int>();
            string values = "244531,245311,246091,1587706,1588876,1589656,2210146,2211316,2226526,2227696,2979991,2981161";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            cr.DbContext.BeginTransaction();
            Closet c = cr.Get(2);
            foreach (int i in lst)
                c.AddGarment(new MasterGarment(i));
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(6);

            service.AddOutfits(2, lst);
        }
        
        [Test]
        public void CanCreateLargeClosetWithAccessoriesCombination()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);

            IList<int> lst = new List<int>();
            string values = "781,14431,54601,55771,56551,62791,107251,109201,123631,179011,180571,391951,670411,671588,861908,948871,962911,1000748,1325611,1326001,1337191,1391131,1571311,1735111,1749931,1915305,1916851,1918411,2064271,2070901,2586871,2637961,2752621,2848951,2996371,2998321,3040051,3066181";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            cr.DbContext.BeginTransaction();
            Closet c = cr.Get(2);
            foreach (int i in lst)
                c.AddGarment(new MasterGarment(i));
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(8);

            service.AddOutfits(2, lst);
        }

        [Test]
        public void CanCreateAccessoriesCombination()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);

            IList<int> lst = new List<int>();
            string values = "244531,1309246,2062726,2979991,2981161,2980771,178231,178621,172771,169261,170041,2554111,2555281,3029131,3029911,3030691";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            cr.DbContext.BeginTransaction();
            Closet c = cr.Get(2);
            foreach (int i in lst)
                c.AddGarment(new MasterGarment(i));
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();

            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);

            service.AddOutfits(2, lst);
        }
        
        [Test]
        public void CanCreateBigClosetCombination()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);

            IList<int> lst = new List<int>();
            string values = "179011,180571,179791,458326,459106,459886,392806,393586,393976,228151,229711,230491,232051,232831,233611,834226,835786,836956,1113856,1114636,1115806,1113076,1117366,1162606,1163776,1165726,1358776,1359946,1361116,1362676,1365796,1571716,1573276,1574446,2161396,2162956,2164516,2162176,2145016,2146576,2148136,2151256,2147746,2571271,2573221,2996761,2997931,163411,164191,3029131,3030301,3029911,3031471,172771,173551,162631,162241,54991,55771,56941,178231,176281,195391,180181,181741,182521,186421,187591,457546,459106,460666,464956,469246,688756,693436,692266,687586,689536,694996,698896,698116,1162996,1164166,1168456,1169236,1172746,1170016,1165336,1169626,1171186,1407526,1408696,1409866,1418446,1418056,1407917,1409087,1413377,1418447,1414937,966827,970727,972677,976187,966052,970342,972292,969952,2177386,2178556,2183626,2188696,2150086,2155156,2153986,2150476,2067406,2064286,2069746,2073646,2070526,2072476,2228866,2226916,20281,21841,25351,170041,169261,2979991,2981161,2570491,2571661,2572441,2636011,2636791,2637571";
            foreach (string val in values.Split(','))
                lst.Add(Convert.ToInt32(val));

            cr.DbContext.BeginTransaction();
            Closet c = cr.Get(2);
            foreach(int i in lst)
                c.AddGarment(new MasterGarment(i));
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();
            
            IList<int> lstFlavors = new List<int>();
            lstFlavors.Add(1);
            lstFlavors.Add(6);

            service.AddOutfits(2, lst);
        }

        [Test]
        public void CanMatchOutfits()
        {
            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), new StyleRuleRepository());

            ous.MatchOutfitUpdatersForCloset(31);
        }

        [Test]
        public void CanCombine()
        {
            IStyleRuleRepository srr = new StyleRuleRepository();
            IClosetRepository cr = new ClosetRepository();
            IGarmentRepository gr = new GarmentRepository();
            IFashionFlavorRepository fr = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), srr);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(srr, cr, ous);

            IOutfitEngineService service = new OutfitEngineService(gr, cr, processor, fr);
            service.CreateOutfits(1);
        }
    }
}
