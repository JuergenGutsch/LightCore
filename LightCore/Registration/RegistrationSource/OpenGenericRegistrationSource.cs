using System;

using LightCore.Activation.Activators;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a registration source for open generic type support.
    /// </summary>
    internal class OpenGenericRegistrationSource : RegistrationSource
    {
        /// <summary>
        /// The dependency selector. (Indicates whether the registration source can handle the type or not).
        /// </summary>
        public override Func<Type, bool> DependencySelector
        {
            get
            {
                return contractType => contractType.IsGenericType && IsRegisteredOpenGeneric(this.RegistrationContainer, contractType);
            }
        }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns><value>The registration item</value> if this source can handle it, otherwise <value>null</value>.</returns>
        protected override RegistrationItem GetRegistrationForCore(Type contractType, IContainer container)
        {
            Type[] genericArguments = contractType.GetGenericArguments();

            // Get the Registration for the open generic type.
            var registrationKey = new RegistrationKey(contractType.GetGenericTypeDefinition());
            RegistrationItem registrationItem = this.RegistrationContainer.Registrations[registrationKey];

            // Register close generic type on-the-fly.
            registrationKey = new RegistrationKey(registrationItem.Key.ContractType.MakeGenericType(genericArguments));

            var activator = new ReflectionActivator(registrationItem.ImplementationType.MakeGenericType(genericArguments));

            registrationItem = new RegistrationItem(registrationKey)
            {
                Activator = activator,
                Lifecycle = registrationItem.Lifecycle
            };

            return registrationItem;
        }

        /// <summary>
        /// Checks whether an open generic type, taken from the closed type (contractType) is registered or not.
        /// (This makes possible to use open generic types and also closed generic types at once.
        /// </summary>
        /// <param name="existingRegistrations">The existing registrations.</param>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the open generic type is registered, otherwise <value>false</value>.</returns>
        private static bool IsRegisteredOpenGeneric(RegistrationContainer existingRegistrations, Type contractType)
        {
            return contractType.IsGenericTypeDefinition
                || contractType.IsGenericType && existingRegistrations.IsRegisteredContract(contractType.GetGenericTypeDefinition());
        }
    }
}