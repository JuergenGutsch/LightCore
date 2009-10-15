using System.Web.Routing;
using System.Web.Mvc;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a default controller factory that works with a <see cref="IContainer" / >.
    /// </summary>
    public class ControllerFactory : ControllerFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ControllerFactory" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public ControllerFactory(IContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Creates the controller.
        /// Resolves it by name as registered from <see cref="ControllerRegistrationModule{TLifecycle}" />.
        /// </summary>
        /// <param name="requestContext">The request context.</param><param name="controllerName">Name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        protected override IController CreateControllerCore(RequestContext requestContext, string controllerName)
        {
            // Returns the resolved controller to the caller.
            return this.Container.Resolve<IController>(controllerName.ToLowerInvariant());
        }
    }
}