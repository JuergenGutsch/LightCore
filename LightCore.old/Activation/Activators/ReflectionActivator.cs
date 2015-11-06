using System;
using System.Linq;
using System.Reflection;

using LightCore.Activation.Components;
using LightCore.ExtensionMethods.System;
using LightCore.Properties;

namespace LightCore.Activation.Activators
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
        private readonly IConstructorSelector _constructorSelector;

        /// <summary>
        /// The argument collector.
        /// </summary>
        private readonly IArgumentCollector _argumentCollector;

        /// <summary>
        /// The cached constructor.
        /// </summary>
        private ConstructorInfo _cachedConstructor;

        /// <summary>
        /// The cached arguments.
        /// </summary>
        private object[] _cachedArguments;

        /// <summary>
        /// The implementation type.
        /// </summary>
        private readonly Type _implementationType;

        ///<summary>
        /// Creates a new instance of <see cref="ReflectionActivator" />.
        ///</summary>
        ///<param name="implementationType">The implementation type.</param>
        ///<param name="constructorSelector">The constructor selector.</param>
        ///<param name="argumentCollector">The argument collector.</param>
        internal ReflectionActivator(Type implementationType, IConstructorSelector constructorSelector, IArgumentCollector argumentCollector)
        {
            this._implementationType = implementationType;
            this._constructorSelector = constructorSelector;
            this._argumentCollector = argumentCollector;
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="resolutionContext">The container.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            var runtimeArguments = resolutionContext.RuntimeArguments;

            if (this._container == null)
            {
                this._container = resolutionContext.Container;
            }

            int countOfRuntimeArguments = runtimeArguments.CountOfAllArguments;

            if (this._cachedConstructor == null || countOfRuntimeArguments > 0)
            {
                var constructors = _implementationType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                _cachedConstructor = this._constructorSelector.SelectConstructor(constructors, resolutionContext);
            }

            if (this._cachedArguments == null || countOfRuntimeArguments > 0 || GetAnyDependencyParameter(resolutionContext))
            {
                this._cachedArguments = this._argumentCollector.CollectArguments(
                        this._container.Resolve,
                        this._cachedConstructor.GetParameters(),
                        resolutionContext);
            }

            if (this._cachedArguments.Length != this._cachedConstructor.GetParameters().Length)
            {
                throw new ResolutionFailedException(
                    Resources.NoSuitableConstructorFoundFormat.FormatWith(_implementationType));
            }

            return this._cachedConstructor.Invoke(this._cachedArguments);
        }

        /// <summary>
        /// Gets <value>true</value> if a dependency parameter exists, otherwise <value>false</value>.
        /// </summary>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns><value>true</value> if a dependency parameter exists, otherwise <value>false</value>.</returns>
        private bool GetAnyDependencyParameter(ResolutionContext resolutionContext)
        {
            return this._cachedConstructor
                .GetParameters()
                .Any(p => resolutionContext.RegistrationContainer.IsRegistered(p.ParameterType)
                          || resolutionContext.RegistrationContainer.IsSupportedByRegistrationSource(p.ParameterType));
        }
    }
}