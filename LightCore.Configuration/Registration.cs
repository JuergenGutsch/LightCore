namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a registration configuration.
    /// </summary>
    public class Registration
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
        /// Gets or sets the identifier for the contract type.
        /// </summary>
        public string ContractType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier for the implementation type.
        /// </summary>
        public string ImplementationType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        public string Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lifecycle
        /// </summary>
        public string Lifecycle
        {
            get;
            set;
        }
    }
}