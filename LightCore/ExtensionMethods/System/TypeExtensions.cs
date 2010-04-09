using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Checks whether a given type is type of generic enumerable.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <returns><true /> if the parameter type is a generic enumerable, otherwise <false /></returns>
        internal static bool IsGenericEnumerable(this Type source)
        {
            if (!source.IsGenericType)
            {
                return false;
            }

            var typeArguments = source.GetGenericArguments();

            if (typeof(IEnumerable<>).MakeGenericType(typeArguments).IsAssignableFrom(source))
            {
                return true;
            }

            return false;
        }
    }
}