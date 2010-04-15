using System;

namespace LightCore.Activation.Activators
{
    /// <summary>
    /// Represents a delegate instance activator.
    /// </summary>
    internal class DelegateActivator : IActivator
    {
        /// <summary>
        /// The activation function as a delegate.
        /// </summary>
        private readonly Func<IContainer, object> _activationFunction;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateActivator" />.
        /// </summary>
        /// <param name="activationFunction">The activator function.</param>
        internal DelegateActivator(Func<IContainer, object> activationFunction)
        {
            this._activationFunction = activationFunction;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="resolutionContext">The resolutionContext.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this._activationFunction(resolutionContext.Container);
        }
    }
}