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
        /// Resolves all contracts.
        /// </summary>
        /// <param name="predicate">The predicate for the query.</param>
        /// <returns>The resolved instances</returns>
        IEnumerable<object> ResolveAll(Func<Registration, bool> predicate);

        /// <summary>
        /// Injects properties to an existing instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void InjectProperties(object instance);
    }
}