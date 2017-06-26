using System;
using System.Collections.Generic;

using LightCore.ExtensionMethods.System.Collections.Generic;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents an inline registration module.
    /// </summary>
    public class InlineRegistrationModule : RegistrationModule
    {
        /// <summary>
        /// Gets or sets the registration callbacks.
        /// </summary>
        public IEnumerable<Action<IContainerBuilder>> RegistrationCallbacks
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InlineRegistrationModule" />.
        /// </summary>
        /// <param name="registrationCallbacks">The registration callbacks.</param>
        public InlineRegistrationModule(params Action<IContainerBuilder>[] registrationCallbacks)
        {
            this.RegistrationCallbacks = registrationCallbacks;
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The ContainerBuilder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            this.RegistrationCallbacks.ForEach(callback => callback(containerBuilder));
        }
    }
}