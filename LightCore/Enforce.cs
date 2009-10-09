using System;
using System.Globalization;
using System.Linq.Expressions;

namespace LightCore
{
    /// <summary>
    /// Contains methods to enforce argument checking.
    /// </summary>
    public static class Enforce
    {
        /// <summary>
        /// Enforce that the given value is not null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="argumentExpression">The expression that resolves the name of the argument.</param>
        /// <returns>The value.</returns>
        public static T NotNull<T>(T value, Expression<Func<T>> argumentExpression) where T : class
        {
            // Check if the value is null. If so, throw an exception.
            if(value == null)
            {
                throw new ArgumentNullException(GetArgumentName(argumentExpression));
            }

            // Otherwise, return the value.
            return value;
        }

        /// <summary>
        /// Gets the name of the argument within the given argument expression.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argumentExpression">The argument expression.</param>
        /// <returns>The name of the argument.</returns>
        private static string GetArgumentName<T>(Expression<Func<T>> argumentExpression)
        {
            var memberExpression =
                (MemberExpression)argumentExpression.Body;
            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Enforce that the given string is neither null nor empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="argumentExpression">The expression that resolves the name of the argument.</param>
        /// <returns>The value.</returns>
        public static string NotNullOrEmpty(string value, Expression<Func<string>> argumentExpression)
        {
            // Check if the value is null or empty. If so, throw an exception.
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture,
                                                          "The argument '{0}' is null or empty.", GetArgumentName(argumentExpression)));
            }

            // Otherwise, return the value.
            return value;
        }
    }
}