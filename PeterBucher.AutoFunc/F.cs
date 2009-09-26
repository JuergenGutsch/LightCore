using System;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents a static helper class for negating functions.
    /// </summary>
    public static class F
    {
        /// <summary>
        /// Negates the innerFunction.
        /// </summary>
        /// <param name="innerFunction">The function to negate.</param>
        /// <returns>The negated function.</returns>
        public static Func<bool> Not(Func<bool> innerFunction)
        {
            return () => !innerFunction();
        }

        /// <summary>
        /// Negates the innerFunction.
        /// </summary>
        /// <typeparam name="T1">The first.</typeparam>
        /// <param name="innerFunction">The function to negate.</param>
        /// <returns>The negated function.</returns>
        /// <summary>
        public static Func<T1, bool> Not<T1>(
            Func<T1, bool> innerFunction)
        {
            return x => !innerFunction(x);
        }

        /// <summary>
        /// Negates the innerFunction.
        /// </summary>
        /// <typeparam name="T1">The first.</typeparam>
        /// <typeparam name="T2">The second.</typeparam>
        /// <param name="innerFunction">The function to negate.</param>
        /// <returns>The negated function.</returns>
        /// <summary>
        public static Func<T1, T2, bool> Not<T1, T2>(
            Func<T1, T2, bool> innerFunction)
        {
            return (x, y) => !innerFunction(x, y);
        }

        /// <summary>
        /// Negates the innerFunction.
        /// </summary>
        /// <typeparam name="T1">The first.</typeparam>
        /// <typeparam name="T2">The second.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <param name="innerFunction">The function to negate.</param>
        /// <returns>The negated function.</returns>
        /// <summary>
        public static Func<T1, T2, T3, bool> Not<T1, T2, T3>(
            Func<T1, T2, T3, bool> innerFunction)
        {
            return (x, y, z) => !innerFunction(x, y, z);
        }

        /// <summary>
        /// Negates the innerFunction.
        /// </summary>
        /// <typeparam name="T1">The first.</typeparam>
        /// <typeparam name="T2">The second.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <param name="innerFunction">The function to negate.</param>
        /// <returns>The negated function.</returns>
        /// <summary>
        public static Func<T1, T2, T3, T4, bool> Not<T1, T2, T3, T4>(
            Func<T1, T2, T3, T4, bool> innerFunction)
        {
            return (x, y, z, a) => !innerFunction(x, y, z, a);
        }
    }
}