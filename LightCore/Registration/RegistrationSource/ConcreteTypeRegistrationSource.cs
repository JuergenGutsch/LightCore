using System;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents an registration source for concrete types.
    /// 
    /// <example>
    /// public Foo(Bar bar) {  }
    /// </example>
    /// </summary>
    internal class ConcreteTypeRegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// Gets whether the registration source supports a type or not.
        /// </summary>
        public Func<Type, bool> SourceSupportsTypeSelector
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
        public RegistrationItem GetRegistrationFor(Type contractType, IContainer container)
        {
            // On-the-fly registration of concrete types. Equivalent to new-operator.
            return new RegistrationItem(contractType)
                       {
                           Activator = new ReflectionActivator(contractType)
                       };
        }
    }
}