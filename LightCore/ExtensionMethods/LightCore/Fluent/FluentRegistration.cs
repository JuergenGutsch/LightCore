using LightCore.Fluent;
using LightCore.Lifecycle;

namespace LightCore.ExtensionMethods.LightCore.Fluent
{
  /// <summary>
  /// Represents extensionmethods for LightCore.Fluent namespace.
  /// </summary>
    public static class FluentRegistrationExtensions
    {
        /// <summary>
        /// Treat the current registration to use the passed lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public static IFluentRegistration StirbLangsamMit<TLifecycle>(this IFluentRegistration source) where TLifecycle : ILifecycle, new()
        {
            return source.ControlledBy<TLifecycle>();
        }

        /// <summary>
        /// Treat the current registration to use the passed lifecycle.
        /// </summary>
        /// <returns>The instance itself to get fluent working.</returns>
        public static IFluentRegistration DieSlowlyWithin<TLifecycle>(this IFluentRegistration source) where TLifecycle : ILifecycle, new()
        {
            return source.ControlledBy<TLifecycle>();
        }
    }
}