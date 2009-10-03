using System;

namespace LightCore
{
    /// <summary>
    /// Represents the contract for a inversion of control container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        TContract Resolve<TContract>();

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="name">The name given in the registration.</param>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        TContract ResolveNamed<TContract>(string name);

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        object Resolve(Type typeOfContract);

        /// <summary>
        /// Injects properties to an existing instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void InjectProperties(object instance);
    }
}