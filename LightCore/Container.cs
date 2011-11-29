using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Lifecycle;
using LightCore.Properties;
using LightCore.Registration;
using LightCore.Registration.RegistrationSource;

namespace LightCore
{
#if NET35
    internal class NET35
    {
        
    }
#endif

#if NET40
    internal class NET40
    {
        
    }
#endif

#if CF35
    internal class CF35
    {
        
    }
#endif

#if SL3
  internal class SL3
  {

  }
#endif

    /// <summary>
    /// Represents the implementation for an inversion of control container.
    /// </summary>
    internal class Container : IContainer
    {
        /// <summary>
        /// Holds the registration container.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private Func<PropertyInfo, bool> _validPropertiesSelector;

        /// <summary> Initializes a new instance of <see cref="Container" />.
        /// <param name="registrationContainer">The registrations for this container.</param>
        /// </summary>
        internal Container(IRegistrationContainer registrationContainer)
        {
            this._registrationContainer = registrationContainer;

            this.CreateValidPropertiesSelector();
            this.RegisterContainer();
        }

        /// <summary>
        /// Creates the valid properties selector and set it to the container.
        /// </summary>
        private void CreateValidPropertiesSelector()
        {
            var validPropertiesSelectors = new List<Func<PropertyInfo, bool>>
                                               {
                                                   p => !p.PropertyType.IsValueType,
                                                   p => this._registrationContainer.HasRegistration(p.PropertyType),
                                                   p => p.GetIndexParameters().Length == 0,
                                                   p => p.CanWrite
                                               };

            this._validPropertiesSelector =
                validPropertiesSelectors.Aggregate((current, next) => p => current(p) && next(p));
        }

        /// <summary>
        /// Register the container itself for service locator reasons. 
        /// </summary>
        private void RegisterContainer()
        {
            var typeOfIContainer = typeof (IContainer);

            // The container is already registered from external.
            if (this._registrationContainer.HasRegistration(typeOfIContainer))
            {
                this._registrationContainer.RemoveRegistration(typeOfIContainer);
            }

            var registrationItem = new RegistrationItem(typeOfIContainer)
                                       {
                                           Activator = new InstanceActivator<IContainer>(this),
                                           Lifecycle = new TransientLifecycle()
                                       };

            this._registrationContainer.AddRegistration(registrationItem);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/>.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract) this.Resolve(typeof (TContract));
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
            return (TContract) this.Resolve(typeof (TContract), arguments);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts) with named constructor arguments.
        /// </summary>
        /// <param name="namedArguments">The  named constructor arguments.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/></returns>
        public TContract Resolve<TContract>(IDictionary<string, object> namedArguments)
        {
            return (TContract) this.Resolve(typeof (TContract), namedArguments);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts) with anonymous named constructor arguments.
        /// </summary>
        /// <param name="namedArguments">The  named constructor arguments.</param>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <typeparamref name="TContract"/></returns>
        public TContract Resolve<TContract>(AnonymousArgument namedArguments)
        {
            return (TContract) this.Resolve(typeof (TContract),
                                            namedArguments.AnonymousType.ToNamedArgumentDictionary());
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <returns>The resolved instance as object.</returns>
        public object Resolve(Type contractType)
        {
            return this.ResolveInternal(contractType, null, null);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="namedArguments">The named arguments.</param>
        /// <returns>The resolved instance as object.</returns>
        private object ResolveInternal(Type contractType, IEnumerable<object> arguments,
                                       IDictionary<string, object> namedArguments)
        {
            RegistrationItem registrationItem;

            if (!this._registrationContainer.TryGetRegistration(contractType, out registrationItem))
            {
                if (this._registrationContainer.HasDuplicateRegistration(contractType))
                {
                    throw new RegistrationNotFoundException(
                        Resources.RegistrationNotFoundBecauseDuplicate.FormatWith(contractType), contractType);
                }

                // No registration found yet, try to create one with available registration sources.
                IRegistrationSource registrationSourceToUse = null;

                for (int i = 0; i < this._registrationContainer.RegistrationSources.Count; i++)
                {
                    var currentRegistrationSource = this._registrationContainer.RegistrationSources[i];

                    if (currentRegistrationSource.SourceSupportsTypeSelector(contractType))
                    {
                        registrationSourceToUse = currentRegistrationSource;
                        break;
                    }
                }

                if (registrationSourceToUse == null)
                {
                    throw new RegistrationNotFoundException(
                        Resources.RegistrationNotFoundFormat.FormatWith(contractType), contractType);
                }

                registrationItem = registrationSourceToUse.GetRegistrationFor(contractType, this);

                this._registrationContainer.AddRegistration(registrationItem);
            }

            // Add runtime arguments to registration.
            this.AddArgumentsToRegistration(registrationItem, arguments, namedArguments);

            // Activate existing registration.
            return this.Resolve(registrationItem);
        }

        /// <summary>
        /// Add all arguments to the passed registration.
        /// </summary>
        /// <param name="registrationItem">The registration.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="namedArguments">The named arguments.</param>
        private void AddArgumentsToRegistration(RegistrationItem registrationItem, IEnumerable<object> arguments,
                                                IDictionary<string, object> namedArguments)
        {
            if (arguments != null)
            {
                registrationItem.RuntimeArguments.AddToAnonymousArguments(arguments);
            }

            if (namedArguments != null)
            {
                registrationItem.RuntimeArguments.AddToNamedArguments(namedArguments);
            }
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        public object Resolve(Type contractType, params object[] arguments)
        {
            return this.ResolveInternal(contractType, arguments, null);
        }

        ///<summary>
        /// Resolves a contract (include subcontracts) with constructor arguments.
        ///</summary>
        ///<param name="contractType">The contract type.</param>
        ///<param name="arguments">The constructor arguments.</param>
        ///<returns>The resolved instance as object.</returns>.
        public object Resolve(Type contractType, IEnumerable<object> arguments)
        {
            return this.ResolveInternal(contractType, arguments, null);
        }

        /// <summary>
        /// Resolves a contract (include subcontract) with named constructor arguments.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="namedArguments">The named constructor arguments.</param>
        /// <returns>The resolved instance as object.</returns>
        public object Resolve(Type contractType, IDictionary<string, object> namedArguments)
        {
            return this.ResolveInternal(contractType, null, namedArguments);
        }

        /// <summary>
        /// Resolves a dependency internally with a registrationItem.
        /// </summary>
        /// <param name="registrationItem">The registrationItem to activate.</param>
        /// <returns>The resolved instance.</returns>
        private object Resolve(RegistrationItem registrationItem)
        {
            var resolutionContext = new ResolutionContext(
                this,
                this._registrationContainer,
                registrationItem.Arguments,
                registrationItem.RuntimeArguments)
                                        {
                                            Registration = registrationItem
                                        };

            object instance = registrationItem.ActivateInstance(resolutionContext);

            // Clear all runtime arguments on registration.
            registrationItem.RuntimeArguments.AnonymousArguments = null;
            registrationItem.RuntimeArguments.NamedArguments = null;

            return instance;
        }

        /// <summary>
        /// Resolves all contracts of type {TContract}.
        /// </summary>
        /// <typeparam name="TContract">The contract type contraining the result.</typeparam>
        /// <returns>The resolved instances</returns>
        public IEnumerable<TContract> ResolveAll<TContract>()
        {
            return this.ResolveAll(typeof (TContract)).Cast<TContract>();
        }

        /// <summary>
        /// Resolves all contracts.
        /// </summary>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll()
        {
            return this._registrationContainer
                .AllRegistrations
                .Select(this.Resolve);
        }

        /// <summary>
        /// Resolves all contract of type <paramref name="contractType"/>.
        /// </summary>
        /// <param name="contractType">The contract type containing the result.</param>
        /// <returns>The resolved instances</returns>
        public IEnumerable<object> ResolveAll(Type contractType)
        {
            return this._registrationContainer.AllRegistrations
                .Where(r => r.ContractType == contractType)
                .Select(this.Resolve);
        }

        /// <summary>
        /// Injects properties to an existing instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void InjectProperties(object instance)
        {
            instance
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(this._validPropertiesSelector)
                .ForEach(p => p.SetValue(instance, this.Resolve(p.PropertyType), null));
        }
    }
}