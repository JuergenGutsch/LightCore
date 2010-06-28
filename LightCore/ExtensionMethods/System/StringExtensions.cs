using System;
using System.ComponentModel;
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

        /// <summary>
        /// Converts the source string to specified type. If a conversion is not possible or fails, the
        /// specified default value is used.
        /// </summary>
        /// <param name="value">The source string.</param>
        /// <param name="typeToConvert">The type to convert to.</param>
        /// <returns>An instance of the specified type.</returns>
        public static object ToOrDefault(this string value, Type typeToConvert)
        {
#if SL2 || SL3 || CF35
            try
            {
                switch (Type.GetTypeCode(typeToConvert))
                {
                    case TypeCode.Int32:
                        return int.Parse(value);
                    case TypeCode.Boolean:
                        return bool.Parse(value);
                    case TypeCode.DateTime:
                        return DateTime.Parse(value);
                    default:
                        if (typeToConvert == typeof(Guid))
                        {
                            return new Guid(value);
                        }

                        return value;
                }
            }
            catch (Exception)
            {
                return value;
            }
#else
            object returnValue = value;

            // If there is no source string, break.
            if (returnValue == null)
            {
                return returnValue;
            }

            // Check whether string can be converted to the target type. If not, break.
            TypeConverter converter = TypeDescriptor.GetConverter(typeToConvert);
            if (!converter.CanConvertFrom(typeof(string)))
            {
                return returnValue;
            }

            // Try to convert to the target type. If this fails, ignore the exception.
            try
            {
                returnValue = converter.ConvertFrom(value);
            }
            catch (Exception)
            {
            }

            // Return to the caller.
            return returnValue;
#endif
        }
    }
}