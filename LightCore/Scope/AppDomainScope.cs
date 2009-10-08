using System;

namespace LightCore.Scope
{
    /// <summary>
    /// Represents a strategy for transient behaviour.
    /// Every request gets a new instance.
    /// </summary>
    public class AppDomainScope : ScopeBase
    {
        public override object ReceiveScopedInstance(Func<object> newInstanceResolver)
        {
            throw new NotImplementedException();
        }
    }
}