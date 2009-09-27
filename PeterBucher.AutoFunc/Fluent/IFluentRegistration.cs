﻿namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public interface IFluentRegistration : IFluentInterface
    {
        /// <summary>
        /// Treat the current registration to singleton lifetime.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration AsSingleton();

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithArguments(params object[] arguments);

        /// <summary>
        /// Indicates that the default constructor should be used.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration UseDefaultConstructor();

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithName(string name);
    }
}