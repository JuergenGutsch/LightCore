
using System.Collections.Generic;
#if !DNXCORE50
using System;
using System.Configuration;
using LightCore.Configuration.Properties;
#endif
using LightCore.Lifecycle;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents the current configuration.
    /// </summary>
    public class LightCoreConfiguration
    {
        ///<summary>
        /// Gets or sets the default lifecycle.
        ///</summary>
        public string DefaultLifecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the active registration groups.
        /// </summary>
        public string ActiveRegistrationGroups
        {
            get;
            set;
        }

        ///<summary>
        /// Gets or sets the type aliases.
        ///</summary>
        public List<TypeAlias> TypeAliases
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
        /// Represents the registration groups.
        /// </summary>
        public List<RegistrationGroup> RegistrationGroups
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LightCoreConfiguration" /> type.
        /// </summary>
        public LightCoreConfiguration()
        {
            TypeAliases = new List<TypeAlias>();

            // Load default lifecycle type alias.
            TypeAliases.AddRange(new List<TypeAlias>
                                          {
                                              new TypeAlias
                                                  {
                                                      Alias = "Transient",
                                                      Type = typeof (TransientLifecycle).AssemblyQualifiedName
                                                  },
                                              new TypeAlias
                                                  {
                                                      Alias = "Singleton",
                                                      Type = typeof (SingletonLifecycle).AssemblyQualifiedName
                                                  },
                                              new TypeAlias
                                                  {
                                                      Alias = "ThreadSingleton",
                                                      Type = typeof (ThreadSingletonLifecycle).AssemblyQualifiedName
                                                  }
            });

            // Load default argument type alias.
            TypeAliases.AddRange(new List<TypeAlias>
                                          {
                                              new TypeAlias
                                                  {
                                                      Alias = "Int32, Integer, int",
                                                      Type = "System.Int32, mscorlib"
                                                  },
                                              new TypeAlias
                                                  {
                                                      Alias = "String, string",
                                                      Type = "System.String, mscorlib"
                                                  },
                                              new TypeAlias
                                                  {
                                                      Alias = "Boolean, bool",
                                                      Type = "System.Boolean, mscorlib"
                                                  },
                                              new TypeAlias
                                                  {
                                                      Alias = "Guid",
                                                      Type = "System.Guid, mscorlib"
                                                  },
                                          });

            Registrations = new List<Registration>();
            RegistrationGroups = new List<RegistrationGroup>();
        }

#if !DNXCORE50
        /// <summary>
        /// Gets the configuration instance.
        /// </summary>
        /// <value>The configuration instance.</value>
        internal static LightCoreConfiguration Instance
        {
            get
            {
                const string sectionName = "LightCoreConfiguration";

                var configSectionHandler =
                    (XamlConfigSectionHandler)ConfigurationManager.GetSection(sectionName);

                if (configSectionHandler == null)
                {
                    throw new ArgumentException(string.Format(Resources.SectionNotFoundFormat, sectionName));
                }

                return configSectionHandler.GetInstance<LightCoreConfiguration>();
            }
        }
#endif
    }
}