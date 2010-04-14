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

            foreach (Type registeredType in typesToRegister)
            {
                var key = new RegistrationKey(registeredType);
                var item = new RegistrationItem(key);

                registrationContainer.Registrations.Add(new KeyValuePair<RegistrationKey, RegistrationItem>(key, item));
            }

            return registrationContainer;
        }
    }
}