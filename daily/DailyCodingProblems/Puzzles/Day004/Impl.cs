namespace DailyCodingProblems.Puzzles.Day004
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CodingPuzzles.Helpers;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 004") { }

        protected override async Task<string> ExecuteImpl()
        {
            List<int[]> tests = new List<int[]>
            {
                new int [0], //should return 1
                new[] {1, 2, 3, 4, 5, 6}, //should return 7
                new[] {1, 4, 3, 5, 6, 2}, //should return 7
                new[] {6, 1, 2, 3, 4, 5}, //should return 7 WCS with back tracking
                new[] {1, 4, -1, 5, 6, 2}, //should return 3
                new[] {1, 100, 99, 98, 97}, //should return 2
                new[] {3, 4, -1, 1}, //should return 2
                new[] {3, 2, 4, -1, 1} //should return 5

            };

            foreach (var arr in tests)
            {
                var (m, i) = Process(arr);
                Console.WriteLine($"{arr.ToCsv()} is missing {m}. Took {i} iterations");
            }

            var a = new [] {3, 4, -1, 1};
            var (missing, iter) = Process(a);
            return await Task.FromResult($"{a.ToCsv()} is missing {missing}. Took {iter} iterations");
        }

        private static (int,int) Process(IList<int> arr)
        {
            var iterCount = 0;
            var l = arr.Count;
            if (l == 0) return (1, iterCount);               //empty list

            for (var i = 0; i < arr.Count; i++)
            {
                iterCount++;
                var c = arr[i];

                if (c < 0) continue;            //get rid of negative numbers
                if (c > l - 1) continue;        //get rid of elements beyond the arr bounds
                if (c == arr[c - 1]) continue;  //int is already in its correct slot

                (arr[c - 1], arr[i]) = (arr[i], arr[c - 1]); //otherwise swap them <3 tuple swaps
                i--;                            //if a swap occurred, step back one and reprocess the current slot
            }

            for (var i = 0; i < l; i++)
                if (arr[i] != i + 1)
                    return (i + 1, iterCount);   //return the +1 index of the first slot that doesn't contain the correct value 
            
            return (l + 1, iterCount);           //return count+1 
        }
    }
}
