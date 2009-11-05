using System.Collections.Generic;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a registration group.
    /// </summary>
    public class RegistrationGroup
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
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
    }
}