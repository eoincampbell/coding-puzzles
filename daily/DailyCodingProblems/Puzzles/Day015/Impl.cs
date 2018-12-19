namespace DailyCodingProblems.Puzzles.Day15
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 004") { }

        protected override async Task<string> ExecuteImpl()
        {
            var rand = new Random();
            var length = 100;
            var attempts = 100000;

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
                Console.WriteLine($"{t1} - {test1Results[t1]:000000} - {((double)test1Results[t1] / (double)attempts):P}");
            

            //Test2
            int length2 = 1000000, count = 1, r2 = 0;
            var test2Results = new Dictionary<int, int>();

            for (var i = 0; i < attempts; i++)
            {
                r2 = GetRandomFromStream(Generator(length), rand);

                if (!test2Results.ContainsKey(r2)) test2Results.Add(r2, 1);
                else test2Results[r2]++;
            }



            foreach (var t2 in test2Results.Keys)
                Console.WriteLine($"{t2} - {test2Results[t2]:000000} - {((double)test2Results[t2] / (double)attempts):P}");


            return await Task.FromResult($"");





        }

        private int GetRandomFromStream(IEnumerable<int> stream, Random rand)
        {
            int count = 0, r2 = 0;
            foreach (var i in stream)
            {

                if (count == 0) // Always select the first element
                {
                    r2 = i;
                }
                else
                {
                    // Replace the previous random item with a new item with 1/count probability
                    var rx = rand.Next(0, count);
                    if (rx == count - 1)
                        r2 = i;
                }

                count++;
            }
            return r2;
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
