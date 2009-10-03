using System;

namespace LightCore
{
    /// <summary>
    /// Represents a registration key.
    /// </summary>
    public class RegistrationKey
    {
        /// <summary>
        /// The name for the registration.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The contract type.
        /// </summary>
        public Type ContractType
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        public RegistrationKey()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        /// <param name="contractType">The contract type as <see cref="Type"  />.</param>
        public RegistrationKey(Type contractType)
        {
            this.ContractType = contractType;
        }
    }
}