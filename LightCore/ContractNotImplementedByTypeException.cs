using System;
using System.Runtime.Serialization;

namespace LightCore
{
    /// <summary>
    /// Thrown when the contract type is not assignable from implementationtype.
    /// </summary>
    [Serializable]
    public class ContractNotImplementedByTypeException : Exception
    {
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
        /// <param name="innerException">The inner exception</param>
        public ContractNotImplementedByTypeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractNotImplementedByTypeException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ContractNotImplementedByTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}