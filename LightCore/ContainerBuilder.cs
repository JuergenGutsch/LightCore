using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Activation;
using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Fluent;
using LightCore.Lifecycle;
using LightCore.Properties;
using LightCore.Registration;

namespace LightCore
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        /// <summary>
        /// Contains the active group configurations as comma separated string.
        /// </summary>
        private string _activeGroupConfigurations;

        /// <summary>
        /// Contains the active group configurations as array for internal use.
        /// </summary>
        private string[] _activeGroupConfigurationsInternal;

        /// <summary>
        /// Gets or sets the active group configurations.
        /// </summary>
        public string ActiveGroupConfigurations
        {
            get
            {
                return _activeGroupConfigurations;
            }

            set
            {
                this._activeGroupConfigurations = value;
                this._activeGroupConfigurationsInternal = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Holds a list with registered registrations.
        /// </summary>
        private readonly IDictionary<RegistrationKey, RegistrationItem> _registrations;

        /// <summary>
        /// Holds a list with registering callbacks.
        /// </summary>
        private readonly IList<Action> _registrationCallbacks;

        /// <summary>
        /// Holds the default lifecycle function.
        /// </summary>
        private Func<ILifecycle> _defaultLifecycleFunction;

        /// <summary>
        /// Initializes a new instance of <see cref="ContainerBuilder" />.
        /// </summary>
        public ContainerBuilder()
        {
            this._registrations = new Dictionary<RegistrationKey, RegistrationItem>();
            this._registrationCallbacks = new List<Action>();
            this._defaultLifecycleFunction = () => new SingletonLifecycle();
        }

        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns>The builded container.</returns>
        public IContainer Build()
        {
            // Invoke the callbacks, they assert if the registration already exists, if not, register the registration.
            this._registrationCallbacks.ForEach(registerCallback => registerCallback());

            return new Container(this._registrations);
        }

        /// <summary>
        /// Registers a module with registrations.
        /// </summary>
        /// <param name="module">The module to register within this container builder.</param>
        public void RegisterModule(RegistrationModule module)
        {
            module.Register(this);
        }

        /// <summary>
        /// Sets the default lifecycle for this container.
        /// </summary>
        /// <typeparam name="TLifecycle">The default lifecycle.</typeparam>
        public void DefaultControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new()
        {
            this._defaultLifecycleFunction = () => new TLifecycle();
        }

        /// <summary>
        /// Sets the default reuse strategy function for this container.
        /// </summary>
        /// <param name="lifecycleFunction">The creator function for default reuse strategy.</param>
        public void DefaultControlledBy(Func<ILifecycle> lifecycleFunction)
        {
            this._defaultLifecycleFunction = lifecycleFunction;
        }

        /// <summary>
        /// Registers a type to itself.
        /// </summary>
        /// <typeparam name="TSelf">The type.</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        public IFluentRegistration Register<TSelf>()
        {
            Type typeOfSelf = typeof (TSelf);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.Register(typeOfSelf, typeOfSelf);
        }

        /// <summary>
        /// Registers a contract with an activator function.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="activatorFunction">The activator as function..</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        public IFluentRegistration Register<TContract>(Func<IContainer, TContract> activatorFunction)
        {
            var typeOfContract = typeof(TContract);

            var key = new RegistrationKey(typeOfContract, null);

            var registration = new RegistrationItem(key)
            {
                Activator = new DelegateActivator<TContract>(activatorFunction)
            };

            this.AddToRegistrations(registration);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return new FluentRegistration(registration);
        }

        /// <summary>
        /// Add a registrationItem to the registrations.
        /// </summary>
        /// <param name="registrationItem">The registration to add.</param>
        private void AddToRegistrations(RegistrationItem registrationItem)
        {
            // Set default reuse scope, if not user defined. (System default is <see cref="SingletonLifecycle" />.
            if (registrationItem.Lifecycle == null)
            {
                registrationItem.Lifecycle = this._defaultLifecycleFunction();
            }

            // Add a register callback for lazy assertion after manipulating in fluent registrationItem api.
            this._registrationCallbacks.Add(() =>
            {
                if (this._activeGroupConfigurationsInternal != null && registrationItem.Key.Group != null)
                {
                    if (!this._activeGroupConfigurationsInternal.Any(g => g.Trim() == registrationItem.Key.Group))
                    {
                        // Do not add inactive registrationItem.
                        return;
                    }
                }

                if(this._registrations.ContainsKey(registrationItem.Key))
                {
                    throw new
                        RegistrationAlreadyExistsException(
                        Resources.RegistrationForContractAndNameAlreadyExistsFormat.FormatWith(
                            registrationItem.Key.ContractType, registrationItem.Key.Name));
                }

                this._registrations.Add(registrationItem.Key, registrationItem);
            });
        }

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        public IFluentRegistration Register<TContract, TImplementation>() where TImplementation : TContract
        {
            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.Register(typeof(TContract), typeof(TImplementation));
        }

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="typeOfImplementation">The type of the implementation for the contract</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        public IFluentRegistration Register(Type typeOfContract, Type typeOfImplementation)
        {
            if (!typeOfContract.IsAssignableFrom(typeOfImplementation))
            {
                throw new ContractNotImplementedByTypeException();
            }

            var key = new RegistrationKey(typeOfContract);

            // Register the type with default lifetime.
            var registration = new RegistrationItem(key)
                                   {
                                       Activator = new ReflectionActivator(typeOfImplementation)
                                   };

            this.AddToRegistrations(registration);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return new FluentRegistration(registration);
        }
    }
}