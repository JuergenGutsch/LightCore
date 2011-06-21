using System;

#if !SL3 && !CF35
using System.Runtime.Serialization;
#endif

namespace LightCore
{
    ///<summary>
    /// Thrown when resolving of a type failed.
    ///</summary>
#if !SL3 && !CF35
    [Serializable]
#endif
    public class ResolutionFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        public ResolutionFailedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ResolutionFailedException(string message)
            : base(message)
        {

        }

#if !SL3 && !CF35
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ResolutionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected ResolutionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
#endif
    }
}