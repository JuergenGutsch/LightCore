using System;

namespace LightCore.Lifecycle
{
    /// <summary>
    /// Represents a lifecycle where instances can be reused.
    /// </summary>
    public interface ILifecycle
    {

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver">The function for lazy get an instance.</param>
        object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver);
    }
}