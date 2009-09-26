using System;

using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents a registration.
    /// </summary>
    public class Registration
    {
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
        /// <param name="imlementationType">The implementation type as <see cref="Type" />.</param>
        /// <param name="key">The registration key as <see cref="RegistrationKey" />.</see></param>
        public Registration(Type contractType, Type imlementationType, RegistrationKey key)
        {
            this.ContractType = contractType;
            this.ImplementationType = imlementationType;
            this.Key = key;
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
        /// Gets the implementation type as <see cref="Type" />.
        /// </summary>
        public Type ImplementationType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the LifeTime of the registration.
        /// </summary>
        public LifeTime LifeTime
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
        /// Gets or sets the current instance of this registration.
        /// </summary>
        public object Instance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the key for this registration.
        /// </summary>
        public RegistrationKey Key
        {
            get;
            private set;
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
    }
}