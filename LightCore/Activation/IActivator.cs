using System.Collections.Generic;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an instance activator.
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Gets or sets whether the default constructor should be used or not.
        /// </summary>
        bool UseDefaultConstructor
        {
            get;
            set;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        object ActivateInstance(IContainer container, IEnumerable<object> arguments);
    }
}