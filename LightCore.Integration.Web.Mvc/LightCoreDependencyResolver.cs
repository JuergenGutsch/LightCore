using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a DependencyResolver for ASP.NET MVC 3.0 / 4.0 that works with a <see cref="IContainer" />.
    /// </summary>
    public class LightCoreDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreDependencyResolver" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public LightCoreDependencyResolver(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Gets a service.
        /// </summary>
        /// <param name="serviceType">The requested services type.</param>
        /// <returns>The service instance.</returns>
        public object GetService(Type serviceType)
        {
            object service = null;

            if (this._container.HasRegistration(serviceType))
            {
                service = this._container.Resolve(serviceType);
            }

            return service;
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">The requested service type.</param>
        /// <returns>The services as enumerable objects.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> services;

            if (this._container.HasRegistration(serviceType))
            {
                services = this._container.ResolveAll(serviceType);
            }
            else
            {
                services = new List<object>();
            }

            return services;
        }
    }
}