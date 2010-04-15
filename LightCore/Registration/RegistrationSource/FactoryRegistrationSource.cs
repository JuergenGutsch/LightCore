using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;
using LightCore.Lifecycle;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents an registration source for factory support. (Func{TContract} as dependency).
    /// It supports also get an parametrized function, for ad-hoc instantiation.
    /// 
    /// <example>
    /// public Foo(Func{IBar} bar) {  }
    /// </example>
    /// 
    /// <example>
    /// public Foo(Func{string, IBar} bar) {  }
    /// </example>
    /// </summary>
    internal class FactoryRegistrationSource : RegistrationSource
    {
        /// <summary>
        /// Holds the name for resolve methods.
        /// </summary>
        private const string ResolveMethodName = "Resolve";

        /// <summary>
        /// Holds the object type.
        /// </summary>
        private static readonly Type TypeOfObject = typeof(object);

        /// <summary>
        /// Holds the container type.
        /// </summary>
        private static readonly Type TypeOfContainer = typeof(IContainer);

        /// <summary>
        /// Holds the resolve methodinfo.
        /// </summary>
        private static readonly MethodInfo ResolveMethod = TypeOfContainer.GetMethod(ResolveMethodName, Type.EmptyTypes);

        /// <summary>
        /// Holds the resolve methodinfo with arguments.
        /// </summary>
        private static readonly MethodInfo ResolveWithArgumentsMethod = TypeOfContainer.GetMethod(ResolveMethodName, new[] { typeof(IEnumerable<object>) });

        /// <summary>
        /// The dependency selector. (Indicates whether the registration source can handle the type or not).
        /// </summary>
        public override Func<Type, bool> DependencySelector
        {
            get
            {
                return
                    contractType => contractType.IsFactoryType()
                        && this.RegistrationContainer.IsRegisteredContract(contractType.GetGenericArguments().LastOrDefault());
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
            Type returnType = genericArguments.Last();

            var registrationKey = new RegistrationKey(contractType);
            RegistrationItem registrationItem;

            if (genericArguments.Length == 1)
            {
                MethodInfo genericResolveMethod = ResolveMethod.MakeGenericMethod(returnType);

                registrationItem = new RegistrationItem(registrationKey)
                                       {
                                           Activator = new DelegateActivator(c =>
                                                                             Expression.Lambda(
                                                                                 contractType,
                                                                                 Expression.Call(
                                                                                     Expression.Constant(c),
                                                                                     genericResolveMethod)).Compile()),
                                           Lifecycle = new TransientLifecycle(),
                                           ImplementationType = contractType
                                       };
            }
            else
            {
                MethodInfo genericWithArgumentsMethod = ResolveWithArgumentsMethod.MakeGenericMethod(returnType);

                var parameterExpressions = genericArguments
                    .Take(genericArguments.Length - 1)
                    .Select(genericArgument => Expression.Parameter(genericArgument, genericArgument.Name))
                    .ToList();

                registrationItem = new RegistrationItem(registrationKey)
                                       {
                                           Activator = new DelegateActivator(c =>
                                                                             Expression.Lambda(
                                                                                 contractType,
                                                                                 Expression.Call(
                                                                                     Expression.Constant(c),
                                                                                     genericWithArgumentsMethod,
                                                                                     Expression.NewArrayInit(
                                                                                         TypeOfObject,
                                                                                         parameterExpressions.Select(
                                                                                             p => Expression.TypeAs(p, TypeOfObject))
                                                                                             .Cast<Expression>())), parameterExpressions).Compile()),
                                           Lifecycle = new TransientLifecycle(),
                                           ImplementationType = contractType
                                       };
            }

            return registrationItem;
        }
    }
}