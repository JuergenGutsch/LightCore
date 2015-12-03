#if !DOTNET5_4
using System;
using System.Runtime.Serialization;

namespace LightCore.Configuration
{
    /// <summary>
    /// Thrown when the container accessor container accessor is not implemented.
    /// </summary>
    [Serializable]
    public class XamlRegistrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XamlRegistrationException"/> type.
        /// </summary>
        public XamlRegistrationException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public XamlRegistrationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlRegistrationException"/> type.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public XamlRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlRegistrationException"/> type.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected XamlRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
#endif