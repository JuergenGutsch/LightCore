using System;
using System.Collections.Generic;
using System.Threading;

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
        private readonly object Lock = new object();

        /// <summary>
        /// Holds an map with instances for different threads.
        /// </summary>
        private readonly IDictionary<int, WeakReference> InstanceMap = new Dictionary<int, WeakReference>();

        /// <summary>
        /// Initializes a new instance of <see cref="ThreadSingletonLifecycle" />.
        /// </summary>
        public ThreadSingletonLifecycle()
        {

        }

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver">The function for lazy get an instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            lock (Lock)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;

                if (InstanceMap.ContainsKey(threadId))
                {
                    return InstanceMap[threadId].Target;
                }

                var instance = newInstanceResolver();
                InstanceMap.Add(threadId, new WeakReference(instance));

                return instance;
            }
        }
    }
}