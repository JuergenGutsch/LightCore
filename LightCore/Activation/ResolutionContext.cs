using LightCore.Registration;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents a context for resolving instances
    /// with most used arguments.
    /// </summary>
    internal class ResolutionContext
    {
        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        internal IContainer Container
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        internal RegistrationItem Registration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the registrations.
        /// </summary>
        internal IRegistrationContainer RegistrationContainer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        internal ArgumentContainer Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the runtime arguments.
        /// </summary>
        internal ArgumentContainer RuntimeArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ResolutionContext" />.
        /// </summary>
        internal ResolutionContext()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ResolutionContext" />.
        /// <param name="container">The container.</param>
        /// <param name="registrations">The registrations.</param>
        /// </summary>
        internal ResolutionContext(IContainer container, IRegistrationContainer registrations)
            : this()
        {
            this.Container = container;
            this.RegistrationContainer = registrations;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ResolutionContext" />.
        /// <param name="container">The container.</param>
        /// <param name="registrations">The registrations.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="runtimeArguments">The runtime arguments.</param>
        /// </summary>
        internal ResolutionContext(IContainer container, IRegistrationContainer registrations, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
            : this(container, registrations)
        {
            this.Arguments = arguments;
            this.RuntimeArguments = runtimeArguments;
        }
    }
}