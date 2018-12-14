using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPuzzles.Helpers
{
    public static class Extensions
    {
        //public static int[] ToDigitArray(this int digits)
        //{
        //    return digits.ToString().Select(o => Convert.ToInt32(o - 48)).ToArray();
        //}

        private static int numDigits(int n)
        {
            if (n < 0) n = (n == Int32.MinValue) ? Int32.MaxValue : -n;
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            if (n < 100000) return 5;
            if (n < 1000000) return 6;
            if (n < 10000000) return 7;
            if (n < 100000000) return 8;
            if (n < 1000000000) return 9;
            return 10;
        }

        public static int[] ToDigitArray(this int digits)
        {
            var result = new int[numDigits(digits)];
            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = digits % 10;
                digits /= 10;
            }
            return result;
        }

        public static string ToS<T>(this IEnumerable<T> input)
        {
            return string.Join("", input);
        }
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
