namespace LightCore.Activation.Activators
{
    /// <summary>
    /// Represents an instance activator.
    /// </summary>
    internal interface IActivator
    {
        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns>The activated instance.</returns>
        object ActivateInstance(ResolutionContext resolutionContext);
    }
}