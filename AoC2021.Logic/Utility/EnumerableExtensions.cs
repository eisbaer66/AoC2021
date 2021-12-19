using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.Utility
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }
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

        public static IEnumerable<int> IndexOfAll(this string template, string key)
        {
            var index = -1;
            while (true)
            {
                index = template.IndexOf(key, index + 1, StringComparison.InvariantCulture);
                if (index < 0)
                    yield break;
                yield return index;
            }
        }

        public static IEnumerable<(T a, T b)> CombinationsWithoutPermutation<T>(this ICollection<T> list)
        {
            return list.SelectMany((a, i) => list
                                             .Skip(i)
                                             .Select(b => (a, b)));
        }
    }
}