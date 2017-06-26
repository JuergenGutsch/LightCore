using System;

namespace LightCore.Lifecycle
{
    /// <summary>
    /// Represents a singleton per container lifecycle.
    /// (One instance is shared within the same container).
    /// </summary>
    public class SingletonLifecycle : ILifecycle
    {
        /// <summary>
        /// Contains the lock object for instance creation.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The instance.
        /// </summary>
        private object _instance;

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver">The function for lazy get an instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            lock (_lock)
            {
                if (this._instance == null)
                {
                    this._instance = newInstanceResolver();
                }

                return this._instance;
            }
        }
    }
}