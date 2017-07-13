using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using LightCore.Integration.Web;
using LightCore.Integration.Web.Mvc;
using LightCore.Web.Mvc.IntegrationSample.Controllers;
using LightCore.Web.Mvc.IntegrationSample.Models;

namespace LightCore.Web.Mvc.IntegrationSample
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        private static IContainer _container;

        protected void Application_Start()
        {
            // Registering the routes.
            RegisterRoutes(RouteTable.Routes);

            // Register dependencies.
            RegisterDependencies();

            // Set controller factory.
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(_container));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            var controllerAssembly = Assembly.GetExecutingAssembly();
            var controllerRegistrationModule = new ControllerRegistrationModule(controllerAssembly);

            builder.RegisterModule(controllerRegistrationModule);

            builder.Register<IFoo, Foo>();
            builder.Register<IFormsAuthentication>(c => new FormsAuthenticationService());
            builder.Register<IMembershipService>(c => new AccountMembershipService());
            builder.Register<IWelcomeRepository, WelcomeRepository>();

            _container = builder.Build();
        }

        public IContainer Container
        {
            get
            {
                return _container;
            }
        }
    }
}