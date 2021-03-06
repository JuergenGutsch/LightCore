﻿using System;
using LightCore.Lifecycle;
using Microsoft.AspNetCore.Http;

namespace LightCore.Integration.AspNetCore.Lifecycle
{
    /// <summary>
    /// Represents a lifecycle for one instance per http request (ASP.NET).
    /// (One instance is shared within the same http request).
    /// </summary>
    public class HttpRequestLifecycle : ILifecycle
    {
        public HttpRequestLifecycle()
        {

        }

        internal HttpRequestLifecycle(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Contains the lock object for instance creation.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Represents an identifier for the current instance.
        /// </summary>
        private readonly string _instanceIdentifier = Guid.NewGuid().ToString();

        /// <summary>
        /// Contains an instance of the HttpContextAccessor. Needed to identify the current http context
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Handle the reuse of instances.
        /// One instance per http request (ASP.NET Core).
        /// </summary>
        /// <param name="newInstanceResolver">The resolve function for a new instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            if (_httpContextAccessor == null)
            {
                _httpContextAccessor = new HttpContextAccessor();
            }
            var context = _httpContextAccessor.HttpContext;

            lock (_lock)
            {
                var instanceToReturn = context.Items[_instanceIdentifier];

                if (instanceToReturn == null)
                {
                    instanceToReturn = newInstanceResolver();
                    context.Items[_instanceIdentifier] = instanceToReturn;
                }

                return instanceToReturn;
            }
        }
    }
}