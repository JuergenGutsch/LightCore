using System;

namespace LightCore.Scope
{
    /// <summary>
    /// Represents a scope where instances can be reused.
    /// </summary>
    public interface IScope
    {

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        object ReceiveScopedInstance(Func<object> newInstanceResolver);
    }
}