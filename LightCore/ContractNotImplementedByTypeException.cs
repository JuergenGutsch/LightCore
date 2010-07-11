using System;

#if !SL2 && !SL3 && !CF35
using System.Runtime.Serialization;
#endif

namespace LightCore
{
    /// <summary>
    /// Thrown when the contract type is not assignable from implementationtype.
    /// </summary>
#if !SL3 && !CF35
    [Serializable]
#endif
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

#if !SL3 && !CF35
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
#endif
    }
}