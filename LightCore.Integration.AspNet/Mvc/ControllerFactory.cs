using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using LightCore.Integration.Web.Properties;

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
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;
        }

        /// <summary>
        /// Creates the controller internal for unit testing.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerType">The controller type.</param>
        /// <returns>The controller instance.</returns>
        internal IController CreateControllerInternal(RequestContext requestContext, Type controllerType)
        {
            return this.GetControllerInstance(requestContext, controllerType);
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param><param name="controllerType">The type of the controller.</param><exception cref="T:System.Web.HttpException"><paramref name="controllerType"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="controllerType"/> cannot be assigned.</exception><exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            if (controllerType == null)
            {
                throw new HttpException(404,
                                        string.Format(Resources.NoControllerTypeFoundForPathFormat,
                                                      requestContext.HttpContext.Request.Path));
            }

            return (IController)this._container.Resolve(controllerType);
        }
    }
}