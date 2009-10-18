using System;

namespace LightCore.Lifecycle
{
    /// <summary>
    /// Represents a lifecycle where instances cannot be reused.
    /// (Every request gets a new instance).
    /// </summary>
    public class TransientLifecycle : ILifecycle
    {
        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            return newInstanceResolver();
        }
    }
}