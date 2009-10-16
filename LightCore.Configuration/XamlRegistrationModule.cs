namespace LightCore.Configuration
{
    /// <summary>
    /// Represents a xaml registration module for LightCore.
    /// </summary>
    public class XamlRegistrationModule : RegistrationModule
    {
        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The controllerbuilder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            RegistrationLoader.Instance.Register(containerBuilder, LightCoreConfiguration.Instance);
        }
    }
}