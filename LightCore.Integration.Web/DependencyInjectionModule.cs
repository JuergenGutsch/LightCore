using System.Web;
using System.Web.UI;

namespace LightCore.Integration.Web
{
    /// <summary>
    /// Represents a <see cref="IHttpModule" /> for property injection on ASP.NET WebForms.
    /// </summary>
    public class DependencyInjectionModule : IHttpModule
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

            this._application.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // Do nothing.
        }

        #endregion

        /// <summary>
        /// Executes before the <see cref="IHttpHandler" /> executes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The eventargs.</param>
        private void OnPreRequestHandlerExecute(object sender, System.EventArgs e)
        {
            object handler = this._application.Context.CurrentHandler;

            if(handler == null)
            {
                return;
            }

            // Inject properties on current handler.
            _container.InjectProperties(handler);

            var page = handler as Page;

            if (page != null)
            {
                // Inject properties on all controls.
                page.PreLoad += (s, a) => this.InjectPropertiesOnControls(page);
            }
        }

        /// <summary>
        /// Inject properties on a <see cref="Control" /> recursive.
        /// </summary>
        /// <param name="parent">The parent control.</param>
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