using System;
using System.Collections.Generic;

namespace PeterBucher.AutoFunc.ExtensionMethods
{
    public static class SystemCollectionsGenericExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}