using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using PeterBucher.AutoFunc.Fluent;
using PeterBucher.AutoFunc.Mapping;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the implementation for a inversion of control container.
    /// </summary>
    public class AutoFuncContainer : IContainer
    {
        /// <summary>
        /// Holds a dictionary with registered types and their corresponding mapping.
        /// </summary>
        private readonly IDictionary<Type, IMappingItem> _mappings;

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncContainer" />.
        /// </summary>
        public AutoFuncContainer()
        {
            this._mappings = new Dictionary<Type, IMappingItem>();
        }

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="ILifecycleFluent"  /> that exposes methods for lifecycle altering.</returns>
        public ILifecycleFluent Register<TContract, TImplementation>()
        {
            Type typeOfContract = typeof(TContract);
            MappingItem mappingItem;

            if (!this._mappings.ContainsKey(typeOfContract))
            {
                Type typeofImplementation = typeof(TImplementation);
                mappingItem = new MappingItem(typeofImplementation);

                this._mappings.Add(typeOfContract, mappingItem);

                return mappingItem.LifecycleFluent;
            }

            throw new Exception("mapping for this contract allready registered");
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract));
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        private object Resolve(Type typeOfContract)
        {
            IMappingItem mappingItem = _mappings.Where(m => m.Key.Equals(typeOfContract)).SingleOrDefault().Value;

            switch (mappingItem.Lifecycle)
            {
                case Lifecycle.Singleton:
                    if (mappingItem.Instance == null)
                    {
                        this.CreateInstanceFromType(mappingItem.ImplementationType);
                    }

                    return mappingItem.Instance;
            }

            return this.CreateInstanceFromType(mappingItem.ImplementationType);
        }

        /// <summary>
        /// Creates an instance of argument type.
        /// </summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <returns>The instance of given type.</returns>
        private object CreateInstanceFromType(Type implementationType)
        {
            ConstructorInfo[] constructors = implementationType.GetConstructors();
            if (constructors.Length == 0 || constructors.Length == 1 && constructors[0].GetParameters() == null)
            {
                return Activator.CreateInstance(implementationType);
            }

            ConstructorInfo constructorWithDependencies = constructors.OrderByDescending(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters == null || parameters.Count() == 0;
                }).First();

            List<object> parameterResults = new List<object>();

            foreach (var parameter in constructorWithDependencies.GetParameters())
            {
                parameterResults.Add(this.Resolve(parameter.ParameterType));
            }

            return constructorWithDependencies.Invoke(parameterResults.ToArray());
        }
    }
}