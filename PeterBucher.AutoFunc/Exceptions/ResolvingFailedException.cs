using System;
using System.Runtime.Serialization;

namespace PeterBucher.AutoFunc.Exceptions
{
    /// <summary>
    /// Thrown when resolving of a type failed.
    /// </summary>
    [Serializable]
    public class ResolvingFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvingFailedException"/> type.
        /// </summary>
        public ResolvingFailedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvingFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ResolvingFailedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvingFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ResolvingFailedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ResolvingFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}