using System;
using System.Web;

using LightCore.Lifecycle;

namespace LightCore.Integration.Web.Lifecycle
{
    /// <summary>
    /// Represents a lifecycle for one instance per http request (ASP.NET).
    /// (One instance is shared within the same http request).
    /// </summary>
    public class HttpRequestLifecycle : ILifecycle
    {
        /// <summary>
        /// Contains the lock object for instance creation.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Represents an identifier for the current instance.
        /// </summary>
        private readonly string _instanceIdentifier = Guid.NewGuid().ToString();

        /// <summary>
        /// The current context for unit testing.
        /// </summary>
        public HttpContextBase CurrentContext
        {
            get;
            set;
        }

        /// <summary>
        /// Handle the reuse of instances.
        /// One instance per http request (ASP.NET).
        /// </summary>
        /// <param name="newInstanceResolver">The resolve function for a new instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {

            HttpContextBase context = this.CurrentContext;

            lock (this._lock)
            {
                if (context == null)
                {
                    context = new HttpContextWrapper(HttpContext.Current);
                }

                object instanceToReturn = context.Items[this._instanceIdentifier];

                if (instanceToReturn == null)
                {
                    instanceToReturn = newInstanceResolver();
                    context.Items[this._instanceIdentifier] = instanceToReturn;
                }

                return instanceToReturn;
            }
        }
    }
}