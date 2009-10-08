using System;
using System.Collections.Generic;

namespace LightCore
{
    public class InlineRegistrationModule : RegistrationModule
    {
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

        public override void Register(IContainerBuilder containerBuilder)
        {
            this.RegistrationCallBacks.ForEach(callback => callback(containerBuilder));
        }
    }
}