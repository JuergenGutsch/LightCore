using System;
using System.Collections.Generic;

using LightCore.Registration;

namespace LightCore.Tests.Activation
{
    /// <summary>
    /// Represents a helper class for creating fake registration containers.
    /// </summary>
    internal class RegistrationHelper
    {
        /// <summary>
        /// Gets a fake registration container with given registered types.
        /// </summary>
        /// <param name="typesToRegister">The types to register.</param>
        /// <returns>The registration container with registered types.</returns>
        internal static RegistrationContainer GetRegistrationContainerFor(Type[] typesToRegister)
        {
            var registrationContainer = new RegistrationContainer();

            foreach (var registeredType in typesToRegister)
            {
                var item = new RegistrationItem(registeredType);

                registrationContainer.Registrations.Add(new KeyValuePair<Type, RegistrationItem>(registeredType, item));
            }

            return registrationContainer;
        }
    }
}