namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public class FluentRegistration : IFluentRegistration
    {
        /// <summary>
        /// The current registration.
        /// </summary>
        private readonly Registration _registration;

        /// <summary>
        /// Initializes a new instance of <see cref="FluentRegistration" /> type.
        /// </summary>
        /// <param name="registration">The current mapping item.</param>
        public FluentRegistration(Registration registration)
        {
            this._registration = registration;
        }

        /// <summary>
        /// Treat the current registration to singleton LifeTime.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration AsSingleton()
        {
            this._registration.LifeTime = LifeTime.Singleton;
            return this;
        }

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public IFluentRegistration WithArguments(params object[] arguments)
        {
            this._registration.Arguments = arguments;
            return this;
        }

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        public void WithName(string name)
        {
            this._registration.Key.Name = name;
        }
    }
}