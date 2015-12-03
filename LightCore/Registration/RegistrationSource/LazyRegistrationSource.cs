using System;
using System.Linq;
using System.Reflection;


using LightCore.Activation.Activators;
using LightCore.Lifecycle;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents an registration source for lazy instantiation. (Lazy{TContract} as dependency).
    /// 
    /// <example>
    /// public Foo(Lazy{IBar} bar) {  }
    /// </example>
    /// 
    /// <example>
    /// public Foo(Lazy{IBar} bar) {  }
    /// </example>
    /// </summary>
    internal class LazyRegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// The regisration container.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

        /// <summary>
        /// Holds the CreateLazyRegistration methodInfo.
        /// </summary>
        private static readonly MethodInfo CreateLazyRegistrationMethod =
            typeof(LazyRegistrationSource)
            .GetMethod("CreateLazyRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryRegistrationSource" />.
        /// </summary>
        /// <param name="registrationContainer">The registration container.</param>
        public LazyRegistrationSource(IRegistrationContainer registrationContainer)
        {
            this._registrationContainer = registrationContainer;
        }

        /// <summary>
        /// Gets whether the registration source supports a type or not.
        /// </summary>
        public Func<Type, bool> SourceSupportsTypeSelector
        {
            get
            {
                Func<Type, bool> isGenericLazy = contractType => contractType.GetTypeInfo().IsGenericType
                                                  &&
                                                  contractType.GetGenericTypeDefinition() == typeof(Lazy<>);

                return contractType => isGenericLazy(contractType)
                                       &&
                                       ((_registrationContainer.HasRegistration(
                                           contractType.GetGenericArguments().FirstOrDefault()))
                                        ||
                                        // Use ConcreteTypeRegistrationSource.
                                        (_registrationContainer.IsSupportedByRegistrationSource(
                                            contractType.GetGenericArguments().FirstOrDefault())));

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
            return (RegistrationItem)CreateLazyRegistrationMethod
                                          .MakeGenericMethod(contractType.GetGenericArguments().FirstOrDefault())
                                          .Invoke(null, null);
        }

        /// <summary>
        /// Creates a Lazy registration for a given type on the fly.
        /// </summary>
        /// <typeparam name="T">The type to use.</typeparam>
        /// <returns>The new registrationItem for lazy resolve.</returns>
        private static RegistrationItem CreateLazyRegistration<T>()
        {
            return new RegistrationItem(typeof (Lazy<T>))
            {
                Activator = new DelegateActivator(c => new Lazy<T>(c.Resolve<T>)),
                Lifecycle = new TransientLifecycle()
            };
        }
    }
}
