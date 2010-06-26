using System;

#if !SL2
using System.Runtime.Serialization;
#endif

namespace LightCore
{
    /// <summary>
    /// Thrown when a registration is invalid. e.g. Registration of interface to interface.
    /// </summary>
#if !SL2
    [Serializable]
#endif
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

#if !SL2
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegistrationException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected InvalidRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
#endif
    }
}