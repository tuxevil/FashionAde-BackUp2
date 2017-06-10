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
using FashionAde.Web.Controllers;
using FashionAde.Data.NHibernateMaps;
using FashionAde.ApplicationServices;
using FashionAde.Utils;
using FashionAde.Utils.Validators;
using NHibernate.Classic;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System.ComponentModel;

using SharpArch.Core.CommonValidator;
using SharpArch.Core.DomainModel;
using System.Linq;

namespace FashionAde.Web
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

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAttribute), typeof(RegularExpressionAttributeAdapter));

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new DataAnnotationsModelValidatorProvider());

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

            //HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            //HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            //HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //HttpContext.Current.Response.Cache.SetNoStore();
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

            // Useful for debugging
            log4net.LogManager.GetLogger(typeof(MvcApplication).Namespace).Error(ex);
        }

        private WebSessionStorage webSessionStorage;
    }

    public class LeoModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Called when the model is updating. We handle updating the Id property here because it gets filtered out
        /// of the normal MVC2 property binding.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <returns>
        /// true if the model is updating; otherwise, false.
        /// </returns>
        protected override bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //if (IsEntityType(bindingContext.ModelType))
            //{
            //    //handle the Id property
            //    PropertyDescriptor idProperty =
            //        (from PropertyDescriptor property in TypeDescriptor.GetProperties(bindingContext.ModelType)
            //         where property.Name == ID_PROPERTY_NAME
            //         select property).SingleOrDefault();

            //    BindProperty(controllerContext, bindingContext, idProperty);

            //}
            return base.OnModelUpdating(controllerContext, bindingContext);
        }

        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            //Type propertyType = propertyDescriptor.PropertyType;

            //if (IsEntityType(propertyType))
            //{
            //    //use the EntityValueBinder
            //    return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, new EntityValueBinder());
            //}

            //if (IsSimpleGenericBindableEntityCollection(propertyType))
            //{
            //    //use the EntityValueCollectionBinder
            //    return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, new EntityCollectionValueBinder());
            //}

            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }

        protected override void SetProperty(ControllerContext controllerContext,
            ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            //if (propertyDescriptor.Name == ID_PROPERTY_NAME)
            //{
            //    SetIdProperty(bindingContext, propertyDescriptor, value);
            //}
            //else if (value as IEnumerable != null && IsSimpleGenericBindableEntityCollection(propertyDescriptor.PropertyType))
            //{
            //    SetEntityCollectionProperty(bindingContext, propertyDescriptor, value);
            //}
            //else
            //{
                base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
            //}

        }

        private static bool IsEntityType(Type propertyType)
        {
            bool isEntityType = propertyType.GetInterfaces()
                .Any(type => type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));

            return isEntityType;
        }

        private static bool IsSimpleGenericBindableEntityCollection(Type propertyType)
        {
            bool isSimpleGenericBindableCollection =
                propertyType.IsGenericType &&
                (propertyType.GetGenericTypeDefinition() == typeof(IList<>) ||
                 propertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                 propertyType.GetGenericTypeDefinition() == typeof(ISet<>) ||
                 propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            bool isSimpleGenericBindableEntityCollection =
                isSimpleGenericBindableCollection && IsEntityType(propertyType.GetGenericArguments().First());

            return isSimpleGenericBindableEntityCollection;
        }

        /// <summary>
        /// If the property being bound is an Id property, then use reflection to get past the
        /// protected visibility of the Id property, accordingly.
        /// </summary>
        private static void SetIdProperty(ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            Type idType = propertyDescriptor.PropertyType;
            object typedId = Convert.ChangeType(value, idType);

            // First, look to see if there's an Id property declared on the entity itself;
            // e.g., using the new keyword
            PropertyInfo idProperty = bindingContext.ModelType
                .GetProperty(propertyDescriptor.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            // If an Id property wasn't found on the entity, then grab the Id property from
            // the entity base class
            if (idProperty == null)
            {
                idProperty = bindingContext.ModelType
                    .GetProperty(propertyDescriptor.Name,
                        BindingFlags.Public | BindingFlags.Instance);
            }

            // Set the value of the protected Id property
            idProperty.SetValue(bindingContext.Model, typedId, null);
        }


        /// <summary>
        /// If the property being bound is a simple, generic collection of entiy objects, then use
        /// reflection to get past the protected visibility of the collection property, if necessary.
        /// </summary>
        private static void SetEntityCollectionProperty(ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, object value)
        {
            object entityCollection = propertyDescriptor.GetValue(bindingContext.Model);
            if (entityCollection != value)
            {
                Type entityCollectionType = entityCollection.GetType();

                foreach (object entity in (value as IEnumerable))
                {
                    entityCollectionType.InvokeMember("Add",
                                                      BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, entityCollection,
                                                      new object[] { entity });
                }
            }
        }

        #region Overridable (but not yet overridden) Methods

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return base.BindModel(controllerContext, bindingContext);
        }

        protected override object CreateModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext, Type modelType)
        {

            return base.CreateModel(controllerContext, bindingContext, modelType);
        }

        protected override void BindProperty(ControllerContext controllerContext,
    ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }

        protected override void OnPropertyValidated(ControllerContext controllerContext,
            ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            base.OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override bool OnPropertyValidating(ControllerContext controllerContext,
            ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {

            return base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
        }

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            foreach (string key in bindingContext.ModelState.Keys)
            {
                for (int i = 0; i < bindingContext.ModelState[key].Errors.Count; i++)
                {
                    ModelError modelError = bindingContext.ModelState[key].Errors[i];

                    // Get rid of all the MVC errors except those associated with parsing info; e.g., parsing DateTime fields
                    if (IsModelErrorAddedByMvc(modelError) && !IsMvcModelBinderFormatException(modelError))
                    {
                        bindingContext.ModelState[key].Errors.RemoveAt(i);
                        // Decrement the counter since we've shortened the list
                        i--;
                    }
                }
            }

            base.OnModelUpdated(controllerContext, bindingContext);

            //// Transfer any errors exposed by IValidator to the ModelState
            //if (bindingContext.Model is IValidatable)
            //{
            //    MvcValidationAdapter.TransferValidationMessagesTo(
            //        bindingContext.ModelName, bindingContext.ModelState,
            //        ((IValidatable)bindingContext.Model).ValidationResults());
            //}
        }
        #endregion

        private bool IsModelErrorAddedByMvc(ModelError modelError)
        {
            return modelError.Exception != null &&
                modelError.Exception.GetType().Equals(typeof(InvalidOperationException));
        }

        private bool IsMvcModelBinderFormatException(ModelError modelError)
        {
            return modelError.Exception != null &&
                modelError.Exception.InnerException != null &&
                modelError.Exception.InnerException.GetType().Equals(typeof(FormatException));
        }
        private const string ID_PROPERTY_NAME = "Id";
    }

}