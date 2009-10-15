﻿using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Activation;
using LightCore.Exceptions;
using LightCore.Fluent;
using LightCore.Lifecycle;
using LightCore.Properties;

namespace LightCore
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public class ContainerBuilder : IContainerBuilder
    {
        /// <summary>
        /// Holds a list with registered registrations.
        /// </summary>
        private readonly IDictionary<RegistrationKey, Registration> _registrations;

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
            this._registrations = new Dictionary<RegistrationKey, Registration>();
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
        /// <param name="module">The module.</param>
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
        internal void DefaultControlledBy(Func<ILifecycle> lifecycleFunction)
        {
            this._defaultLifecycleFunction = lifecycleFunction;
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

            var key = new RegistrationKey(typeOfContract);

            var registration = new Registration(key)
            {
                Activator = new DelegateActivator<TContract>(activatorFunction)
            };

            this.AddToRegistrations(registration);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return new FluentRegistration(registration);
        }

        /// <summary>
        /// Add a registration to the registrations.
        /// </summary>
        /// <param name="registration">The registration to add.</param>
        private void AddToRegistrations(Registration registration)
        {
            // Set default reuse scope, if not user defined. (System default is <see cref="SingletonLifecycle" />.
            if (registration.Lifecycle == null)
            {
                registration.Lifecycle = this._defaultLifecycleFunction();
            }

            // Add a register callback for lazy assertion after manipulating in fluent registration api.
            this._registrationCallbacks.Add(() =>
            {
                this.AssertRegistrationExists(registration.Key);
                this._registrations.Add(registration.Key, registration);
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
                throw new RegisteredTypesNotCompatibleException();
            }

            var key = new RegistrationKey(typeOfContract);

            // Register the type with default lifetime.
            var registration = new Registration(key)
                                   {
                                       Activator = new ReflectionActivator(typeOfImplementation)
                                   };

            this.AddToRegistrations(registration);

            // Return a new instance of <see cref="IFluentRegistration" /> for supporting a fluent interface for registration configuration.
            return new FluentRegistration(registration);
        }

        /// <summary>
        /// Asserts whether the registration already exists.
        /// </summary>
        /// <param name="registrationKey">The registration key to check for.</param>
        private void AssertRegistrationExists(RegistrationKey registrationKey)
        {
            var selectors = new List<Func<RegistrationKey, bool>>
                                {
                                    r => r.ContractType == registrationKey.ContractType,
                                    //r => r.ImplementationType == registrationKey.ImplementationType,
                                    r => r.Name == registrationKey.Name
                                };

            Func<RegistrationKey, bool> registrationEqualsSelector = selectors.Aggregate((current, next) => r => current(r) && next(r));
            Func<RegistrationKey, bool> registrationNameEqualsSelector = r => r.Name != null && r.Name == registrationKey.Name;

            Func<RegistrationKey, bool> mainSelector = r => registrationEqualsSelector(r) || registrationNameEqualsSelector(r);

            // Check if the registration key already exists.
            if (this._registrations.Any(r => mainSelector(r.Key)))
            {
                throw new RegistrationAlreadyExistsException(
                    Resources.RegistrationForContractAndNameAlreadyExistsFormat.FormatWith(
                        registrationKey.ContractType,
                        registrationKey.Name));
            }
        }
    }
}