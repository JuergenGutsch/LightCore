namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public interface IFluentRegistration : IFluentInterface
    {
        /// <summary>
        /// Treat the current registration to singleton lifecycle.
        /// </summary>
        IFluentRegistration AsSingleton();

        /// <summary>
        /// Treat the current registration to transient lifecycle.
        /// </summary>
        IFluentRegistration AsTransient();

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        void WithName(string name);
    }
}