using System;
using System.Collections.Generic;

using LightCore.Registration.RegistrationSource;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents an accessor interface for registration containers.
    /// </summary>
    internal interface IRegistrationContainer
    {
        /// <summary>
        /// Contains all registrations.
        /// </summary>
        IEnumerable<RegistrationItem> AllRegistrations { get; }

        /// <summary>
        /// Contains all registration sources.
        /// </summary>
        IList<IRegistrationSource> RegistrationSources { get; set; }

        /// <summary>
        /// Try get a registration based upon a contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="registrationItem">The registration item.</param>
        /// <returns>The registrationitem.</returns>
        bool TryGetRegistration(Type contractType, out RegistrationItem registrationItem);

        /// <summary>
        /// Try get a registration based upon a contract type.
        /// </summary>
        /// <param name="predicate">the predicate.</param>
        /// <returns>The registrationitem.</returns>
        RegistrationItem GetRegistration(Func<RegistrationItem, bool> predicate);

        /// <summary>
        /// Determines whether a contracttype is at any place, registered / supported by the container, or not.
        /// Searches registrations in all locations.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if a registration with the contracttype found, or supported. Otherwise <value>false</value>.</returns>
        bool HasRegistration(Type contractType);

        /// <summary>
        /// Determines whether a contracttype is registered as duplicate (many of them).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <returns><value>true</value> if this type is registered many times.</returns>
        bool HasDuplicateRegistration(Type contractType);

        /// <summary>
        /// Adds a registration.
        /// </summary>
        /// <param name="registration">The registration.</param>
        void AddRegistration(RegistrationItem registration);

        /// <summary>
        /// Removes a registration.
        /// </summary>
        /// <param name="contractType"></param>
        void RemoveRegistration(Type contractType);

        /// <summary>
        /// Determines whether a contracttype is supported by registration sources.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the type is supported by a registration source, otherwise <value>false</value>.</returns>
        bool IsSupportedByRegistrationSource(Type contractType);
    }
}
