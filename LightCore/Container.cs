using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.Activation;
using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Lifecycle;
using LightCore.Properties;
using LightCore.Registration;

namespace LightCore
{
    /// <summary>
    /// Represents the implementation for an inversion of control container.
    /// </summary>
    internal class Container : IContainer
    {
        /// <summary>
        /// Holds a dictionary with registered registration keys and their corresponding registrations.
        /// </summary>
        private readonly IDictionary<RegistrationKey, RegistrationItem> _registrations;

        /// <summary>
        /// Initializes a new instance of <see cref="Container" />.
        /// <param name="registrations">The registrations for this container.</param>
        /// </summary>
        internal Container(IDictionary<RegistrationKey, RegistrationItem> registrations)
        {
            // Save registrations.
            this._registrations = registrations;

            // Register the container itself for service locator reasons.
            var registrationKey = new RegistrationKey(typeof (IContainer));
            var registrationItem = new RegistrationItem(registrationKey)
                                       {
                                           Activator = new DelegateActivator<IContainer>(c => this),
                                           Lifecycle = new SingletonLifecycle()
                                       };

            this._registrations.Add(new KeyValuePair<RegistrationKey, RegistrationItem>(registrationKey,
                                                                                        registrationItem));
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
        public object Resolve(Type typeOfContract)
        {
            return this.Resolve(typeOfContract, null);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="name">The name.</param>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        public object Resolve(Type typeOfContract, string name)
        {
            var key = new RegistrationKey(typeOfContract, name);

            RegistrationItem registrationItem;

            if (this._registrations.TryGetValue(key, out registrationItem))
            {
                return registrationItem.ActivateInstance(this);
            }

            if (!typeOfContract.IsConcreteType())
            {
                throw new RegistrationNotFoundException(
                    Resources.RegistrationForContractAndNameNotFoundFormat
                        .FormatWith(
                        typeOfContract.Name,
                        name));
            }

            registrationItem = new RegistrationItem(key)
                                   {
                                       Activator = new ReflectionActivator(typeOfContract),
                                       Lifecycle = new TransientLifecycle()
                                   };

            return registrationItem.ActivateInstance(this);
        }

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <typeparam name="TContract">The contract type contraining the result.</typeparam>
        /// <returns>The resolved instances</returns>
        public IEnumerable<TContract> ResolveAll<TContract>()
        {
            foreach (object instance in this.ResolveAll(typeof(TContract)))
            {
                yield return (TContract)instance;
            }
        }

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll()
        {
            foreach (var registration in this._registrations)
            {
                yield return this.Resolve(registration.Key.ContractType, registration.Key.Name);
            }
        }

        /// <summary>
        /// Resolves all contracts based on a contracttype.
        /// </summary>
        /// <param name="contractType">The contract type contraining the result.</param>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll(Type contractType)
        {
            foreach (var registration in this._registrations.Where(r => r.Key.ContractType == contractType))
            {
                yield return this.Resolve(contractType, registration.Key.Name);
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
        internal bool IsRegistered(Type typeOfContract)
        {
            if(typeOfContract == null)
            {
                return false;
            }

            var key = new RegistrationKey(typeOfContract);

            return this._registrations.ContainsKey(key);
        }
    }
}