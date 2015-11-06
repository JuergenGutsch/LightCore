using System;

using System.Runtime.Serialization;

namespace LightCore
{
    /// <summary>
    /// Thrown when a mapping not found for resolving a type.
    /// </summary>
    [Serializable]
    public class RegistrationNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        public RegistrationNotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RegistrationNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public RegistrationNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}