using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using FashionAde.Data.NHibernateMaps;
using FashionAde.Data.Repository;
using FashionAde.OutfitUpdaterImportation.Controllers;
using FashionAde.OutfitUpdaterImportation.Core;
using SharpArch.Data.NHibernate;
using SharpArch.Wcf.NHibernate;

namespace OUImportationTest
{
    class Program
    {
        private static WcfSessionStorage storage = new WcfSessionStorage();

        static void Main(string[] args)
        {
            try
            {
                // Create the container
                IWindsorContainer container = new WindsorContainer();
                
                // Initialize NHibernate
                NHibernateInitializer.Instance().InitializeNHibernateOnce(
                    () => InitializeNHibernateSession());

                GC.Collect();
                OUImportationController temp = new OUImportationController(new ZapposClassBuilder(), new OutfitUpdaterRepository(), new TrendRepository());
                //OUImportationController temp = new OUImportationController(new ZapposClassBuilder(), null, null);
                temp.HaveHeader = true;
                if (args[0].ToLower() == "false")
                    temp.HaveHeader = false;
                temp.Separator = args[1];
                temp.Path = args[2];
                temp.Filename = args[3];
                if (args[4].ToLower() == "false")
                    temp.Sync = false;

                temp.MemorySafe = Convert.ToDouble(args[5]);
                temp.Create();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        private static void InitializeNHibernateSession()
        {
            NHibernateSession.Init(
                storage,
                new string[] { "FashionAde.Data.dll" },
                new AutoPersistenceModelGenerator().Generate(),
                "NHibernate.config");
        }
    }
}
