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
            if(source == null)
            {
                return false;
            }

            bool isConcreteType = !source.IsAbstract && !source.IsInterface;

            isConcreteType &= !source.IsValueType;
            isConcreteType &= source != typeof (string);
            isConcreteType &= !IsFactoryType(source);

            return isConcreteType;
        }

        /// <summary>
        /// Checks whether the type is a generic factory or not.
        /// </summary>
        /// <param name="source">The type to check.</param>
        /// <returns><value>true</value> if the type is a generic factory, otherwise <value>false</value>.</returns>
        internal static bool IsFactoryType(this Type source)
        {
            if (source == null || !source.IsGenericType)
            {
                return false;
            }

            Type genericTypeDefinition = source.GetGenericTypeDefinition();

            return genericTypeDefinition == typeof(Func<>)
                   || genericTypeDefinition == typeof(Func<,>)
                   || genericTypeDefinition == typeof(Func<,,>)
                   || genericTypeDefinition == typeof(Func<,,,>)
                   || genericTypeDefinition == typeof(Func<,,,,>);
        }

        /// <summary>
        /// Checks whether a given type is type of generic enumerable.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <returns><true /> if the parameter type is a generic enumerable, otherwise <false /></returns>
        internal static bool IsGenericEnumerable(this Type source)
        {
            if (source == null || !source.IsGenericType)
            {
                return false;
            }

            var typeArguments = source.GetGenericArguments();

            if (typeArguments.Length > 1)
            {
                return false;
            }

            return typeof(IEnumerable<>).MakeGenericType(typeArguments).IsAssignableFrom(source);
        }
    }
}