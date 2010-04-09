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
        /// Holds the registration container.
        /// </summary>
        private readonly RegistrationContainer _registrationContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="Container" />.
        /// <param name="registrationContainer">The registrations for this container.</param>
        /// </summary>
        internal Container(RegistrationContainer registrationContainer)
        {
            // Save registrations.
            this._registrationContainer = registrationContainer;

            // Register the container itself for service locator reasons.
            var registrationKey = new RegistrationKey(typeof(IContainer));

            // The container is already registered from external.
            if (this._registrationContainer.Registrations.ContainsKey(registrationKey))
            {
                this._registrationContainer.Registrations.Remove(registrationKey);
            }

            var registrationItem = new RegistrationItem(registrationKey)
                                       {
                                           Activator = new InstanceActivator<IContainer>(this),
                                           Lifecycle = new SingletonLifecycle()
                                       };

            this._registrationContainer.Registrations.Add(new KeyValuePair<RegistrationKey, RegistrationItem>(registrationKey, registrationItem));
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract));
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="arguments">The constructor arguments.</param>
        ///<typeparam name="TContract">The type of the contract.</typeparam>
        ///<returns>The resolved instance as <typeparamref name="TContract"/></returns>.
        public TContract Resolve<TContract>(params object[] arguments)
        {
            return this.Resolve<TContract>(arguments.ToList());
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="arguments">The constructor arguments.</param>
        ///<typeparam name="TContract">The type of the contract.</typeparam>
        ///<returns>The resolved instance as <typeparamref name="TContract"/></returns>.
        public TContract Resolve<TContract>(IEnumerable<object> arguments)
        {
            return (TContract)this.Resolve(typeof(TContract), arguments);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts) with named constructor arguments.
        /// </summary>
        /// <param name="namedArguments">The  named constructor arguments.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/></returns>
        public TContract Resolve<TContract>(IDictionary<string, object> namedArguments)
        {
            return (TContract)this.Resolve(typeof(TContract), namedArguments);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <returns>The resolved instance as object.</returns>
        public object Resolve(Type contractType)
        {
            if (contractType == null)
            {
                throw new ArgumentNullException("contractType");
            }

            var key = new RegistrationKey(contractType);

            RegistrationItem registrationItem;

            if (this._registrationContainer.Registrations.TryGetValue(key, out registrationItem))
            {
                // Activate existing registration.
                return this.Resolve(registrationItem);
            }

            if (contractType.IsGenericType && OpenGenericContractIsRegistered(contractType))
            {
                Type[] genericArguments = contractType.GetGenericArguments();

                // Get the Registration for the open generic type.
                key = new RegistrationKey(contractType.GetGenericTypeDefinition());
                registrationItem = this._registrationContainer.Registrations[key];

                // Register close generic type on-the-fly.
                key = new RegistrationKey(registrationItem.Key.ContractType.MakeGenericType(genericArguments));

                var activator = new ReflectionActivator(registrationItem.ImplementationType.MakeGenericType(genericArguments));

                registrationItem = new RegistrationItem(key)
                {
                    Activator = activator,
                    Lifecycle = registrationItem.Lifecycle
                };

                this._registrationContainer.Registrations.Add(registrationItem.Key, registrationItem);

                return this.Resolve(registrationItem);
            }

            // No registration found for this type.
            if (!contractType.IsConcreteType())
            {
                throw new RegistrationNotFoundException(
                    Resources.RegistrationForContractAndNameNotFoundFormat);
            }

            // On-the-fly registration of concrete types. Equivalent to new-operator.
            registrationItem = new RegistrationItem(key)
                                   {
                                       Activator = new ReflectionActivator(contractType),
                                       Lifecycle = new TransientLifecycle()
                                   };

            return this.Resolve(registrationItem);
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        public object Resolve(Type contractType, params object[] arguments)
        {
            return this.Resolve(contractType, (IEnumerable<object>)arguments);
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        public object Resolve(Type contractType, IEnumerable<object> arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            return this.ResolveWithArguments(contractType, arguments, null);
        }

        /// <summary>
        /// Resolves a contract (include subcontract) with named constructor arguments.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="namedArguments">The named constructor arguments.</param>
        /// <returns>The resolved instance as object.</returns>
        public object Resolve(Type contractType, IDictionary<string, object> namedArguments)
        {
            if (namedArguments == null)
            {
                throw new ArgumentNullException("namedArguments");
            }

            return this.ResolveWithArguments(contractType, null, namedArguments);
        }

        /// <summary>
        /// Resolves a dependency with arguments internally.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="namedArguments">The named arguments.</param>
        /// <returns></returns>
        private object ResolveWithArguments(Type contractType, IEnumerable<object> arguments, IDictionary<string, object> namedArguments)
        {
            var key = new RegistrationKey(contractType);

            RegistrationItem registration;

            if (this._registrationContainer.Registrations.TryGetValue(key, out registration))
            {
                if(arguments != null)
                {
                    registration.RuntimeArguments.AddToAnonymousArguments(arguments);
                }

                if(namedArguments != null)
                {
                    registration.RuntimeArguments.AddToNamedArguments(namedArguments);
                }
            }

            return this.Resolve(contractType);
        }

        /// <summary>
        /// Resolves a dependency internally with a registrationItem.
        /// </summary>
        /// <param name="registrationItem">The registrationItem to activate.</param>
        /// <returns>The resolved instance.</returns>
        private object Resolve(RegistrationItem registrationItem)
        {
            object instance = registrationItem.ActivateInstance(this);

            // Clear all runtime arguments on registration.
            registrationItem.RuntimeArguments.AnonymousArguments = null;
            registrationItem.RuntimeArguments.NamedArguments = null;

            return instance;
        }

        /// <summary>
        /// Checks whether an open generic type, taken from the closed type (contractType) is registered or not.
        /// (This makes possible to use open generic types and also closed generic types at once.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the open generic type is registered, otherwise <value>false</value>.</returns>
        internal bool OpenGenericContractIsRegistered(Type contractType)
        {
            return contractType.IsGenericTypeDefinition || contractType.IsGenericType
                                                             &&
                                                             this.ContractIsRegistered(contractType.GetGenericTypeDefinition());
        }

        /// <summary>
        /// Resolves all contracts of type {TContract}.
        /// </summary>
        /// <typeparam name="TContract">The contract type contraining the result.</typeparam>
        /// <returns>The resolved instances</returns>
        public IEnumerable<TContract> ResolveAll<TContract>()
        {
            return this.ResolveAll(typeof(TContract)).Cast<TContract>();
        }

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll()
        {
            return this._registrationContainer
                .Registrations
                .Values
                .Concat(this._registrationContainer.DuplicateRegistrations)
                .Select(registration => this.Resolve(registration));
        }

        /// <summary>
        /// Resolves all contract of type <paramref name="contractType"/>.
        /// </summary>
        /// <param name="contractType">The contract type contraining the result.</param>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll(Type contractType)
        {
            return this._registrationContainer
                .Registrations
                .Values
                .Concat(this._registrationContainer.DuplicateRegistrations)
                .Where(r => r.Key.ContractType == contractType)
                .Select(registration => this.Resolve(registration));
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
                                                   p => this.ContractIsRegistered(p.PropertyType) || OpenGenericContractIsRegistered(p.PropertyType),
                                                   p => p.GetIndexParameters().Length == 0
                                               };

            var validProperties = properties.Where(
                validPropertiesSelectors.Aggregate((current, next) => p => current(p) && next(p)));

            validProperties.ForEach(p => p.SetValue(instance, this.Resolve(p.PropertyType), null));
        }

        /// <summary>
        /// Determines whether a contracttype is registered or not.
        /// </summary>
        /// <param name="contractType">The type of contract.</param>
        /// <returns><value>true</value> if an registration with the contracttype found, otherwise <value>false</value>.</returns>
        internal bool ContractIsRegistered(Type contractType)
        {
            if (contractType == null)
            {
                return false;
            }

            return this._registrationContainer
                .Registrations
                .Values
                .Concat(this._registrationContainer.DuplicateRegistrations)
                .Any(registration => registration.Key.ContractType == contractType);
        }
    }
}