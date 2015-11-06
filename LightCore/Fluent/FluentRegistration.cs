using System;
using System.Reflection;
using System.Collections.Generic;

using LightCore.ExtensionMethods.System;
using LightCore.Lifecycle;
using LightCore.Properties;
using LightCore.Registration;

namespace LightCore.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    internal class FluentRegistration : IFluentRegistration
    {
        /// <summary>
        /// The current registration.
        /// </summary>
        private readonly RegistrationItem _registrationItem;

        /// <summary>
        /// Initializes a new instance of <see cref="FluentRegistration" /> type.
        /// </summary>
        /// <param name="registrationItem">The current mapping item.</param>
        internal FluentRegistration(RegistrationItem registrationItem)
        {
            this._registrationItem = registrationItem;
        }

        /// <summary>
        /// Treat the current registration to use the passed lifecycle. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <typeparam name="TLifecycle">The lifecycle type.</typeparam>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new()
        {
            this._registrationItem.Lifecycle = new TLifecycle();
            return this;
        }

        /// <summary>
        /// Treat the current registration to use the passed lifecycle. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <param name="type">The lifecycle type.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ControlledBy(Type type)
        {
            if (!typeof(ILifecycle).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                throw new ArgumentException(Resources.PassedTypeDoesNotImplementILifecycleFormat.FormatWith(type));
            }

            this._registrationItem.Lifecycle = (ILifecycle)Activator.CreateInstance(type);
            return this;
        }

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithArguments(params object[] arguments)
        {
            this._registrationItem.Arguments.AddToAnonymousArguments(arguments);
            return this;
        }

        /// <summary>
        /// Adds named arguments to the registration.
        /// </summary>
        /// <param name="namedArguments">The arguments as anonymous type, e.g. new { arg1 = "test" }.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithNamedArguments(object namedArguments)
        {
            return this.WithNamedArguments(namedArguments.ToNamedArgumentDictionary());
        }

        /// <summary>
        /// Adds named arguments to the registration.
        /// </summary>
        /// <param name="namedArguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithNamedArguments(IDictionary<string, object> namedArguments)
        {
            this._registrationItem.Arguments.AddToNamedArguments(namedArguments);
            return this;
        }

        /// <summary>
        /// Gives a group association to the registration.
        /// </summary>
        /// <param name="group">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithGroup(string group)
        {
            this._registrationItem.Group = group;
            return this;
        }
    }
}