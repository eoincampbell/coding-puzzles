using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPuzzles.Helpers
{
    public static class Extensions
    {
        public static int Pow(this int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        private static int NumDigits(int n)
        {
            if (n < 0) n = (n == int.MinValue) ? int.MaxValue : -n;
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            if (n < 100000) return 5;
            if (n < 1000000) return 6;
            if (n < 10000000) return 7;
            if (n < 100000000) return 8;
            return n < 1000000000 ? 9 : 10;
        }

        public static int[] ToDigitArray(this int digits)
        {
            var result = new int[NumDigits(digits)];
            for (var i = result.Length - 1; i >= 0; i--)
            {
                result[i] = digits % 10;
                digits /= 10;
            }
            return result;
        }

        public static string ToS<T>(this IEnumerable<T> input) => string.Join("", input);
        public static string ToCsv<T>(this IEnumerable<T> input) => string.Join(",", input);
        public static string ToTsv<T>(this IEnumerable<T> input) => string.Join("\t", input);

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
