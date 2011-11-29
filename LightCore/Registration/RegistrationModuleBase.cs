namespace LightCore.Registration
{
    /// <summary>
    /// Represents an abstract registration module for implementing custom registrations.
    /// </summary>
    public abstract class RegistrationModuleBase
    {
        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The ContainerBuilder.</param>
        public abstract void Register(IContainerBuilder containerBuilder);
    }
}