using LightCore.Registration;

namespace LightCore.Activation
{
    /// <summary>
    /// Represents a context for resolve instances
    /// with most used arguments.
    /// </summary>
    internal class ResolutionContext
    {
        /// <summary>
        /// The container.
        /// </summary>
        internal IContainer Container
        {
            get;
            set;
        }

        /// <summary>
        /// The registrations.
        /// </summary>
        internal RegistrationContainer Registrations
        {
            get;
            set;
        }

        /// <summary>
        /// The arguments.
        /// </summary>
        internal ArgumentContainer Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// The runtime arguments.
        /// </summary>
        internal ArgumentContainer RuntimeArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ResolutionContext" />.
        /// <param name="container">The container.</param>
        /// <param name="registrations">The registrations.</param>
        /// </summary>
        internal ResolutionContext(IContainer container, RegistrationContainer registrations)
        {
            this.Container = container;
            this.Registrations = registrations;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ResolutionContext" />.
        /// <param name="container">The container.</param>
        /// <param name="registrations">The registrations.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="runtimeArguments">The runtime arguments.</param>
        /// </summary>
        internal ResolutionContext(IContainer container, RegistrationContainer registrations, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
            : this(container, registrations)
        {
            this.Arguments = arguments;
            this.RuntimeArguments = runtimeArguments;
        }
    }
}