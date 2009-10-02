using System;
using System.Web;
using PeterBucher.AutoFunc.Builder;
using PeterBucher.AutoFunc.Integration.Web;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Web.IntegrationSample
{
    public class Global : HttpApplication, IContainerAccessor
    {
        private static IContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.Register<IWelcomeRepository, WelcomeRepository>();

            _container = builder.Build();
        }

        #region Other application events

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        #endregion

        #region IContainerAccessor Members

        public IContainer Container
        {
            get
            {
                return _container;
            }
        }

        #endregion
    }
}