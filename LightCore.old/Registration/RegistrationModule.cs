namespace LightCore.Registration
{
    /// <summary>
    /// Represents an abstract registration module for implementing custom registrations.
    /// </summary>
    public abstract class RegistrationModule
    {
        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The ContainerBuilder.</param>
        public abstract void Register(IContainerBuilder containerBuilder);
    }
}