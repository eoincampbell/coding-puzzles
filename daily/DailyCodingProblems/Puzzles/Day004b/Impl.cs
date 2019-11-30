namespace DailyCodingProblems.Puzzles.Day4b
{
    using Base;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CodingPuzzles.Helpers;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 004") { }

        protected override async Task<string> ExecuteImpl()
        {
            var arr = new[] { 1, 2, 3, 6, 7, 7 };
            var sl = new SortedList<int,int>(new DecendingDuplicateComparer<int>());
            foreach (var i in arr) sl.Add(i, i);
            while (sl.Count() > 1)
            {
                Print(sl);

                var f = sl.First().Value; sl.RemoveAt(0);
                var s = sl.First().Value; sl.RemoveAt(0);

                if (f == s) continue;
                var remains = Math.Max(f, s) - Math.Min(f, s);
                sl.Add(remains, remains);
            }


            var result = sl.Any() ? sl.First().Key : 0;

            return await Task.FromResult($"{result}");
        }

        public void Print(SortedList<int, int> list)
        {
            Console.WriteLine(list.Values.ToList().ToCsv());
        }

        public class DecendingDuplicateComparer<TKey> : IComparer<TKey> where TKey : IComparable
        {
            public int Compare(TKey x, TKey y)
            {
                var r = y.CompareTo(x);
                return r == 0 ? 1 : r;
            }
        }

    }
}
