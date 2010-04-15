using System;
using System.Collections.Generic;
using System.Linq;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents a container for registrations.
    /// </summary>
    internal class RegistrationContainer
    {
        /// <summary>
        /// Holds the cache of allready visited types for speed.
        /// </summary>
        private readonly IDictionary<Type, bool> _registeredCache;

        /// <summary>
        /// Gets or sets the registration selectors.
        /// </summary>
        internal IList<Func<Type, bool>> RegistrationSelectors
        {
            get;
            set;
        }

        /// <summary>
        /// Containes the unique registrations.
        /// </summary>
        internal IDictionary<RegistrationKey, RegistrationItem> Registrations
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the duplicate registrations, e.g. plugins.
        /// </summary>
        internal IList<RegistrationItem> DuplicateRegistrations
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RegistrationContainer" />.
        /// </summary>
        internal RegistrationContainer()
        {
            this.RegistrationSelectors = new List<Func<Type, bool>>();
            this.Registrations = new Dictionary<RegistrationKey, RegistrationItem>();
            this.DuplicateRegistrations = new List<RegistrationItem>();

            this._registeredCache = new Dictionary<Type, bool>();
        }

        /// <summary>
        /// Determines whether a contracttype is registered / supported by the container, or not.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if a registration with the contracttype found, or supported. Otherwise <value>false</value>.</returns>
        internal bool IsRegisteredOrSupportedContract(Type contractType)
        {
            return this.IsRegisteredContract(contractType) || this.IsSupportedByRegistrationSource(contractType);
        }

        /// <summary>
        /// Determines whether a contracttype is registered or not.
        /// </summary>
        /// <param name="contractType">The type of contract.</param>
        /// <returns><value>true</value> if an registration with the contracttype found, otherwise <value>false</value>.</returns>
        internal bool IsRegisteredContract(Type contractType)
        {
            bool isRegistered;

            if (!this._registeredCache.TryGetValue(contractType, out isRegistered))
            {
                var allRegistrations = this.Registrations.Values.Concat(this.DuplicateRegistrations);

                isRegistered = allRegistrations.Any(registration => registration.Key.ContractType == contractType);

                this._registeredCache.Add(contractType, isRegistered);
            }

            return isRegistered;
        }

        /// <summary>
        /// Determines whether a contracttype is supported by registration sources.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the type is supported by a registration source, otherwise <value>false</value>.</returns>
        internal bool IsSupportedByRegistrationSource(Type contractType)
        {
            return this.RegistrationSelectors.Any(selector => selector(contractType));
        }
    }
}