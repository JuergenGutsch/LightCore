using System;
using System.Collections.Generic;

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
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        TContract Resolve<TContract>();

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        TContract Resolve<TContract>(string name);

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <returns>The resolved instance as object.</returns>
        object Resolve(Type contractType);

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="name">The name.</param>
        /// <returns>The resolved instance as object.</returns>
        object Resolve(Type contractType, string name);

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <typeparam name="TContract">The contract type contraining the result.</typeparam>
        /// <returns>The resolved instances</returns>
        IEnumerable<TContract> ResolveAll<TContract>();

        /// <summary>
        /// Resolves all contracts based on a contracttype.
        /// </summary>
        /// <param name="contractType">The contract type contraining the result.</param>
        /// <returns>The resolved instances</returns>
        IEnumerable<object> ResolveAll(Type contractType);

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <returns>The resolved instances</returns>
        IEnumerable<object> ResolveAll();

        /// <summary>
        /// Injects properties to an existing instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void InjectProperties(object instance);
    }
}