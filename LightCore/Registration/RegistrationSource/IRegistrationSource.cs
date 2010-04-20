﻿using System;

namespace LightCore.Registration.RegistrationSource
{
    /// <summary>
    /// Represents a contract for dynamically add registrations, e.g. for generic or lazy support.
    /// </summary>
    internal interface IRegistrationSource
    {
        /// <summary>
        /// Gets whether the registration source supports a type or not.
        /// </summary>
        Func<Type, bool> SourceSupportsTypeSelector { get; }

        /// <summary>
        /// Gets a registration for some contract type.
        /// </summary>
        /// <param name="contractType">The contract type.</param>
        /// <param name="container">The container.</param>
        /// <returns><value>The registration item</value> if this source can handle it, otherwise <value>null</value>.</returns>
        RegistrationItem GetRegistrationFor(Type contractType, IContainer container);
    }
}