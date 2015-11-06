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

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="arguments">The constructor arguments.</param>
        ///<typeparam name="TContract">The type of the contract.</typeparam>
        ///<returns>The resolved instance as <typeparamref name="TContract"/></returns>.
        TContract Resolve<TContract>(params object[] arguments);

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="arguments">The constructor arguments.</param>
        ///<typeparam name="TContract">The type of the contract.</typeparam>
        ///<returns>The resolved instance as <typeparamref name="TContract"/></returns>.
        TContract Resolve<TContract>(IEnumerable<object> arguments);

        /// <summary>
        /// Resolves a contract (include subcontracts) with named constructor arguments.
        /// </summary>
        /// <param name="namedArguments">The  named constructor arguments.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/></returns>
        TContract Resolve<TContract>(IDictionary<string, object> namedArguments);

        /// <summary>
        /// Resolves a contract (include subcontracts) with anonymous named constructor arguments.
        /// </summary>
        /// <param name="namedArguments">The  named constructor arguments.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/></returns>
        TContract Resolve<TContract>(AnonymousArgument namedArguments);

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <returns>The resolved instance as object.</returns>
        object Resolve(Type contractType);

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        object Resolve(Type contractType, params object[] arguments);

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        object Resolve(Type contractType, IEnumerable<object> arguments);

        /// <summary>
        /// Resolves a contract (include subcontract) with named constructor arguments.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="namedArguments">The named constructor arguments.</param>
        /// <returns>The resolved instance as object.</returns>
        object Resolve(Type contractType, IDictionary<string, object> namedArguments);

        /// <summary>
        /// Resolves all contracts of type {TContract}.
        /// </summary>
        /// <typeparam name="TContract">The contract type contraining the result.</typeparam>
        /// <returns>The resolved instances</returns>
        IEnumerable<TContract> ResolveAll<TContract>();

        /// <summary>
        /// Resolves all contract of type <paramref name="contractType"/>.
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