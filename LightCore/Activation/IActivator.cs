using LightCore.Registration;

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
        /// <param name="runtimeArguments">The runtime arguments.</param>
        /// <returns>The activated instance.</returns>
        object ActivateInstance(Container container, ArgumentContainer arguments, ArgumentContainer runtimeArguments);
    }
}