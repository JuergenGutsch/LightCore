namespace PeterBucher.AutoFunc.Build
{
    /// <summary>
    /// Represents an abstract registration module for implemenenting custom registrations.
    /// </summary>
    public abstract class RegistrationModule
    {
        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The controllerbuilder.</param>
        public abstract void Register(IContainerBuilder containerBuilder);
    }
}