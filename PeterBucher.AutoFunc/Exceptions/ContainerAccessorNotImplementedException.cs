using System;
using System.Runtime.Serialization;

namespace PeterBucher.AutoFunc.Exceptions
{
    /// <summary>
    /// Thrown when the container accessor container accessor is not implemented.
    /// </summary>
    [Serializable]
    public class ContainerAccessorNotImplementedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAccessorNotImplementedException"/> type.
        /// </summary>
        public ContainerAccessorNotImplementedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAccessorNotImplementedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ContainerAccessorNotImplementedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAccessorNotImplementedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ContainerAccessorNotImplementedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAccessorNotImplementedException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ContainerAccessorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}