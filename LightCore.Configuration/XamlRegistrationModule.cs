#if !DOTNET5_4
using System;
using System.IO;
using System.Windows.Markup;

using LightCore.Configuration.Properties;
using LightCore.Registration;

namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a xaml registration module for LightCore.
    /// </summary>
    public class XamlRegistrationModule : RegistrationModule
    {
        private readonly LightCoreConfiguration _configuration;

        ///<summary>
        /// Initializes a new instance of <see cref="XamlRegistrationModule" />.
        /// Uses the default app.config or web.config for loading the configuration.
        ///</summary>
        public XamlRegistrationModule()
        {
            _configuration = LightCoreConfiguration.Instance;
        }

        ///<summary>
        /// Initializes a new instance of <see cref="XamlRegistrationModule" />.
        ///</summary>
        ///<param name="configurationPath">The path to a xaml config file.</param>
        public XamlRegistrationModule(string configurationPath)
        {
            if (!File.Exists(configurationPath))
            {
                throw new FileNotFoundException(string.Format(Resources.ConfigurationFileNotFoundFormat, configurationPath));
            }

            using (var file = File.Open(configurationPath, FileMode.Open))
            {
                _configuration = (LightCoreConfiguration)XamlReader.Load(file);
            }
        }

        ///<summary>
        /// Initializes a new instance of <see cref="XamlRegistrationModule" />.
        ///</summary>
        ///<param name="configurationStream">The stream containing the configuration file content.</param>
        public XamlRegistrationModule(Stream configurationStream)
        {
            if (configurationStream.Length == 0)
            {
                throw new ArgumentException(string.Format(Resources.BadStreamContent, configurationStream));
            }

            _configuration = (LightCoreConfiguration)XamlReader.Load(configurationStream);
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The containerbuilder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            RegistrationLoader
                .Instance
                .Register(containerBuilder, _configuration);
        }
    }
}
#endif