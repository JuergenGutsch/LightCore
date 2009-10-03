namespace System
{
    /// <summary>
    /// Represents extensionmethods for System namespace.
    /// </summary>
    public static class SystemExtensions
    {
        /// <summary>
        /// Formats a string with given arguments.
        /// </summary>
        /// <param name="source">The format string.</param>
        /// <param name="values">The values.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatWith(this string source, params object[] values)
        {
            return string.Format(source, values);
        }
    }
}