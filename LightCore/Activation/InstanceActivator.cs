using System.Collections.Generic;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents a activator based on a instance.
    /// </summary>
    internal class InstanceActivator<TInstance> : IActivator
    {
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
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(Container container, IEnumerable<object> arguments)
        {
            return this._instance;
        }
    }
}