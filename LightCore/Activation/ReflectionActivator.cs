using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Properties;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an reflection instance activator.
    /// </summary>
    public class ReflectionActivator : IActivator
    {
        /// <summary>
        /// Selector for dependency parameters.
        /// </summary>
        private readonly Func<ParameterInfo, bool> _dependencyParameterSelector;

        /// <summary>
        /// Selector for non dependency parameters.
        /// </summary>
        private readonly Func<ParameterInfo, bool> _nonDependencyParameterSelector;

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
        public ReflectionActivator(Type implementationType)
        {
            this._implementationType = implementationType;

            // Setup selectors.
            this._dependencyParameterSelector = p => p.ParameterType.IsInterface || p.ParameterType.IsAbstract;
            this._nonDependencyParameterSelector = p => !this._dependencyParameterSelector(p);
        }

        /// <summary>
        /// Activates an instance with given arguments.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The activated instance.</returns>
        public object ActivateInstance(Container container, IEnumerable<object> arguments)
        {
            this._container = container;

            if (_cachedConstructor != null)
            {
                return _cachedConstructor.Invoke(this._cachedArguments);
            }


            var constructors = this._implementationType.GetConstructors();
            bool onlyDefaultConstructorAvailable = constructors.Length == 1 &&
                                                   constructors[0].GetParameters().Length == 0;

            // Use the default constructor.
            if (onlyDefaultConstructorAvailable)
            {
                return Activator.CreateInstance(this._implementationType);
            }

            // Select constructor that matches the given arguments.
            if (arguments != null)
            {
                return CreateInstanceWithArguments(this._implementationType, constructors, arguments.ToArray());
            }

            // Select the constructor with most parameters (dependencies).
            var cachedConstructorWithMostParameters = constructors.OrderByDescending(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters != null && parameters.Count() > 0;
                }).First();

            // Invoke constructor with most dependencies and return it to the caller.
            return this.InvokeConstructor(cachedConstructorWithMostParameters, null);
        }

        /// <summary>
        /// Creates an instance with arguments.
        /// </summary>
        /// <param name="implementationType">The imlementation type.</param>
        /// <param name="constructors">The constructor candidates.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The created instance.</returns>
        private object CreateInstanceWithArguments(Type implementationType, IEnumerable<ConstructorInfo> constructors, object[] arguments)
        {
            var constructorCandidates = constructors.Where(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters != null && parameters.Where(this._nonDependencyParameterSelector).Count() == arguments.Count();
                });

            // Only one constructor with same parameter count.
            if (constructorCandidates.Count() == 1)
            {
                return this.InvokeConstructor(constructorCandidates.First(), arguments);
            }

            // Find the right constructor according the parameter types.
            foreach (var constructor in constructorCandidates.OrderByDescending(c => c.GetParameters().Count()))
            {
                if (ConstructorParameterTypesMatch(constructor.GetParameters().Where(this._nonDependencyParameterSelector).ToArray(), arguments))
                {
                    return this.InvokeConstructor(constructor, arguments);
                }
            }

            // No constructor found.
            throw new ResolutionFailedException(Resources.ConstructorNotFoundFormat.FormatWith(implementationType.Name));
        }

        /// <summary>
        /// Invokes a constructor with optional given arguments.
        /// Automatically detects the right resolved arguments and arguments,
        /// and injects these into the constructor invocation.
        /// </summary>
        /// <param name="constructor">The constructor to invoke.</param>
        /// <param name="arguments">The optional arguments.</param>
        /// <returns>The resolved object.</returns>
        private object InvokeConstructor(ConstructorInfo constructor, IEnumerable<object> arguments)
        {
            ParameterInfo[] parameters = constructor.GetParameters();

            // If there are depdendency parameters, resolve these.
            if (parameters.Any(_dependencyParameterSelector))
            {
                var dependencyParameters = parameters.Where(_dependencyParameterSelector);
                var resolvedDependencies = dependencyParameters.Convert(p => this._container.Resolve(p.ParameterType));

                // If there are arguments, concat at the end.
                if (arguments != null)
                {
                    resolvedDependencies = resolvedDependencies.Concat(arguments);
                }

                this._cachedConstructor = constructor;
                this._cachedArguments = resolvedDependencies.ToArray();

                return constructor.Invoke(this._cachedArguments);
            }

            this._cachedArguments = arguments.ToArray();

            // There are only non depdency arguments.
            return constructor.Invoke(this._cachedArguments);
        }

        /// <summary>
        /// Checks whether the types of parameter infos and arguments matches.
        /// Ignores depdency parameters at the beginning (increment the index from begining on).
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns><value>true</value> if the parameter and argument types match, otherwise <value>false</value>.</returns>
        private bool ConstructorParameterTypesMatch(ParameterInfo[] parameters, object[] arguments)
        {
            int startIndex = 0;
            var depdendencyParameters = parameters.Where(_dependencyParameterSelector);

            if (depdendencyParameters.Any())
            {
                startIndex = depdendencyParameters.Count();
            }

            for (int i = startIndex; i < arguments.Length; i++)
            {
                if (parameters[i].ParameterType != arguments[i].GetType())
                {
                    return false;
                }
            }

            return true;
        }
    }
}