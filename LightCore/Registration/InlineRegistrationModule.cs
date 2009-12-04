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
        public IEnumerable<Action<IContainerBuilder>> RegistrationCallBacks
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InlineRegistrationModule" />.
        /// </summary>
        /// <param name="registrationCallBacks">The registration callbacks.</param>
        public InlineRegistrationModule(params Action<IContainerBuilder>[] registrationCallBacks)
        {
            this.RegistrationCallBacks = registrationCallBacks;
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The controllerbuilder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            this.RegistrationCallBacks.ForEach(callback => callback(containerBuilder));
        }
    }
}