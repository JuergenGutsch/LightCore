using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Practices.ServiceLocation;

namespace LightCore.CommonServiceLocator
{
    /// <summary>
    /// Represents an adapter for the <see cref="IServiceLocator" /> interface,
    /// for the "CommonServiceLocator" project.
    /// </summary>
    public class LightCoreAdapter : ServiceLocatorImplBase
    {
        /// <summary>
        /// Contains the LightCore container.
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreAdapter" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public LightCoreAdapter(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Get an instance by service type and name (key).
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="key">The key.</param>
        /// <returns>The requested instance as object.</returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return this._container.Resolve(serviceType);
            }

            return this._container.Resolve(serviceType, key);
        }

        /// <summary>
        /// Gets all instances by service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>All requested instances.</returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return this._container
                .ResolveAll()
                .Where(i => i.GetType() == serviceType);
        }
    }
}