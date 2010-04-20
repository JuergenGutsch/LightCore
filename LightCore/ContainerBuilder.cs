using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;
using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Fluent;
using LightCore.Lifecycle;
using LightCore.Properties;
using LightCore.Registration;
using LightCore.Registration.RegistrationSource;

namespace LightCore
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        /// <summary>
        /// Contains the active registration groups as comma separated string.
        /// </summary>
        private string _activeRegistrationGroups;

        /// <summary>
        /// Contains the active registration groups as array for internal use.
        /// </summary>
        private string[] _activeRegistrationGroupsInternal;

        /// <summary>
        /// Gets or sets the active group configurations.
        /// </summary>
        public string ActiveRegistrationGroups
        {
            get
            {
                return _activeRegistrationGroups;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("value");
                }

                this._activeRegistrationGroups = value;
                this._activeRegistrationGroupsInternal = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Holds a container with registrations to register.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

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
            this._registrationContainer = new RegistrationContainer();
            this._registrationCallbacks = new List<Action>();
            this._defaultLifecycleFunction = () => new TransientLifecycle();
        }

        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns>The builded container.</returns>
        public IContainer Build()
        {
            var allRegistrationSources = new List<IRegistrationSource>()
                                             {
                                                 new OpenGenericRegistrationSource(this._registrationContainer),
                                                 new EnumerableRegistrationSource(this._registrationContainer),
                                                 new FactoryRegistrationSource(this._registrationContainer),
                                                 new ConcreteTypeRegistrationSource()
                                             };

            this._registrationContainer.RegistrationSources = allRegistrationSources;

            // Invoke the callbacks, they assert if the registration already exists, if not, register the registration.
            this._registrationCallbacks.ForEach(registerCallback => registerCallback());
            this._registrationCallbacks.Clear();

            var container = new Container(this._registrationContainer);

            return container;
        }

        /// <summary>
        /// Registers a module with registrations.
        /// </summary>
        /// <param name="registrationModule">The module to register within this container builder.</param>
        public void RegisterModule(RegistrationModule registrationModule)
        {
            registrationModule.Register(this);
        }

        /// <summary>
        /// Sets the default lifecycle for this container. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <typeparam name="TLifecycle">The default lifecycle.</typeparam>
        public void DefaultControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new()
        {
            this._defaultLifecycleFunction = () => new TLifecycle();
        }

        /// <summary>
        /// Sets the default lifecycle function for this container. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <param name="lifecycleFunction">The creator function for default lifecycle.</param>
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
            Type typeOfSelf = typeof(TSelf);

            if (!typeOfSelf.IsConcreteType())
            {
                throw new InvalidRegistrationException(
                    Resources.InvalidRegistrationFormat.FormatWith(typeOfSelf.ToString()));
            }

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.Register(typeOfSelf, typeOfSelf);
        }

        /// <summary>
        /// Registers a type an instance.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        public IFluentRegistration Register<TInstance>(TInstance instance)
        {
            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.AddToRegistrationFluent(new RegistrationItem(typeof(TInstance))
                                                    {
                                                        Activator = new InstanceActivator<TInstance>(instance)
                                                    });
        }

        /// <summary>
        /// Registers a contract with an activator function.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="activatorFunction">The activator as function..</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        public IFluentRegistration Register<TContract>(Func<IContainer, TContract> activatorFunction)
        {
            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.AddToRegistrationFluent(new RegistrationItem(typeof(TContract))
                                                    {
                                                        Activator =
                                                            new GenericDelegateActivator<TContract>(activatorFunction)
                                                    });
        }

        /// <summary>
        /// Add a registrationItem to the registrations.
        /// </summary>
        /// <param name="registrationItem">The registration to add.</param>
        private IFluentRegistration AddToRegistrationFluent(RegistrationItem registrationItem)
        {
            Action registrationCallback = () =>
                                              {

                                                  // Set default reuse scope, if not user defined. (System default is <see cref="TransientLifecycle" />.
                                                  if (registrationItem.Lifecycle == null)
                                                  {
                                                      registrationItem.Lifecycle = this._defaultLifecycleFunction();
                                                  }

                                                  if (this._activeRegistrationGroupsInternal != null &&
                                                      registrationItem.Group != null)
                                                  {
                                                      if (
                                                          !this._activeRegistrationGroupsInternal.Any(
                                                              g => g.Trim() == registrationItem.Group.Trim()))
                                                      {
                                                          // Do not add inactive registrationItem.
                                                          return;
                                                      }
                                                  }

                                                  if (
                                                      this._registrationContainer.Registrations.ContainsKey(
                                                          registrationItem.ContractType))
                                                  {
                                                      // Duplicate registration for enumerable requests.
                                                      RegistrationItem duplicateItem =
                                                          this._registrationContainer.Registrations[
                                                              registrationItem.ContractType];

                                                      this._registrationContainer.DuplicateRegistrations.Add(
                                                          registrationItem);
                                                      this._registrationContainer.DuplicateRegistrations.Add(
                                                          duplicateItem);

                                                      this._registrationContainer.Registrations.Remove(
                                                          duplicateItem.ContractType);
                                                  }
                                                  else
                                                  {
                                                      this._registrationContainer.Registrations.Add(
                                                          registrationItem.ContractType,
                                                          registrationItem);
                                                  }
                                              };

            this._registrationCallbacks.Add(registrationCallback);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return new FluentRegistration(registrationItem);
        }

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// 
        ///  Can be a generic contract (open generic types) with its implementationtype.
        /// e.g. builder.RegisterGeneric(typeof(IRepository{T}), typeof(Repository{T}));
        /// container.Resolve{IRepository{Foo}}();
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
            if (!typeOfContract.IsGenericTypeDefinition && !typeOfContract.IsAssignableFrom(typeOfImplementation))
            {
                throw new ContractNotImplementedByTypeException(
                    Resources.ContractNotImplementedByTypeFormat.FormatWith(typeOfContract, typeOfImplementation));
            }

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return this.AddToRegistrationFluent(new RegistrationItem(typeOfContract)
                                                    {
                                                        Activator = new ReflectionActivator(typeOfImplementation),
                                                        Lifecycle = this._defaultLifecycleFunction(),
                                                        ImplementationType = typeOfImplementation
                                                    });
        }
    }
}