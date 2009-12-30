using System;

namespace LightCore.ExtensionMethods.System
{
    /// <summary>
    /// Represents extensionmethods for System namespace.
    /// </summary>
    internal static class SystemExtensions
    {
        /// <summary>
        /// Formats a string with given arguments.
        /// </summary>
        /// <param name="source">The format string.</param>
        /// <param name="values">The values.</param>
        /// <returns>The formatted string.</returns>
        internal static string FormatWith(this string source, params object[] values)
        {
            return string.Format(source, values);
        }

        /// <summary>
        /// Checks whether the type is concrete or not.
        /// </summary>
        /// <param name="source">The type to check.</param>
        /// <returns><value>true</value> if the type is concrete, otherwise <value>false</value>.</returns>
        internal static bool IsConcreteType(this Type source)
        {
            return !source.IsAbstract && !source.IsInterface;
        }
    }
}