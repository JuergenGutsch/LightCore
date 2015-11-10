using System;

namespace LightCore.Configuration
{
    /// <summary>
    /// Thrown when the container accessor container accessor is not implemented.
    /// </summary>
    public class JsonRegistrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRegistrationException"/> type.
        /// </summary>
        public JsonRegistrationException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public JsonRegistrationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public JsonRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}