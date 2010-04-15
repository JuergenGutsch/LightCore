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
    internal class EnumerableRegistrationSource : RegistrationSource
    {
        /// <summary>
        /// The dependency selector. (Indicates whether the registration source can handle the type or not).
        /// </summary>
        public override Func<Type, bool> DependencySelector
        {
            get
            {
                return contractType => contractType.IsGenericEnumerable()
                                       &&
                                       this.RegistrationContainer.IsRegisteredContract(contractType.GetGenericArguments().FirstOrDefault());
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
            var registrationItem = new RegistrationItem(registrationKey)
                                       {
                                           Activator =
                                               new DelegateActivator(c => ResolveEnumerable(contractType, c)),
                                           Lifecycle = new TransientLifecycle()
                                       };

            return registrationItem;
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

            Array.ForEach(resolvedInstances, instance => list.Add(instance));

            return list;
        }
    }
}