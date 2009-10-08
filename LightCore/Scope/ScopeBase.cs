using System;

namespace LightCore.Scope
{
    /// <summary>
    /// Represents a scope where instances can be reused.
    /// </summary>
    public abstract class ScopeBase : IScope
    {
        public abstract object ReceiveScopedInstance(Func<object> newInstanceResolver);
    }
} 