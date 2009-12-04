using System.IO;
using System.Windows.Markup;

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
            this._configuration = LightCoreConfiguration.Instance;
        }

        ///<summary>
        /// Initializes a new instance of <see cref="XamlRegistrationModule" />.
        ///</summary>
        ///<param name="configPath">The path to a xaml config file.</param>
        public XamlRegistrationModule(string configPath)
        {
            using (var file = File.Open(configPath, FileMode.Open))
            {
                this._configuration = (LightCoreConfiguration)XamlReader.Load(file);
            }
        }

        ///<summary>
        /// Initializes a new instance of <see cref="XamlRegistrationModule" />.
        ///</summary>
        ///<param name="configurationStream">The stream containing the configuration file content.</param>
        public XamlRegistrationModule(Stream configurationStream)
        {
            this._configuration = (LightCoreConfiguration)XamlReader.Load(configurationStream);
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The controllerbuilder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            RegistrationLoader
                .Instance
                .Register(containerBuilder, _configuration);
        }
    }
}