using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Castle.MicroKernel.Registration;
using FashionAde.Utils;

namespace FashionAde.ApplicationServices {

    public static class ServiceLocatorInitializer {
        public static void Init(IWindsorContainer container)
        {
            ComponentRegistrar.AddComponentsTo(container);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}