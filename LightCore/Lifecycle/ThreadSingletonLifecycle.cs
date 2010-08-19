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
        private static readonly object Lock = new object();

        /// <summary>
        /// Holds an map with instances for different threads.
        /// </summary>
        private readonly IDictionary<int, WeakReference> _instanceMap;

        /// <summary>
        /// Initializes a new instance of <see cref="ThreadSingletonLifecycle" />.
        /// </summary>
        public ThreadSingletonLifecycle()
        {
            this._instanceMap = new Dictionary<int, WeakReference>();
        }

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver">The function for lazy get an instance.</param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            lock(Lock)
            {
                if(this._instanceMap.ContainsKey(threadId))
                {
                    return this._instanceMap[threadId].Target;
                }

                var instance = newInstanceResolver();
                this._instanceMap.Add(threadId, new WeakReference(instance));


                return instance;
            }
        }
    }
}