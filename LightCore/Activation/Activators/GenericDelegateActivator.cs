using System;

namespace LightCore.Activation.Activators
{
    /// <summary>
    /// Represents a typed delegate instance activator.
    /// </summary>
    internal class GenericDelegateActivator<TContract> : DelegateActivator
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DelegateActivator" />.
        /// </summary>
        /// <param name="activationFunction">The activator function.</param>
        internal GenericDelegateActivator(Func<IContainer, TContract> activationFunction)
            : base(c => activationFunction(c))
        {

        }
    }
}