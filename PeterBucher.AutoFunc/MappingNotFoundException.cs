using System;
using System.Runtime.Serialization;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Thrown when a mapping not found for resolving a type.
    /// </summary>
    [Serializable]
    public class MappingNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingNotFoundException"/> type.
        /// </summary>
        public MappingNotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MappingNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public MappingNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingNotFoundException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected MappingNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}