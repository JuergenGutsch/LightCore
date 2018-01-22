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
        private static readonly object lock1 = new object();
        private readonly object lock2 = new object();

        /// <summary>
        /// Holds an map with instances for different threads.
        /// </summary>
        private static readonly IDictionary<int, WeakReference> instanceMap = new Dictionary<int, WeakReference>();

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
            lock (lock1)
            {
                lock (lock2)
                {
                    var threadId = Thread.CurrentThread.ManagedThreadId;

                    if (instanceMap.ContainsKey(threadId))
                    {
                        return instanceMap[threadId].Target;
                    }

                    var instance = newInstanceResolver();
                    instanceMap.Add(threadId, new WeakReference(instance));

                    return instance;
                }
            }
        }
    }
}