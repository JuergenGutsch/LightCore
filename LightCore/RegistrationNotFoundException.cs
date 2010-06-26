using System;

#if !SL2
using System.Runtime.Serialization;
#endif

namespace LightCore
{
    /// <summary>
    /// Thrown when a mapping not found for resolving a type.
    /// </summary>
#if !SL2
    [Serializable]
#endif
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

#if !SL2
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public RegistrationNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationNotFoundException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected RegistrationNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
#endif
    }
}