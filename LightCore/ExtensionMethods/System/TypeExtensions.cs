using System;

namespace LightCore.ExtensionMethods.System
{
    /// <summary>
    /// Represents extensionmethods for <see cref="Type" /> type.
    /// </summary>
    internal static class SystemExtensions
    {
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