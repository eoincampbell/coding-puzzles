using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public static class Extensions
    {
        public static string ToCsv<T>(this IEnumerable<T> input)
        {
            return string.Join(",", input);
        }

        public static SortedList<TKey, TValue> ToSortedList<TSource, TKey, TValue>
        (this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            // Argument checks elided
            SortedList<TKey, TValue> ret = new SortedList<TKey, TValue>();
            foreach (var item in source)
            {
                // Will throw if the key already exists
                ret.Add(keySelector(item), valueSelector(item));
            }
            return ret;
        }
    }
}
