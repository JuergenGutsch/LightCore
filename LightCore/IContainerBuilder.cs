using System;

using LightCore.Fluent;
using LightCore.Lifecycle;
using LightCore.Registration;

namespace LightCore
{
    /// <summary>
    /// Represents a builder that is reponsible for accepting, validating registrations
    /// and builds the container with that registrations.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Gets or sets the active group configurations.
        /// </summary>
        string ActiveRegistrationGroups { get; set; }

        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns>The builded container.</returns>
        IContainer Build();

        /// <summary>
        /// Registers a module with registrations.
        /// </summary>
        /// <param name="registrationModule">The module to register within this container builder.</param>
        void RegisterModule(RegistrationModuleBase registrationModule);

        /// <summary>
        /// Sets the default lifecycle for this container. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <typeparam name="TLifecycle">The default lifecycle.</typeparam>
        void DefaultControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new();

        /// <summary>
        /// Sets the default lifecycle function for this container. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <param name="lifecycleFunction">The creator function for default lifecycle.</param>
        void DefaultControlledBy(Func<ILifecycle> lifecycleFunction);

        /// <summary>
        /// Registers a type to itself.
        /// </summary>
        /// <typeparam name="TSelf">The type.</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes fluent registration.</returns>
        IFluentRegistration Register<TSelf>();

        /// <summary>
        /// Registers a instance for usage.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="instance">The instance to register.</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        IFluentRegistration Register<TInstance>(TInstance instance);

        /// <summary>
        /// Registers a contract with an activator function.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="activatorFunction">The activator as function..</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        IFluentRegistration Register<TContract>(Func<IContainer, TContract> activatorFunction);

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation for the contract</typeparam>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        IFluentRegistration Register<TContract, TImplementation>() where TImplementation : TContract;

        /// <summary>
        /// Registers a contract with its implementationtype.
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="typeOfImplementation">The type of the implementation for the contract</param>
        /// <returns>An instance of <see cref="IFluentRegistration"  /> that exposes a fluent interface for registration configuration.</returns>
        IFluentRegistration Register(Type typeOfContract, Type typeOfImplementation);
    }
}