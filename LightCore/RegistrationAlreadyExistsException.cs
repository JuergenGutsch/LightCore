using System;
using System.Runtime.Serialization;

namespace LightCore
{
    /// <summary>
    /// Thrown when a registration already exists in container.
    /// </summary>
    [Serializable]
    public class RegistrationAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationAlreadyExistsException"/> type.
        /// </summary>
        public RegistrationAlreadyExistsException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationAlreadyExistsException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RegistrationAlreadyExistsException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationAlreadyExistsException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public RegistrationAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationAlreadyExistsException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected RegistrationAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}