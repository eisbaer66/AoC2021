using System;
using System.Collections.Generic;

namespace AoC2021.Logic.Utility
{
    public static class EnumerableExtensions
    {
        public static (IList<T>, IList<T>) UnZip<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var matches = new List<T>();
            var nonMatches = new List<T>();
            foreach (var item in items)
            {
                IList<T> list = predicate(item) ? matches : nonMatches;
                list.Add(item);
            }

            return (matches, nonMatches);
        }
    }
}