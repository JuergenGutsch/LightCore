using System;
using System.Globalization;

namespace LightCore.ExtensionMethods.System
{
    /// <summary>
    /// Represents extensionmethods for <see cref="String" /> type.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Formats a string with given arguments.
        /// </summary>
        /// <param name="source">The format string.</param>
        /// <param name="values">The values.</param>
        /// <returns>The formatted string.</returns>
        internal static string FormatWith(this string source, params object[] values)
        {
            return string.Format(CultureInfo.InvariantCulture, source, values);
        }
    }
}