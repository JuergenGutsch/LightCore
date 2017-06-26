namespace LightCore.Activation.Activators
{
    /// <summary>
    /// Represents a activator based on a instance.
    /// This is always singleton per container.
    /// </summary>
    internal class InstanceActivator<TContract> : IActivator
    {
        /// <summary>
        /// Holds the instance.
        /// </summary>
        private readonly TContract _instance;

        /// <summary>
        /// Initializes a new instance of <see cref="InstanceActivator{TContract}" />.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public InstanceActivator(TContract instance)
        {
            this._instance = instance;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="resolutionContext">The resolutionContext.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this._instance;
        }
    }
}