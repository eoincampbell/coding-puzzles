namespace DailyCodingProblems.Puzzles.Day1
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 001") { }

        protected async override Task<string> ExecuteImpl()
        {
            return await Task.FromResult(ImplA() + " " + ImplB());
        }

        private bool ImplA()
        {
            var arr = new int[] { 10, 3, 15, 7 };
            var k = 17;

            Array.Sort(arr);
            int i = 0, j = arr.Length - 1;
            while (i < j)
            {
                if (arr[i] + arr[j] == k) return true;
                else if (arr[i] + arr[j] > k) j--;
                else if (arr[i] + arr[j] < k) i++;
            }
            return false;
        }

        private bool ImplB()
        {
            var arr = new int[] { 10, 3, 15, 7 };
            var k = 17;

            var hs = new HashSet<int>();
            foreach (var n in arr)
            {
                if (hs.Contains(n)) return true;
                else hs.Add(k - n);
            }
            return false;
        }
    }
}
