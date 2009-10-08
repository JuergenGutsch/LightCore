using LightCore.Activation;
using LightCore.Fluent;
using LightCore.Reuse;

namespace LightCore
{
    /// <summary>
    /// Represents a registration.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Gets the key for this registration.
        /// </summary>
        public RegistrationKey Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the activator for an instance.
        /// </summary>
        public IActivator Activator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reuse strategy for the instance creation.
        /// </summary>
        public IReuseStrategy ReuseStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments for object creations.
        /// </summary>
        public object[] Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current fluent interface instance.
        /// </summary>
        public IFluentRegistration FluentRegistration
        {
            get
            {
                return new FluentRegistration(this);
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        public Registration()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        /// <param name="key">The registration key as <see cref="RegistrationKey" />.</see></param>
        public Registration(RegistrationKey key)
        {
            this.Key = key;
        }
    }
}