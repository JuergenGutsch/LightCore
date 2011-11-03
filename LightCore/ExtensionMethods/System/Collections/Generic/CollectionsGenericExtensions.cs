using System;
using System.Collections.Generic;
using System.Linq;

namespace LightCore.ExtensionMethods.System.Collections.Generic
{
    /// <summary>
    /// Represents extensionmethods for System.Collection.Generic namespace.
    /// </summary>
    internal static class CollectionsGenericExtensions
    {
        /// <summary>
        /// Executes an action for each item in the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of an item.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action to execute.</param>
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Merges two dictionaries together.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="dictionaryToMerge">The dictionary to merge with.</param>
        /// <returns>A new dictionary which represents the merged result of <paramref name="source"/> and <paramref name="dictionaryToMerge"/>.</returns>
        internal static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionaryToMerge)
        {
            var result = new Dictionary<TKey, TValue>();
            var dictionaries = new List<IDictionary<TKey, TValue>>();

            foreach (var x in dictionaries.SelectMany(dict => dict))
            {
                result[x.Key] = x.Value;
            }

            return result;
        }
    }
}