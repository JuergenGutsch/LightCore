﻿using System;

using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents a registration.
    /// </summary>
    internal class RegistrationItem
    {
        /// <summary>
        /// Gets the key for this registration.
        /// </summary>
        internal RegistrationKey Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the implementation type for this registration.
        /// </summary>
        internal Type ImplementationType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the activator.
        /// </summary>
        internal IActivator Activator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scope that holds the reuse strategy.
        /// </summary>
        internal ILifecycle Lifecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments for object creations.
        /// </summary>
        internal ArgumentContainer Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the runtime arguments for object creations.
        /// </summary>
        internal ArgumentContainer RuntimeArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationItem" />.
        /// </summary>
        internal RegistrationItem()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationItem" />.
        /// </summary>
        /// <param name="key">The registration key as <see cref="RegistrationKey" />.</param>
        internal RegistrationItem(RegistrationKey key)
            : this()
        {
            this.Key = key;
        }

        /// <summary>
        /// Activates the current registration.
        /// </summary>
        /// <param name="resolutionContext">The resolution context (e.g. for resolve inner depenencies).</param>
        /// <returns>The activated instance.</returns>
        internal object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this.Lifecycle.ReceiveInstanceInLifecycle(
                () => this.Activator.ActivateInstance(resolutionContext));
        }
    }
}