using System.Web;
using System.Web.UI;

namespace PeterBucher.AutoFunc.Integration.Web
{
    public class AutoFuncDependencyInjectionModule : IHttpModule
    {
        /// <summary>
        /// The current application.
        /// </summary>
        private HttpApplication _application;

        /// <summary>
        /// The current container.
        /// </summary>
        private static IContainer _container;

        #region IHttpModule Members

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            this._application = context;

            var accessor = this._application as IContainerAccessor;

            if (accessor == null)
            {
                return;
            }

            _container = accessor.Container;

            this._application.PreRequestHandlerExecute += _application_PreRequestHandlerExecute;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // Do nothing.
        }

        #endregion

        private void _application_PreRequestHandlerExecute(object sender, System.EventArgs e)
        {
            object handler = this._application.Context.CurrentHandler;

            _container.InjectProperties(handler);

            var page = handler as Page;

            if (page != null)
            {
                page.PreLoad += (s, a) => this.InjectPropertiesOnControls(page);
            }
        }

        private void InjectPropertiesOnControls(Control parent)
        {
            if (parent.Controls != null)
            {
                foreach (Control control in parent.Controls)
                {
                    _container.InjectProperties(control);
                    InjectPropertiesOnControls(control);
                }
            }
        }
    }
}