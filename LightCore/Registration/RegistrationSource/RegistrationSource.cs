using System;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a contract for dynamically add registrations, e.g. for generic or lazy support.
    /// </summary>
    internal abstract class RegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// The registration container.
        /// </summary>
        public RegistrationContainer RegistrationContainer
        {
            get;
            set;
        }

        /// <summary>
        /// The dependency selector. (Indicates whether the registration source can handle the type or not).
        /// </summary>
        public virtual Func<Type, bool> DependencySelector
        {
            get
            {
                return contractType => false;
            }
        }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns>The registration item if this source can handle it, otherwise <value>null</value>.</returns>
        public RegistrationItem GetRegistrationFor(Type contractType, IContainer container)
        {
            if (!this.DependencySelector(contractType))
            {
                return null;
            }

            return this.GetRegistrationForCore(contractType, container);
        }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns>The registration item if this source can handle it, otherwise <value>null</value>.</returns>
        protected abstract RegistrationItem GetRegistrationForCore(Type contractType, IContainer container);
    }
}