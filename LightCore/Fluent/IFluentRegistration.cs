using PeterBucher.AutoFunc.Reuse;

namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public interface IFluentRegistration : IFluentInterface
    {
        /// <summary>
        /// Treat the current registration to be transient.
        /// One instance per request.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ScopedToTransient();

        /// <summary>
        /// Treat the current registration to singleton LifeTime.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ScopedToSingleton();

        /// <summary>
        /// Treat the current registration to the passed reuse strategy behaviour.
        /// </summary>
        /// <param name="reuseStrategy">The reuse strategy.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ScopedTo(IReuseStrategy reuseStrategy);

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithArguments(params object[] arguments);

        /// <summary>
        /// Indicates that the default constructor should be used.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration UseDefaultConstructor();

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithName(string name);
    }
}