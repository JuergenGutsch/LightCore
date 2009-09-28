using System;

namespace PeterBucher.AutoFunc.Reuse
{
    /// <summary>
    /// Represents a strategy for transient behaviour.
    /// Every request gets a new instance.
    /// </summary>
    public class TransientReuseStrategy : IReuseStrategy
    {
        /// <summary>
        /// Handle the reuse of instances.
        /// Every request becomes a new instance.
        /// </summary>
        /// <param name="resolveNewInstance">The resolve function for a new instance.</param>
        public object HandleReuse(Func<object> resolveNewInstance)
        {
            return resolveNewInstance();
        }
    }
}