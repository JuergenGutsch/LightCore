using System;
using System.Collections.Generic;
using System.Threading;


#if FALSE

namespace LightCore.Lifecycle
{
    /// <summary>
    /// Represents a singleton per thread lifecycle.
    /// (One instance is shared within one thread).
    /// </summary>
    public class ThreadSingletonLifecycle : ILifecycle
    {
        /// <summary>
        /// Contains the lock object for instance creation.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Holds an map with instances for different threads.
        /// </summary>
        private readonly IDictionary<int, object> _instanceMap;

        /// <summary>
        /// Initializes a new instance of <see cref="ThreadSingletonLifecycle" />.
        /// </summary>
        public ThreadSingletonLifecycle()
        {
            this._instanceMap = new Dictionary<int, object>();
        }

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver">The function for lazy get an instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            lock (_lock)
            {
                if (_instanceMap.ContainsKey(threadId))
                {
                    return _instanceMap[threadId];
                }

                var instance = newInstanceResolver();
                _instanceMap.Add(threadId, instance);


                return instance;
            }
        }
    }
}

#endif