using System;

using LightCore.Reuse;

namespace LightCore
{
    /// <summary>
    /// Represents an abstract registration module for implementing custom registrations.
    /// </summary>
    public abstract class RegistrationModule
    {
        /// Gets or sets the reuse strategy to use for all controllers.
        /// </summary>
        public Func<IReuseStrategy> ReuseStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The controllerbuilder.</param>
        public abstract void Register(IContainerBuilder containerBuilder);
    }
}