using System;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;
using LightCore.Lifecycle;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents an registration source for concrete types.
    /// </summary>
    internal class ConcreteTypeRegistrationSource : RegistrationSource
    {
        /// <summary>
        /// The dependency selector. (Indicates whether the registration source can handle the type or not).
        /// </summary>
        public override Func<Type, bool> DependencySelector
        {
            get
            {
                return contractType => contractType.IsConcreteType();
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
            var registrationKey = new RegistrationKey(contractType);

            // On-the-fly registration of concrete types. Equivalent to new-operator.
            var registrationItem = new RegistrationItem(registrationKey)
            {
                Activator = new ReflectionActivator(contractType),
                Lifecycle = new TransientLifecycle()
            };

            return registrationItem;
        }
    }
}