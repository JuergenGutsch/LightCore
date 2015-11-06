using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightCore.ExtensionMethods.System
{
    /// <summary>
    /// Represents extensionmethods for <see cref="object" /> type.
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Converts a anonymous type to a dictionary{string, object}.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A dictionary that holds the name / values of the passed object.</returns>
        internal static IDictionary<string, object> ToNamedArgumentDictionary(this object source)
        {
            return source
                .GetType()
                .GetProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(source, null));
        }
    }
}