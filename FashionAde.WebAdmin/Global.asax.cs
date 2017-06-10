using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using NHibernate.Cfg;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;
using SharpArch.Web.Castle;
using SharpArch.Web.Areas;
using SharpArch.Web.CommonValidator;
using SharpArch.Web.ModelBinder;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using FashionAde.WebAdmin.Controllers;
using FashionAde.Data.NHibernateMaps;
using FashionAde.ApplicationServices;
using FashionAde.Utils;
using xVal.RuleProviders;
using NHibernate.Validator.Constraints;
using NHibernate.Validator.Engine;
using xVal.Rules;
using System.Collections.Generic;
using FashionAde.Utils.Validators;
using SharpArch.Core.NHibernateValidator.ValidatorProvider;
using System.ComponentModel;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.DomainModel;
using System.Linq;
using Iesi.Collections.Generic;
using System.Collections;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.WebAdmin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AreaViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

            // Add NHibernate Validator Provider for the Server Side to be automated.
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new NHibernateValidatorProvider());

            // Add xVal Validator Provider For NHV 1.2
            // This allow us to use automatic client side validation when rules are defined.
            // without using data annotations, given that we may have an unnecessary mix with Domain Validation framework.
            xVal.ActiveRuleProviders.Providers.Add(new ValidatorRulesProvider());

            InitializeServiceLocator();

            AreaRegistration.RegisterAllAreas();
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from 
        /// WindsorController to the container.  Also associate the Controller 
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddApplicationServicesTo(container);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        public override void Init()
        {
            base.Init();

            // The WebSessionStorage must be created during the Init() to tie in HttpApplication events
            webSessionStorage = new WebSessionStorage(this);
        }

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
        /// must only be called once.  Consequently, we invoke a thread-safe singleton class to 
        /// ensure it's only initialized once.
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                () => InitializeNHibernateSession());

            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        /// <summary>
        /// If you need to communicate to multiple databases, you'd add a line to this method to
        /// initialize the other database as well.
        /// </summary>
        private void InitializeNHibernateSession()
        {
            NHibernateSession.Init(
                webSessionStorage,
                new string[] { Server.MapPath("~/bin/FashionAde.Data.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                Server.MapPath("~/NHibernate.config"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            Exception ex = Server.GetLastError();
            ReflectionTypeLoadException reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        private WebSessionStorage webSessionStorage;
    }
}
