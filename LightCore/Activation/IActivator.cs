using System.Collections.Generic;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an instance activator.
    /// </summary>
    internal interface IActivator
    {
        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        object ActivateInstance(Container container, IEnumerable<object> arguments);
    }
}