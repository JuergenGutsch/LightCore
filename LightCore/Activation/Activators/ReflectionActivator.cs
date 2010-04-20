using System;
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
        /// The implementation type.
        /// </summary>
        private readonly Type _implementationType;

        /// <summary>
        /// The cached constructor arguments.
        /// </summary>
        private object[] _cachedArguments;

        ///<summary>
        /// Creates a new instance of <see cref="ReflectionActivator" />.
        ///</summary>
        ///<param name="implementationType">The implementation type.</param>
        internal ReflectionActivator(Type implementationType)
            : this(implementationType, new ConstructorSelector(), new ArgumentCollector())
        {

        }

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

            if (_cachedConstructor != null && countOfRuntimeArguments == 0)
            {
                return _cachedConstructor.Invoke(this._cachedArguments);
            }

            var constructors = _implementationType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ConstructorInfo finalConstructor = this._constructorSelector.SelectConstructor(constructors, resolutionContext);

            this._cachedConstructor = finalConstructor;

            if (this._cachedArguments == null || countOfRuntimeArguments > 0)
            {
                this._cachedArguments =
                    this._argumentCollector.CollectArguments(
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
    }
}