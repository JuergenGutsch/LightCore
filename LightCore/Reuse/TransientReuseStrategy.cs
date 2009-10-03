using System;

namespace LightCore.Reuse
{
    /// <summary>
    /// Represents a strategy for transient behaviour.
    /// Every request gets a new instance.
    /// </summary>
    public class TransientReuseStrategy : IReuseStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TransientReuseStrategy" />.
        /// </summary>
        public TransientReuseStrategy()
        {

        }

        /// <summary>
        /// Handle the reuse of instances.
        /// Every request becomes a new instance.
        /// </summary>
        /// <param name="newInstanceResolver">The resolve function for a new instance.</param>
        public object HandleReuse(Func<object> newInstanceResolver)
        {
            return newInstanceResolver();
        }
    }
}