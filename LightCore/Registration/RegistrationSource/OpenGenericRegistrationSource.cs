using System;

using LightCore.Activation.Activators;
using LightCore.Activation.Components;
using LightCore.Fluent;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a registration source for open generic type support.
    /// 
    /// <example>
    /// public Foo(IRepository{Bar} barRepository) {  }
    /// </example>
    /// </summary>
    internal class OpenGenericRegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// The regisration container.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

        /// <summary>
        /// Gets whether the registration source supports a type or not.
        /// </summary>
        public Func<Type, bool> SourceSupportsTypeSelector
        {
            get
            {
                return
                    contractType => contractType != null
                                    && contractType.IsGenericType
                                    && IsRegisteredOpenGeneric(contractType);
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OpenGenericRegistrationSource" />.
        /// </summary>
        /// <param name="registrationContainer">The registration container.</param>
        public OpenGenericRegistrationSource(IRegistrationContainer registrationContainer)
        {
            this._registrationContainer = registrationContainer;
        }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns><value>The registration item</value> if this source can handle it, otherwise <value>null</value>.</returns>
        public RegistrationItem GetRegistrationFor(Type contractType, IContainer container)
        {
            Type[] genericArguments = contractType.GetGenericArguments();

            // Get the Registration for the open generic type.
            RegistrationItem openGenericTypeRegistration =
                this._registrationContainer
                    .Registrations[contractType.GetGenericTypeDefinition()];

            // Register closed generic type on-the-fly.
            Type implementationType = openGenericTypeRegistration.ImplementationType.MakeGenericType(genericArguments);

            var closedGenericRegistration = new RegistrationItem(contractType)
                                                {
                                                    Activator = new ReflectionActivator(
                                                        implementationType,
                                                        container.Resolve<IConstructorSelector>(),
                                                        container.Resolve<IArgumentCollector>()
                                                        ),
                                                    ImplementationType = implementationType
                                                };

            var fluentRegistration = new FluentRegistration(closedGenericRegistration);
            fluentRegistration.ControlledBy(openGenericTypeRegistration.Lifecycle.GetType());

            return closedGenericRegistration;
        }

        /// <summary>
        /// Checks whether an open generic type, taken from the closed type (contractType) is registered or not.
        /// (This makes possible to use open generic types and also closed generic types at once.
        /// </summary>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the open generic type is registered, otherwise <value>false</value>.</returns>
        private bool IsRegisteredOpenGeneric(Type contractType)
        {
            return this._registrationContainer.IsRegistered(contractType.GetGenericTypeDefinition());
        }
    }
}