using System.Collections.Generic;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a registration group.
    /// </summary>
    public class RegistrationGroup
    {
        /// <summary>
        /// Gets or sets the registration group name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the registration group is enabled or not.
        /// </summary>
        public string Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the registrations associated with this group.
        /// </summary>
        public List<Registration> Registrations
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RegistrationGroup" />.
        /// </summary>
        public RegistrationGroup()
        {
            Registrations = new List<Registration>();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Name: '{Name}', Enabled: '{Enabled}'";
        }
    }
}