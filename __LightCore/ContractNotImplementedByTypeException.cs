using System;
#if !DNXCORE50
using System.Runtime.Serialization;
#endif

namespace LightCore
{
    /// <summary>
    /// Thrown when the contract type is not assignable from implementationtype.
    /// </summary>
#if !DNXCORE50
    [Serializable]
#endif
    public class ContractNotImplementedByTypeException : Exception
    {
        /// <summary>
        /// Gets the contract type.
        /// </summary>
        public Type ContractType { get; private set; }

        /// <summary>
        /// Gets the implementation type.
        /// </summary>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        public ContractNotImplementedByTypeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ContractNotImplementedByTypeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="contractType">The contract type.</param>
        /// <param name="implementationType">The implementation type.</param>
        public ContractNotImplementedByTypeException(string message, Type contractType, Type implementationType)
                : base(message)
        {
            ContractType = contractType;
            ImplementationType = implementationType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ContractNotImplementedByTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !DNXCORE50
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ContractNotImplementedByTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}