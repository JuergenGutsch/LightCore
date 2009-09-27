using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.Integration.Web;
using PeterBucher.AutoFunc.Integration.Web.Mvc;
using PeterBucher.AutoFunc.WebIntegrationSample.Controllers;
using PeterBucher.AutoFunc.WebIntegrationSample.Models;

namespace PeterBucher.AutoFunc.WebIntegrationSample
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
            ControllerBuilder.Current.SetControllerFactory(new AutoFuncControllerFactory(_container));
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
            var controllerRegistrationModule = new AutoFuncControllerRegistrationModule(controllerAssembly);
            
            builder.RegisterModule(controllerRegistrationModule);

            builder.Register<IFormsAuthentication, FormsAuthenticationService>().UseDefaultConstructor();
            builder.Register<IMembershipService, AccountMembershipService>().UseDefaultConstructor();
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