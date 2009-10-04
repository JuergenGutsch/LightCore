using System.Collections.Generic;
using System.Configuration;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents the current configuration.
    /// </summary>
    public class LightCoreConfiguration
    {
        public LightCoreConfigurationDefaults Defaults
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the registrations.
        /// </summary>
        public List<Registration> Registrations
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LightCoreConfiguration" /> type.
        /// </summary>
        public LightCoreConfiguration()
        {
            this.Registrations = new List<Registration>();
        }

        /// <summary>
        /// Gets the configuration instance.
        /// </summary>
        /// <value>The configuration instance.</value>
        public static LightCoreConfiguration Instance
        {
            get
            {
                var configSectionHandler =
                    (XamlConfigSectionHandler)ConfigurationManager.GetSection("LightCoreConfiguration");

                return configSectionHandler.GetInstance<LightCoreConfiguration>();
            }
        }
    }
}