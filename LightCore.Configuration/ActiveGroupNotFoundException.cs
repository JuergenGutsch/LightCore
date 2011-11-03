using System;
using System.Runtime.Serialization;

namespace LightCore.Configuration
{
    /// <summary>
    /// Thrown when a given active group was not found in registration.
    /// </summary>
    [Serializable]
    public class ActiveGroupNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        public ActiveGroupNotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ActiveGroupNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ActiveGroupNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ActiveGroupNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}