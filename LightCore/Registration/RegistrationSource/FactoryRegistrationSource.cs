#if !CF35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using LightCore.Activation.Activators;
using LightCore.ExtensionMethods.System;

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
    internal class FactoryRegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// Holds the name for resolve methods.
        /// </summary>
        private const string ResolveMethodName = "Resolve";

        /// <summary>
        /// Holds the object type.
        /// </summary>
        private static readonly Type TypeOfObject = typeof( object );

        /// <summary>
        /// Holds the resolve methodinfo with arguments.
        /// </summary>
        private static readonly MethodInfo ResolveWithArgumentsMethod = typeof( IContainer ).GetMethod( ResolveMethodName, new[] { typeof( IEnumerable<object> ) } );

        /// <summary>
        /// The regisration container.
        /// </summary>
        private readonly IRegistrationContainer _registrationContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="FactoryRegistrationSource" />.
        /// </summary>
        /// <param name="registrationContainer">The registration container.</param>
        public FactoryRegistrationSource( IRegistrationContainer registrationContainer )
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
                // LastOrDefault for the lastest parameter, e.g.: Func<string, bool, IFoo>().
                return contractType => contractType.IsFactoryType() && _registrationContainer.IsRegistered( contractType.GetGenericArguments().LastOrDefault() );
            }
        }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns><value>The registration item</value> if this source can handle it, otherwise <value>null</value>.</returns>
        public RegistrationItem GetRegistrationFor( Type contractType, IContainer container )
        {
            Type[] genericArguments = contractType.GetGenericArguments();
            Type returnType = genericArguments.Last();

            MethodInfo genericWithArgumentsMethod = ResolveWithArgumentsMethod.MakeGenericMethod( returnType );

            var parameterExpressions = genericArguments
                .Take( genericArguments.Length - 1 ) // ReturnType (Length -1) will be ignored.
                .Select( genericArgument => Expression.Parameter( genericArgument, genericArgument.Name ) )
                .ToList();

            return new RegistrationItem( contractType )
                       {
                           Activator = new DelegateActivator( c =>
                                                                 {
                                                                     var containerConstant = Expression.Constant( c );

                                                                     var newArgumentArray = Expression.NewArrayInit(
                                                                         TypeOfObject,
                                                                         parameterExpressions
                                                                             .Select( p => Expression.TypeAs( p, TypeOfObject ) )
                                                                             .Cast<Expression>() );

                                                                     var resolveCall = Expression.Call(
                                                                         containerConstant,
                                                                         genericWithArgumentsMethod,
                                                                         newArgumentArray );

                                                                     // () => value(LightCore.Container).Resolve(new [] {}).
                                                                     var lambda = Expression.Lambda( contractType,
                                                                                                    resolveCall,
                                                                                                    parameterExpressions );

                                                                     return lambda.Compile();
                                                                 } )
                       };
        }
    }
}
#endif