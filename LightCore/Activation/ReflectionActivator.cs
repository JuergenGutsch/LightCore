using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using LightCore.Activation.Components;
using LightCore.ExtensionMethods.System;
using LightCore.Registration;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an reflection instance activator.
    /// </summary>
    internal class ReflectionActivator : IActivator
    {
        /// <summary>
        /// Selector for dependency parameters / types.
        /// </summary>
        private readonly Func<Type, bool> _dependencyTypeSelector;

        /// <summary>
        /// The constructor selector.
        /// </summary>
        private readonly ConstructorSelector _constructorSelector;

        /// <summary>
        /// The argument collector.
        /// </summary>
        private readonly ArgumentCollector _argumentCollector;

        /// <summary>
        /// The implementation type.
        /// </summary>
        private readonly Type _implementationType;

        /// <summary>
        /// The cached constructor.
        /// </summary>
        private ConstructorInfo _cachedConstructor;

        /// <summary>
        /// The cached constructor arguments.
        /// </summary>
        private object[] _cachedArguments;

        /// <summary>
        /// A reference to the container to resolve inner dependencies.
        /// </summary>
        private Container _container;

        ///<summary>
        /// Creates a new instance of <see cref="ReflectionActivator" />.
        ///</summary>
        ///<param name="implementationType"></param>
        internal ReflectionActivator(Type implementationType)
        {
            this._implementationType = implementationType;
            this._constructorSelector = new ConstructorSelector();
            this._argumentCollector = new ArgumentCollector();

            // Setup selectors.
            this._dependencyTypeSelector = (Type parameterType) =>
                                           this._container.ContractIsRegistered(parameterType)
                                           || this._container.OpenGenericContractIsRegistered(parameterType)
                                           || this.IsRegisteredGenericEnumerable(parameterType);
        }

        /// <summary>
        /// Checks whether a parameter is typeo of IEnumerable{T}, where {T} is a registered contract.
        /// </summary>
        /// <param name="parameterType">The parameter type.</param>
        /// <returns><true /> if the parameter is a registered type within an generic enumerable instance.</returns>
        private bool IsRegisteredGenericEnumerable(Type parameterType)
        {
            return parameterType.IsGenericEnumerable()
                && this._container.ContractIsRegistered(parameterType.GetGenericArguments().FirstOrDefault());
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="runtimeArguments">The runtime arguments.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(Container container, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
        {
            this._container = container;

            int countOfRuntimeArguments = runtimeArguments.CountOfAllArguments;

            if (_cachedConstructor != null && countOfRuntimeArguments == 0)
            {
                return _cachedConstructor.Invoke(this._cachedArguments);
            }

            var constructors = this._implementationType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ConstructorInfo finalConstructor = this._constructorSelector.SelectConstructor(this._dependencyTypeSelector, constructors, arguments, runtimeArguments);

            this._cachedConstructor = finalConstructor;

            if (this._cachedArguments == null || countOfRuntimeArguments > 0)
            {
                this._argumentCollector.DependencyTypeSelector = this._dependencyTypeSelector;
                this._argumentCollector.Parameters = this._cachedConstructor.GetParameters();
                this._argumentCollector.Arguments = arguments;
                this._argumentCollector.RuntimeArguments = runtimeArguments;
                this._argumentCollector.DependencyResolver = t => this.ResolveDependency(t);

                this._cachedArguments = this._argumentCollector.CollectArguments();
            }

            return this._cachedConstructor.Invoke(this._cachedArguments);
        }

        /// <summary>
        /// Resolves a dependency.
        /// </summary>
        /// <param name="parameterType">The parameter type.</param>
        /// <returns>The resolved dependecy.</returns>
        private object ResolveDependency(Type parameterType)
        {
            if (parameterType.IsGenericEnumerable())
            {
                Type genericArgument = parameterType
                    .GetGenericArguments()
                    .FirstOrDefault();

                object[] resolvedInstances = this._container.ResolveAll(genericArgument).ToArray();

                Type openListType = typeof(List<>);
                Type closedListType = openListType.MakeGenericType(genericArgument);

                var list = (IList)Activator.CreateInstance(closedListType);

                Array.ForEach(resolvedInstances, instance => list.Add(instance));

                return list;
            }

            return this._container.Resolve(parameterType);
        }
    }
}