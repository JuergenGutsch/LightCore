using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns>The builded container.</returns>
        IContainer Build();

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes methods for LifeTime altering.</returns>
        IFluentRegistration Register<TContract, TImplementation>();
    }
}