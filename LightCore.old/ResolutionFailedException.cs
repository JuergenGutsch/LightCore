using System;

using System.Runtime.Serialization;

namespace LightCore
{
    ///<summary>
    /// Thrown when resolving of a type failed.
    ///</summary>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionFailedException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ResolutionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}