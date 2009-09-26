using System;

namespace PeterBucher.AutoFunc.Fluent
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
        /// Treat the current registration to singleton lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration AsSingleton()
        {
            this._registration.Lifecycle = Lifecycle.Singleton;
            return this;
        }

        /// <summary>
        /// Treat the current registration to transient lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration AsTransient()
        {
            this._registration.Lifecycle = Lifecycle.Transient;
            return this;
        }

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public void WithName(string name)
        {
            this._registration.Key.Name = name;
        }
    }
}