using System;
using System.Collections.Generic;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents a delegate instance activator.
    /// </summary>
    public class DelegateActivator<TContract> : IActivator
    {
        /// <summary>
        /// The activation function as a delegate.
        /// </summary>
        private readonly Func<IContainer, TContract> _activationFunction;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateActivator{TContract}" />.
        /// </summary>
        /// <param name="activationFunction">The activator function.</param>
        public DelegateActivator(Func<IContainer, TContract> activationFunction)
        {
            this._activationFunction = activationFunction;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(Container container, IEnumerable<object> arguments)
        {
            return this._activationFunction(container);
        }
    }
}