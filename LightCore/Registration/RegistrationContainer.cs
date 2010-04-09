using System.Collections.Generic;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents a container for registrations.
    /// </summary>
    internal class RegistrationContainer
    {
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
            this.Registrations = new Dictionary<RegistrationKey, RegistrationItem>();
            this.DuplicateRegistrations = new List<RegistrationItem>();
        }
    }
}