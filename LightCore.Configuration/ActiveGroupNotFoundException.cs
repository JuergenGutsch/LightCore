using System;

namespace LightCore.Configuration
{
    /// <summary>
    /// Thrown when a given active group was not found in registration.
    /// </summary>
    public class ActiveGroupNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveGroupNotFoundException"/> type.
        /// </summary>
        public ActiveGroupNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveGroupNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ActiveGroupNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveGroupNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public ActiveGroupNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}