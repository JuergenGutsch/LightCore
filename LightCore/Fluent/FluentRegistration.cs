using System;

using LightCore.Lifecycle;

namespace LightCore.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public class FluentRegistration : IFluentRegistration
    {
        /// <summary>
        /// The current registration.
        /// </summary>
        private readonly Registration _registration;

        /// <summary>
        /// Initializes a new instance of <see cref="FluentRegistration" /> type.
        /// </summary>
        /// <param name="registration">The current mapping item.</param>
        public FluentRegistration(Registration registration)
        {
            this._registration = registration;
        }

        /// <summary>
        /// Treat the current registration to use the passed lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new()
        {
            this._registration.Lifecycle = new TLifecycle();
            return this;
        }

        /// <summary>
        /// Treat the current registration to use the passed lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ControlledBy(Type type)
        {
            this._registration.Lifecycle = (ILifecycle)Activator.CreateInstance(type);
            return this;
        }

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithArguments(params object[] arguments)
        {
            this._registration.Arguments = arguments;
            return this;
        }

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithName(string name)
        {
            this._registration.Key.Name = name;
            return this;
        }

        /// <summary>
        /// Gives a group association to the registration.
        /// </summary>
        /// <param name="group">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithGroup(string group)
        {
            this._registration.Group = group;
            return this;
        }
    }
}