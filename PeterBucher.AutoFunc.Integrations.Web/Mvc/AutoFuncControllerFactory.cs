using System.Web.Routing;
using System.Web.Mvc;

namespace PeterBucher.AutoFunc.Integrations.Web.Mvc
{
    /// <summary>
    /// Represents a controller factory that works with a <see cref="IContainer" / >.
    /// </summary>
    public class AutoFuncControllerFactory : IControllerFactory
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerFactory" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutoFuncControllerFactory(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Creates the controller.
        /// Resolves it by name as registered from <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="requestContext">The request context.</param><param name="controllerName">Name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            // Returns the resolved controller to the caller.
            return this._container.ResolveNamed<IController>(controllerName.ToLowerInvariant());
        }

        /// <summary>
        /// Releases the controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public void ReleaseController(IController controller)
        {
            // do nothing.
        }
    }
}