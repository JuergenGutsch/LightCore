using System;

namespace LightCore.Scope
{
    /// <summary>
    /// Represents a scope where instances cannot be reused.
    /// </summary>
    public class LocalScope : ScopeBase
    {
        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        public override object ReceiveScopedInstance(Func<object> newInstanceResolver)
        {
            return newInstanceResolver();
        }
    }
}