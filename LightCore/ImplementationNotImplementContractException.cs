using System;
using System.Runtime.Serialization;

namespace LightCore
{
    /// <summary>
    /// Thrown when the contract type is not assignable from implementationtype.
    /// </summary>
    [Serializable]
    public class ImplementationNotImplementContractException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationNotImplementContractException"/> type.
        /// </summary>
        public ImplementationNotImplementContractException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationNotImplementContractException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ImplementationNotImplementContractException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationNotImplementContractException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ImplementationNotImplementContractException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationNotImplementContractException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ImplementationNotImplementContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}