﻿using System;
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

        public static string ToTsv<T>(this IEnumerable<T> input)
        {
            return string.Join("\t", input);
        }

        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var ret = new SortedList<TKey, TValue>();
            foreach (var item in source)
            {
                ret.Add(item.Key, item.Value);
            }
            return ret;
        }

        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var ret = new SortedList<TKey, TValue>();
            foreach (var item in source)
            {
                ret.Add(item.Key, item.Value);
            }
            return ret;
        }
    }
}