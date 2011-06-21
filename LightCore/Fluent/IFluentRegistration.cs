using System;
using System.Collections.Generic;

using LightCore.Lifecycle;

namespace LightCore.Fluent
{
    /// <summary>
    /// Represents the fluent interface for the registration.
    /// </summary>
    public interface IFluentRegistration : IFluentInterface
    {
        /// <summary>
        /// Treat the current registration to use the passed lifecycle. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <typeparam name="TLifecycle">The lifecycle type.</typeparam>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new();

        /// <summary>
        /// Treat the current registration to use the passed lifecycle. (e.g. SingletonLifecycle, TrainsientLifecycle, ...).
        /// </summary>
        /// <param name="type">The lifecycle type.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ControlledBy(Type type);

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithArguments(params object[] arguments);

        /// <summary>
        /// Adds named arguments with an anonymous type to the registration.
        /// </summary>
        /// <param name="namedArguments">The arguments as anonymous type, e.g. new { arg1 = "test" }.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithNamedArguments(object namedArguments);

        /// <summary>
        /// Adds named arguments to the registration.
        /// </summary>
        /// <param name="namedArguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithNamedArguments(IDictionary<string, object> namedArguments);

        /// <summary>
        /// Gives a group association to the registration.
        /// </summary>
        /// <param name="group">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithGroup(string group);
    }
}