using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;
using LightCore.Lifecycle;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a registration source for IEnumerable{TContract}, (ResolveAll as dependency) support.
    /// 
    /// <example>
    /// public Foo(IEnumerable{IBar} bar) {  }
    /// </example>
    /// </summary>
    internal class EnumerableRegistrationSource : IRegistrationSource
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
                return contractType => contractType.IsGenericEnumerable()
                                       &&
                                       ((this._registrationContainer.HasRegistration(
                                           contractType.GetGenericArguments().FirstOrDefault()))
                                        ||
                    // Use ConcreteTypeRegistrationSource.
                                        (this._registrationContainer.IsSupportedByRegistrationSource(
                                            contractType.GetGenericArguments().FirstOrDefault())));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EnumerableRegistrationSource" />.
        /// </summary>
        /// <param name="registrationContainer">The registration container.</param>
        public EnumerableRegistrationSource(IRegistrationContainer registrationContainer)
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
                           Activator = new DelegateActivator(c => ResolveEnumerable(contractType, c)),
                           Lifecycle = new TransientLifecycle()
                       };
        }

        /// <summary>
        /// Resolves an enumerable type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns>A IEnumerable{TContract} instance with all registered instances.</returns>
        private static IEnumerable ResolveEnumerable(Type contractType, IContainer container)
        {
            Type genericArgument = contractType.GetGenericArguments().FirstOrDefault();

            object[] resolvedInstances = container.ResolveAll(genericArgument).ToArray();

            Type openListType = typeof(List<>);
            Type closedListType = openListType.MakeGenericType(genericArgument);

            var list = (IList)Activator.CreateInstance(closedListType);

            foreach(var instance in resolvedInstances)
            {
                list.Add(instance);
            }

            return list;
        }
    }
}