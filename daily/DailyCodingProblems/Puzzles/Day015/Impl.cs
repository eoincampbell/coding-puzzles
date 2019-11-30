namespace DailyCodingProblems.Puzzles.Day015
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            const double attempts = 1000000;
            var list = Generator(length).ToList();
            
            //Test 1
            var res = new Dictionary<int, int>();
            for (var i = 0; i < attempts; i++)
            {
                var r = list[rand.Next(0, length)];
                res[r] = res.ContainsKey(r) ? res[r] + 1 : 1;
            }
            res.OrderBy(k => k.Key)
                .ToList()
                .ForEach(f => Console.WriteLine(Print(f.Key, f.Value, attempts)));    

            //Test2
            res = new Dictionary<int, int>();
            for (var i = 0; i < attempts; i++)
            {
                var r = Generator(length).GetRandom();
                res[r] = res.ContainsKey(r) ? res[r] + 1 : 1;
            }
            res.OrderBy(k => k.Key)
                .ToList()
                .ForEach(f => Console.WriteLine(Print(f.Key, f.Value, attempts)));


            return await Task.FromResult($"");
        }

        private static string Print(int k, int v, double a) => $"{k} - {v:000,000} - {(v / a):P}";

        private static IEnumerable<int> Generator(int n)
        {
            for (var i = 0; i < n; i++) yield return i % 10;    
        }
    }
}
