namespace DailyCodingProblems.Puzzles.Day15
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class Day15Extensions
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            var rand = new Random();
            var count = 0;
            var result = default(T);

            foreach (var element in source)
            {
                if (count == 0) result = element;
                else if (rand.Next(count) == count - 1) result = element;
                count++;
            }
            return result;
        }
    }

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 015") { }

        protected override async Task<string> ExecuteImpl()
        {
            var rand = new Random();
            const int length = 100;
            const double attempts = 100000;

            //Test 1
            var list = Generator(length).ToList();
            var test1Results = new Dictionary<int, int>();
            for (var i = 0; i < attempts; i++)
            {
                var r1 = list[rand.Next(0, length)];
                if (!test1Results.ContainsKey(r1)) test1Results.Add(r1, 1);
                else test1Results[r1]++;
            }
            foreach(var t1 in test1Results.Keys)
                Console.WriteLine($"{t1} - {test1Results[t1]:000000} - {(test1Results[t1]/attempts):P}");


            //Test2
            var test2Results = new Dictionary<int, int>();
            for (var i = 0; i < attempts; i++)
            {
                var r2 = Generator(length).GetRandom();
                if (!test2Results.ContainsKey(r2)) test2Results.Add(r2, 1);
                else test2Results[r2]++;
            }
            foreach (var t2 in test2Results.Keys)
                Console.WriteLine($"{t2} - {test2Results[t2]:000000} - {(test2Results[t2]/attempts):P}");


            return await Task.FromResult($"");





        }

        

        private IEnumerable<int> Generator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                yield return i % 10;
            }
        }
    }
}
