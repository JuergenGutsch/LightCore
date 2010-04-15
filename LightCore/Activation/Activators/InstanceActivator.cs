namespace LightCore.Activation.Activators
{
    /// <summary>
    /// Represents a activator based on a instance.
    /// </summary>
    internal class InstanceActivator<TInstance> : IActivator
    {
        /// <summary>
        /// Holds the instance.
        /// </summary>
        private readonly TInstance _instance;

        /// <summary>
        /// Initializes a new instance of <see cref="InstanceActivator{TContract}" />.
        /// </summary>
        /// <param name="instance">The instance.</param>
        internal InstanceActivator(TInstance instance)
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