using System;
using System.Web.Routing;
using System.Web.Mvc;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a base controller factory that works with a <see cref="IContainer" />.
    /// Can be used as base class for custom implementations.
    /// </summary>
    public abstract class ControllerFactoryBase : IControllerFactory
    {
        /// <summary>
        /// The container.
        /// </summary>
        protected IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerFactoryBase" />.
        /// </summary>
        protected ControllerFactoryBase()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerFactoryBase" />.
        /// </summary>
        /// <param name="container">The container.</param>
        protected ControllerFactoryBase(IContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Creates the controller.
        /// Resolves it by name as registered from <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            return this.CreateControllerCore(requestContext, controllerName);
        }

        /// <summary>
        /// Creates the controller.
        /// Resolves it by name as registered from <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        protected abstract IController CreateControllerCore(RequestContext requestContext, string controllerName);

        /// <summary>
        /// Releases the controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public virtual void ReleaseController(IController controller)
        {
            // do nothing
        }
    }
}