using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using FashionAde.ApplicationServices;
using SharpArch.Data.NHibernate;
using SharpArch.Wcf.NHibernate;
using FashionAde.Data.NHibernateMaps;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using FashionAde.Services;
using FashionAde.Utils;

namespace FashionAde.ServiceHost
{
    public partial class OutfitEngine : ServiceBase
    {
        SharpArch.Wcf.NHibernate.ServiceHost host;
        SharpArch.Wcf.NHibernate.ServiceHost hostUpdater;

        private static WcfSessionStorage storage;

        public OutfitEngine()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            storage = new WcfSessionStorage();

            log4net.Config.XmlConfigurator.Configure();

            // Create container
            IWindsorContainer container = new WindsorContainer();

            // Add Engine for the Host Service
            container.AddComponent("outfitEngineService", typeof(OutfitEngineService));
            container.AddComponent("outfitUpdaterService", typeof(OutfitUpdaterService));

            // Add the Services to the Container
            ComponentRegistrar.AddServicesTo(container);

            // Create the container
            ServiceLocatorInitializer.Init(container);

            // Initialize NHibernate
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                () => InitializeNHibernateSession());

            // Create Service Host
            host = new SharpArch.Wcf.NHibernate.ServiceHost(typeof(OutfitEngineService));
            host.Open();

            hostUpdater = new SharpArch.Wcf.NHibernate.ServiceHost(typeof(OutfitUpdaterService));
            hostUpdater.Open();

        }

        protected override void OnStop()
        {
            if (host != null)
                host.Close();

            if (hostUpdater != null)
                hostUpdater.Close();
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
