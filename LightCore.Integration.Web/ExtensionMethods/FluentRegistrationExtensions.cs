using LightCore.Fluent;
using LightCore.Integration.Web.Reuse;

namespace LightCore.Integration.Web.ExtensionMethods
{
    /// <summary>
    /// Represents extensionmethods for LightCore.Fluent namespace.
    /// </summary>
    public static class FluentRegistrationExtensions
    {
        /// <summary>
        /// Treat the current registration to reuse the instance per http request.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The instance itself to get fluent working.</returns>
        public static IFluentRegistration ScopedToHttpRequest(this IFluentRegistration source)
        {
            return source.ScopedTo<HttpRequestReuseStrategy>();
        }
    }
}