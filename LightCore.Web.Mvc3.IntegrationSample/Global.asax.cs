using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using LightCore.Integration.Web.Mvc;
using LightCore.TestTypes;

namespace LightCore.Web.Mvc3.IntegrationSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new
                    {
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    } // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();

            var controllerRegistrationModule = new ControllerRegistrationModule(Assembly.GetExecutingAssembly());
            controllerRegistrationModule.Register(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new LightCoreDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}