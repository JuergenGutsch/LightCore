using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using LightCore.Activation.Components;
using LightCore.ExtensionMethods.System;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an reflection instance activator.
    /// </summary>
    internal class ReflectionActivator : IActivator
    {
        /// <summary>
        /// The container.
        /// </summary>
        private IContainer _container;

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

        ///<summary>
        /// Creates a new instance of <see cref="ReflectionActivator" />.
        ///</summary>
        ///<param name="implementationType">The implementation type.</param>
        internal ReflectionActivator(Type implementationType)
        {
            this._implementationType = implementationType;
            this._constructorSelector = new ConstructorSelector();
            this._argumentCollector = new ArgumentCollector();
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="resolutionContext">The container.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            if (this._container == null)
            {
                this._container = resolutionContext.Container;
            }

            int countOfRuntimeArguments = resolutionContext.RuntimeArguments.CountOfAllArguments;

            if (_cachedConstructor != null && countOfRuntimeArguments == 0)
            {
                return _cachedConstructor.Invoke(this._cachedArguments);
            }

            var constructors = this._implementationType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            resolutionContext.Arguments = resolutionContext.Arguments;
            resolutionContext.RuntimeArguments = resolutionContext.RuntimeArguments;

            ConstructorInfo finalConstructor = this._constructorSelector.SelectConstructor(constructors, resolutionContext);

            this._cachedConstructor = finalConstructor;

            if (this._cachedArguments == null || countOfRuntimeArguments > 0)
            {
                this._cachedArguments =
                    this._argumentCollector.CollectArguments(
                        this.ResolveDependency,
                        this._cachedConstructor.GetParameters(),
                        resolutionContext);
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