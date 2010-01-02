using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents an reflection instance activator.
    /// </summary>
    internal class ReflectionActivator : IActivator
    {
        /// <summary>
        /// Selector for dependency parameters.
        /// </summary>
        private readonly Func<ParameterInfo, bool> _dependencyParameterSelector;

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

            // Setup selectors.
            this._dependencyParameterSelector =
                delegate(ParameterInfo p)
                    {
                        Type parameterType = p.ParameterType;

                        return this._container.ContractIsRegistered(parameterType)
                               || this._container.OpenGenericContractIsRegistered(parameterType)
                               || this.IsRegisteredGenericEnumerable(p);
                    };
        }

        /// <summary>
        /// Checks whether a parameter is typeo of IEnumerable{T}, where {T} is a registered contract.
        /// </summary>
        /// <param name="parameter">The parameter candidate.</param>
        /// <returns><true /> if the parameter is a registered type within an generic enumerable instance.</returns>
        private bool IsRegisteredGenericEnumerable(ParameterInfo parameter)
        {
            return this.IsGenericEnumerable(parameter.ParameterType)
                   && this._container.ContractIsRegistered(parameter.ParameterType.GetGenericArguments()
                                                       .FirstOrDefault());
        }

        /// <summary>
        /// Checks whether a given parameterType is type of generic enumerable.
        /// </summary>
        /// <param name="parameterType">The parameter type.</param>
        /// <returns><true /> if the parameter type is a generic enumerable, otherwise <false /></returns>
        private bool IsGenericEnumerable(Type parameterType)
        {
            if (!parameterType.IsGenericType)
            {
                return false;
            }

            var typeArguments = parameterType.GetGenericArguments();

            if (typeof(IEnumerable<>).MakeGenericType(typeArguments).IsAssignableFrom(parameterType))
            {
                return true;
            }

            return false;
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
                return this.InvokeDefaultConstructor();
            }

            var constructorsWithParameters = constructors.OrderByDescending(constructor => constructor.GetParameters().Count());
            ConstructorInfo finalConstructor = null;

            // Loop througth all constructors, from the most to the least parameters.
            foreach (ConstructorInfo constructorCandidate in constructorsWithParameters)
            {
                ParameterInfo[] parameters = constructorCandidate.GetParameters();
                var dependencyParameters = parameters.Where(this._dependencyParameterSelector);

                // If there are no arguments at all, use default constructor.
                if (arguments == null && !dependencyParameters.Any())
                {
                    return this.InvokeDefaultConstructor();
                }

                // Parameters and registered dependencies match.
                if (arguments == null && parameters.Count() == dependencyParameters.Count())
                {
                    finalConstructor = constructorCandidate;
                    break;
                }

                // There are arguments, the types matches.
                if (arguments != null && this.ConstructorParameterTypesMatch(parameters, arguments.ToArray()))
                {
                    finalConstructor = constructorCandidate;
                    break;
                }
            }

            this._cachedConstructor = finalConstructor;

            return this.InvokeConstructor(finalConstructor, (arguments != null ? arguments.ToArray() : null));
        }

        /// <summary>
        /// Invokes the default constructor of the implementation type.-
        /// </summary>
        /// <returns>The instance constructed bei default constructor.</returns>
        private object InvokeDefaultConstructor()
        {
            return Activator.CreateInstance(this._implementationType);
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
            int parameterStartIndex = 0;
            var depdendencyParameters = parameters.Where(_dependencyParameterSelector);

            if (depdendencyParameters.Any())
            {
                parameterStartIndex = depdendencyParameters.Count();
            }

            if ((parameters.Count()) - parameterStartIndex != arguments.Count())
            {
                return false;
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if (parameters[parameterStartIndex].ParameterType != arguments[i].GetType())
                {
                    return false;
                }

                parameterStartIndex++;
            }

            return true;
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

                var resolvedDependencies = this.ResolveDependencies(dependencyParameters);

                // If there are arguments, concat at the end.
                if (arguments != null)
                {
                    resolvedDependencies = resolvedDependencies.Concat(arguments);
                }

                this._cachedArguments = resolvedDependencies.ToArray();

                return constructor.Invoke(this._cachedArguments);
            }

            this._cachedArguments = arguments.ToArray();

            return constructor.Invoke(this._cachedArguments);
        }

        /// <summary>
        /// Resolve all dependencies and consider IEnumerable{TContract}.
        /// </summary>
        /// <param name="dependencyParameters"></param>
        /// <returns></returns>
        private IEnumerable<object> ResolveDependencies(IEnumerable<ParameterInfo> dependencyParameters)
        {
            foreach (ParameterInfo parameter in dependencyParameters)
            {
                if (this.IsGenericEnumerable(parameter.ParameterType))
                {
                    Type genericArgument = parameter
                        .ParameterType
                        .GetGenericArguments()
                        .FirstOrDefault();

                    object[] resolvedInstances = this._container.ResolveAll(genericArgument).ToArray();

                    Type openListType = typeof(List<>);
                    Type closedListType = openListType.MakeGenericType(genericArgument);

                    var list = (IList)Activator.CreateInstance(closedListType);

                    Array.ForEach(resolvedInstances, instance => list.Add(instance));

                    yield return list;
                }
                else
                {
                    yield return this._container.Resolve(parameter.ParameterType);
                }
            }
        }
    }
}