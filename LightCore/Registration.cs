using System;
using LightCore.Activator;
using LightCore.Fluent;
using LightCore.Reuse;

namespace LightCore
{
    /// <summary>
    /// Represents a registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Gets the key for this registration.
        /// </summary>
        public RegistrationKey Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the contract type as <see cref="Type" />.
        /// </summary>
        public Type ContractType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the activator for an instance.
        /// </summary>
        public IActivator Activator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reuse strategy for the instance creation.
        /// </summary>
        public IReuseStrategy ReuseStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the default constructor should be used, or not.
        /// </summary>
        public bool UseDefaultConstructor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments for object creations.
        /// </summary>
        public object[] Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current fluent interface instance.
        /// </summary>
        public IFluentRegistration FluentRegistration
        {
            get
            {
                return new FluentRegistration(this);
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        public Registration()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        /// <param name="contractType">The contract type as <see cref="Type"  />.</param>
        /// <param name="key">The registration key as <see cref="RegistrationKey" />.</see></param>
        public Registration(Type contractType, RegistrationKey key)
        {
            this.ContractType = contractType;
            this.Key = key;
        }
    }
}