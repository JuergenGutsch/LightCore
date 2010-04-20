using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Registration.RegistrationSource;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents an accessor interface for registration containers.
    /// </summary>
    internal class RegistrationContainer : IRegistrationContainer
    {
        /// <summary>
        /// Holds the cache of allready visited types for speed.
        /// </summary>
        private readonly IDictionary<Type, bool> _registeredCache;

        /// <summary>
        /// Containes the unique registrations.
        /// </summary>
        public IDictionary<Type, RegistrationItem> Registrations
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the duplicate registrations, e.g. plugins.
        /// </summary>
        public IList<RegistrationItem> DuplicateRegistrations
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RegistrationContainer" />.
        /// </summary>
        internal RegistrationContainer()
        {
            this._registeredCache = new Dictionary<Type, bool>();

            this.Registrations = new Dictionary<Type, RegistrationItem>();
            this.DuplicateRegistrations = new List<RegistrationItem>();
            this.RegistrationSources = new List<IRegistrationSource>();
        }

        /// <summary>
        /// Gets all registrations. (Without IRegistrationSource{T}).
        /// </summary>
        public IEnumerable<RegistrationItem> AllRegistrations
        {
            get
            {
                return this.Registrations.Values.Concat(this.DuplicateRegistrations);
            }
        }

        /// <summary>
        /// Contains all registration sources.
        /// </summary>
        public IList<IRegistrationSource> RegistrationSources
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether a contracttype is registered / supported by the container, or not.
        ///  Search on all locations.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if a registration with the contracttype found, or supported. Otherwise <value>false</value>.</returns>
        public bool IsRegistered(Type contractType)
        {
            bool isRegistered;

            if (!this._registeredCache.TryGetValue(contractType, out isRegistered))
            {
                isRegistered = this.AllRegistrations.Any(registration => registration.ContractType == contractType);

                this._registeredCache.Add(contractType, isRegistered);
            }

            return isRegistered;
        }

        /// <summary>
        /// Determines whether a contracttype is supported by registration sources.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the type is supported by a registration source, otherwise <value>false</value>.</returns>
        public bool IsSupportedByRegistrationSource(Type contractType)
        {
            return this.RegistrationSources
                       .Select(registrationSource => registrationSource.SourceSupportsTypeSelector)
                       .Any(sourceSupportsTypeSelector => sourceSupportsTypeSelector(contractType));
        }
    }
}