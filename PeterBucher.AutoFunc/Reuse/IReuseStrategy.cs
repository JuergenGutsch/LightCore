using System;

namespace PeterBucher.AutoFunc.Reuse
{
    /// <summary>
    /// Represents a strategy for reusing instances.
    /// </summary>
    public interface IReuseStrategy
    {
        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="resolveNewInstance"></param>
        object HandleReuse(Func<object> resolveNewInstance);
    }
}