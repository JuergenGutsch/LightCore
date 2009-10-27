using System;
using System.Collections.Generic;

namespace LightCore.ExtensionMethods.System.Collections.Generic
{
  /// <summary>
  /// Represents extensionmethods for System.Collection.Generic namespace.
  /// </summary>
  public static class SystemCollectionsGenericExtensions
  {
    /// <summary>
    /// Executes an action for each item in the enumeration.
    /// </summary>
    /// <typeparam name="T">The type of an item.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="action">The action to execute.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
      {
        action(item);
      }
    }

    /// <summary>
    /// Converts an enuneration from an inputtype to an outputtype,
    /// with the given converter function.
    /// </summary>
    /// <typeparam name="TInput">The inputtype.</typeparam>
    /// <typeparam name="TOutput">The outputtype.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="converter">The converter function.</param>
    /// <returns>The converted source.</returns>
    public static IEnumerable<TOutput> Convert<TInput, TOutput>(this IEnumerable<TInput> source, Func<TInput, TOutput> converter)
    {
      foreach (var item in source)
      {
        yield return converter(item);
      }
    }
  }
}