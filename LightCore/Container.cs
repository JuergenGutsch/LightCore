using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Properties;

namespace LightCore
{
    /// <summary>
    /// Represents the implementation for an inversion of control container.
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
            // Save registrations.
            this._registrations = registrations;
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract), null);
        }

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        public TContract Resolve<TContract>(string name)
        {
            return (TContract)this.Resolve(typeof(TContract), name);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        internal object Resolve(Type typeOfContract)
        {
            return this.Resolve(typeOfContract, null);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="name">The name.</param>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        private object Resolve(Type typeOfContract, string name)
        {
            var key = new RegistrationKey(typeOfContract, name);

            Registration registration;

            if(!this._registrations.TryGetValue(key, out registration))
            {
                throw new RegistrationNotFoundException(
                    Resources.RegistrationForContractAndNameNotFoundFormat
                        .FormatWith(
                        typeOfContract.Name,
                        name));
            }

            return registration.ActivateInstance(this);
        }

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <param name="predicate">The predicate for the query.</param>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll(Func<Registration, bool> predicate)
        {
            foreach(var registration in this._registrations.Where(r => predicate(r.Value)))
            {
                yield return this.Resolve(registration.Key.ContractType, registration.Key.Name);
            }
        }

        /// <summary>
        /// Injects properties to an existing instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void InjectProperties(object instance)
        {
            Type instanceType = instance.GetType();
            var properties =
                instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            var validPropertiesSelectors = new List<Func<PropertyInfo, bool>>
                                               {
                                                   p => p.PropertyType.IsAbstract || p.PropertyType.IsInterface,
                                                   p => !p.PropertyType.IsValueType,
                                                   p => this.IsRegistered(p.PropertyType),
                                                   p => p.GetIndexParameters().Length == 0
                                               };

            var validProperties = properties.Where(
                validPropertiesSelectors.Aggregate((current, next) => p => current(p) && next(p)));

            validProperties.ForEach(p => p.SetValue(instance, this.Resolve(p.PropertyType), null));
        }

        /// <summary>
        /// Determines whether a contracttype is registered or not.
        /// </summary>
        /// <param name="typeOfContract">The type of contract.</param>
        /// <returns><value>true</value> if an registration with the contracttype found, otherwise <value>false</value>.</returns>
        private bool IsRegistered(Type typeOfContract)
        {
            var key = new RegistrationKey(typeOfContract);

            return this._registrations.ContainsKey(key);
        }
    }
}