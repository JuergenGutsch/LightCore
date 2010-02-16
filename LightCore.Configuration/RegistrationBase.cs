namespace LightCore.Configuration
{
    /// <summary>
    /// Represents the registration base class.
    /// </summary>
    public abstract class RegistrationBase
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
        /// Gets or sets whether the current registration is enabled or not.
        /// </summary>
        public string Enabled
        {
            get;
            set;
        }
    }
}