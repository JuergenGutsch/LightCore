using System;
using System.Collections.Generic;

namespace LightCore.Activator
{
    /// <summary>
    /// Represents a delegate instance activator.
    /// </summary>
    public class DelegateActivator<TContract> : IActivator
    {
        private readonly Func<TContract> _activationFunction;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateActivator" />.
        /// </summary>
        /// <param name="activationFunction">The activator function.</param>
        public DelegateActivator(Func<TContract> activationFunction)
        {
            this._activationFunction = activationFunction;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(IContainer container, IEnumerable<object> arguments)
        {
            return this._activationFunction();
        }
    }
}