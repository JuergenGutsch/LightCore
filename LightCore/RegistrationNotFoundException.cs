using System;
#if !DOTNET5_4
using System.Runtime.Serialization;
#endif


namespace LightCore
{
    /// <summary>
    /// Thrown when a mapping not found for resolving a type.
    /// </summary>
#if !DOTNET5_4
    [Serializable]
#endif
    public class RegistrationNotFoundException : Exception
    {
        /// <summary>
        /// The contract type.
        /// </summary>
        public Type ContractType { get; private set; }


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
        /// <param name="contractType">The contract type.</param>
        public RegistrationNotFoundException(string message, Type contractType)
            : base(message)
        {
            ContractType = contractType;
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

#if !DOTNET5_4

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