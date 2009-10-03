using System;
using LightCore.Reuse;

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
        /// Treat the current registration to be transient.
        /// One instance per request.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ScopedToTransient()
        {
            this._registration.ReuseStrategy = new TransientReuseStrategy();
            return this;
        }

        /// <summary>
        /// Treat the current registration to singleton behaviour.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ScopedToSingleton()
        {
            this._registration.ReuseStrategy = new SingletonReuseStrategy();
            return this;
        }

        /// <summary>
        /// Treat the current registration to the passed reuse strategy behaviour.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ScopedTo<TReuseStrategy>() where TReuseStrategy : IReuseStrategy, new()
        {
            this._registration.ReuseStrategy = new TReuseStrategy();
            return this;
        }

        /// <summary>
        /// Treat the current registration to the passed reuse strategy behaviour function.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration ScopedTo(Func<IReuseStrategy> reuseStrategyFunction)
        {
            this._registration.ReuseStrategy = reuseStrategyFunction();
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
        /// Indicates that the default constructor should be used.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration UseDefaultConstructor()
        {
            this._registration.Activator.UseDefaultConstructor = true;;
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
    }
}