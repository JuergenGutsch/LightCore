using System;

using System.Runtime.Serialization;

namespace LightCore
{
    /// <summary>
    /// Thrown when a registration is invalid. e.g. Registration of interface to interface.
    /// </summary>
    [Serializable]
    public class InvalidRegistrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> type.
        /// </summary>
        public InvalidRegistrationException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public InvalidRegistrationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}