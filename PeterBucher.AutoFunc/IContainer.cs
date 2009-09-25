using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the contract for a inversion of control container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="ILifecycleFluent"  /> that exposes methods for lifecycle altering.</returns>
        ILifecycleFluent Register<TContract, TImplementation>();
        
        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        TContract Resolve<TContract>();
    }
}