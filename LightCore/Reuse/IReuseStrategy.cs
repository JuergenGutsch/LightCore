using System;

namespace LightCore.Reuse
{
    /// <summary>
    /// Represents a strategy for reusing instances.
    /// </summary>
    public interface IReuseStrategy
    {
        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        object HandleReuse(Func<object> newInstanceResolver);
    }
}