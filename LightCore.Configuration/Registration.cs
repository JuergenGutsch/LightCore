using System.Collections.Generic;

namespace LightCore.Configuration
{
    /// <summary>
    ///     Represents a registration configuration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="Registration" />.
        /// </summary>
        public Registration()
        {
            Arguments = new List<Argument>();
        }

        /// <summary>
        ///     Gets or sets whether the registration group is enabled or not.
        /// </summary>
        public string Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the identifier for the contract type.
        /// </summary>
        public string ContractType { get; set; }

        /// <summary>
        ///     Gets or sets the identifier for the implementation type.
        /// </summary>
        public string ImplementationType { get; set; }

        /// <summary>
        ///     Gets or sets the lifecycle
        /// </summary>
        public string Lifecycle { get; set; }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        public List<Argument> Arguments { get; set; }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ContractType: '{ContractType}', ImplementationType: '{ImplementationType}', Arguments: '{Arguments}', Lifecycle: '{Lifecycle}'";
        }
    }
}