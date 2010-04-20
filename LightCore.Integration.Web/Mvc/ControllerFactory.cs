using System.Web.Mvc;
using System.Web.Routing;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a default controller factory that works with a <see cref="IContainer" />.
    /// </summary>
    public class ControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerFactory" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public ControllerFactory(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <param name="requestContext">The request context.</param><param name="controllerName">Name of the controller.</param>
        /// <returns>
        /// A reference to the controller.
        /// </returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return (IController)this._container.Resolve(this.GetControllerType(controllerName));
        }
    }
}