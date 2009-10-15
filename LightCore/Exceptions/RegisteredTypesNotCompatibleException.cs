using System;
using System.Runtime.Serialization;

namespace LightCore.Exceptions
{
    /// <summary>
    /// Thrown when the contract type is not assignable from implementationtype.
    /// </summary>
    [Serializable]
    public class RegisteredTypesNotCompatibleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredTypesNotCompatibleException"/> type.
        /// </summary>
        public RegisteredTypesNotCompatibleException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredTypesNotCompatibleException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RegisteredTypesNotCompatibleException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredTypesNotCompatibleException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public RegisteredTypesNotCompatibleException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredTypesNotCompatibleException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected RegisteredTypesNotCompatibleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}