using System;

namespace LightCore
{
    /// <summary>
    /// Represents a registration key.
    /// </summary>
    public class RegistrationKey
    {
        /// <summary>
        /// The contract type.
        /// </summary>
        public Type ContractType
        {
            get;
            set;
        }

        /// <summary>
        /// The implementation type.
        /// </summary>
        public Type ImplementationType
        {
            get;
            set;
        }

        /// <summary>
        /// The name for the registration.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The arguments.
        /// </summary>
        public object[] Arguments
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
        /// <param name="imlementationType">The implementation type as <see cref="Type" />.</param>
        /// <param name="arguments">The arguments for the registration.</see></param>
        public RegistrationKey(Type contractType, Type imlementationType, params object[] arguments)
        {
            this.ContractType = contractType;
            this.ImplementationType = imlementationType;
            this.Arguments = arguments;
        }
    }
}