using PeterBucher.AutoFunc.Fluent;
using PeterBucher.AutoFunc.Integration.Web.Reuse;

namespace PeterBucher.AutoFunc.Integration.Web.ExtensionMethods
{
    /// <summary>
    /// Represents extensionmethods for PeterBucher.AutoFunc.Fluent namespace.
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
            return source.ScopedTo(new HttpRequestReuseStrategy());
        }
    }
}