using LightCore.Lifecycle;

namespace LightCore.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public interface IFluentRegistration : IFluentInterface
    {
        /// <summary>
        /// Treat the current registration to the passed reuse strategy behaviour.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration ControlledBy<TLifecycle>() where TLifecycle : ILifecycle, new();

        /// <summary>
        /// Adds arguments to the registration.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithArguments(params object[] arguments);

        /// <summary>
        /// Gives a name to the registration.
        /// </summary>
        /// <param name="name">The registration name.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        IFluentRegistration WithName(string name);
    }
}