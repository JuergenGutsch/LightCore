using System;
using System.Runtime.Serialization;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Thrown when a mapping already exists in container.
    /// </summary>
    [Serializable]
    public class MappingAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingAlreadyRegisteredException"/> type.
        /// </summary>
        public MappingAlreadyRegisteredException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingAlreadyRegisteredException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MappingAlreadyRegisteredException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingAlreadyRegisteredException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public MappingAlreadyRegisteredException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingAlreadyRegisteredException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected MappingAlreadyRegisteredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}