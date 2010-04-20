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
        /// Containes the unique registrations.
        /// </summary>
        IDictionary<Type, RegistrationItem> Registrations { get; set; }

        /// <summary>
        /// Contains the duplicate registrations, e.g. plugins.
        /// </summary>
        IList<RegistrationItem> DuplicateRegistrations { get; set; }

        /// <summary>
        /// Contains all registrations.
        /// </summary>
        IEnumerable<RegistrationItem> AllRegistrations { get; }

        /// <summary>
        /// Contains all registration sources.
        /// </summary>
        IList<IRegistrationSource> RegistrationSources { get; set; }

        /// <summary>
        /// Determines whether a contracttype is at any place, registered / supported by the container, or not.
        /// Searches registrations in all locations.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if a registration with the contracttype found, or supported. Otherwise <value>false</value>.</returns>
        bool IsRegistered(Type contractType);

        /// <summary>
        /// Determines whether a contracttype is supported by registration sources.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the type is supported by a registration source, otherwise <value>false</value>.</returns>
        bool IsSupportedByRegistrationSource(Type contractType);
    }
}