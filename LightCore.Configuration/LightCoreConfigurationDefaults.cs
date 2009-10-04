namespace LightCore.Configuration
{
    /// <summary>
    /// Represents the light core configuration defaults.
    /// </summary>
    public class LightCoreConfigurationDefaults
    {
        /// <summary>
        /// Gets or sets the default assembly.
        /// </summary>
        public string DefaultAssembly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default namespace for contracts.
        /// </summary>
        public string DefaultContractNamespace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default namespace for implementations.
        /// </summary>
        public string DefaultImplementationNamespace
        {
            get;
            set;
        }
    }
}