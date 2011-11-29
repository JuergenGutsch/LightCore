using System;
using System.Linq;

using LightCore.Activation.Activators;
using LightCore.Lifecycle;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a registration source for TContract[], (ResolveAll as dependency) support.
    /// 
    /// <example>
    /// public Foo(IBar[] bar) {  }
    /// </example>
    /// </summary>
    internal class ArrayRegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// The regisration container.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

        public Func<Type, bool> SourceSupportsTypeSelector
        {
            get
            {
                return contractType => contractType.IsArray
                                       &&
                                       (this._registrationContainer.HasRegistration(contractType.GetElementType())
                                        ||
                                        // Use ConcreteTypeRegistrationSource.
                                        (this._registrationContainer.IsSupportedByRegistrationSource(
                                            contractType.GetElementType())));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ArrayRegistrationSource" />.
        /// </summary>
        /// <param name="registrationContainer">The registration container.</param>
        public ArrayRegistrationSource(IRegistrationContainer registrationContainer)
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
            return new RegistrationItem(contractType)
                       {
                           Activator = new DelegateActivator(c => ResolveArray(contractType, c)),
                           Lifecycle = new TransientLifecycle()
            };
        }

        /// <summary>
        /// Resolves an array type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns>A TContract[] instance with all registered instances.</returns>
        private static Array ResolveArray(Type contractType, IContainer container)
        {
            Type elementType = contractType.GetElementType();
            object[] resolvedInstances = container.ResolveAll(elementType).ToArray();
            return resolvedInstances;
        }
    }
}