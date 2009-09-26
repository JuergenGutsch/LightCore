using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the implementation for a inversion of control container.
    /// </summary>
    public class Container : IContainer
    {
        /// <summary>
        /// Holds a dictionary with registered registration keys and their corresponding registrations.
        /// </summary>
        private readonly IDictionary<RegistrationKey, Registration> _registrations;

        /// <summary>
        /// Initializes a new instance of <see cref="Container" />.
        /// </summary>
        internal Container(IDictionary<RegistrationKey, Registration> registrations)
        {
            this._registrations = registrations;
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract), null);
        }

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="name">The name given in the registration.</param>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        public TContract ResolveNamed<TContract>(string name)
        {
            return (TContract)this.Resolve(typeof(TContract), name);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="name">The name given in the registration.</param>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        private object Resolve(Type typeOfContract, string name)
        {
            Func<Type, bool> typeSelector = t => t.Equals(typeOfContract);
            Func<string, bool> nameSelector = n => n == name;

            Func<KeyValuePair<RegistrationKey, Registration>, bool> selector =
                r => typeSelector(r.Key.ContractType) && nameSelector(r.Key.Name);

            if (!this._registrations.Any(selector))
            {
                string exceptionMessage = string.Format("registration for contract '{0}' and name '{1}' not found",
                                                        typeOfContract.Name, name);

                throw new RegistrationNotFoundException(exceptionMessage);
            }

            Registration registration = this._registrations.Where(selector).Single().Value;

            switch (registration.Lifecycle)
            {
                case Lifecycle.Singleton:
                    if (registration.Instance == null)
                    {
                        registration.Instance = this.CreateInstanceFromType(registration.ImplementationType);
                    }

                    return registration.Instance;
            }

            return this.CreateInstanceFromType(registration.ImplementationType);
        }

        /// <summary>
        /// Creates an instance of argument type.
        /// </summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <returns>The instance of given type.</returns>
        private object CreateInstanceFromType(Type implementationType)
        {
            ConstructorInfo[] constructors = implementationType.GetConstructors();

            // Use the default constructor.
            if (constructors.Length == 0 || constructors.Length == 1 && constructors[0].GetParameters() == null)
            {
                return Activator.CreateInstance(implementationType);
            }

            // Select the constructor with most parameters (dependencies).
            ConstructorInfo constructorWithDependencies = constructors.OrderByDescending(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters == null || parameters.Count() == 0;
                }).First();

            var parameterResults = new List<object>();

            // Resolve up all dependencies (recursive).
            foreach (var parameter in constructorWithDependencies.GetParameters())
            {
                parameterResults.Add(this.Resolve(parameter.ParameterType, null));
            }

            // Invoke constructor with arguments and return it to the caller.
            return constructorWithDependencies.Invoke(parameterResults.ToArray());
        }
    }
}