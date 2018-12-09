namespace AdventOfCode2017.Puzzles.Day4
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 4 ", ".\\Puzzles\\Day4\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            int v = 0;
            foreach (var i in Inputs)
            {
                var valid = i.Split(' ')
                    .GroupBy(g => g)
                    .Select(r => new { r.Key, Count = r.Count() })
                    .All(w => w.Count == 1);

                if (valid) v++;
            }
            return await Task.FromResult($"{v}");
        }

        public override async Task<string> RunPart2()
        {
            int valid = 0;

            foreach(var i in Inputs)
            {
                var words = i.Split(' ');

                var invalid = words.GroupBy(g => g)
                    .Select(r => new { r.Key, Count = r.Count() })
                    .Any(w => w.Count > 1);

                if (invalid) continue;

                bool checkWords()
                {
                    foreach (var w1 in words)
                    {
                        foreach (var w2 in words)
                        {
                            var w1cs = w1.ToCharArray();
                            var w2cs = w2.ToCharArray();
                            Array.Sort(w1cs);
                            Array.Sort(w2cs);
                            if (w1 != w2 && w1cs.SequenceEqual(w2cs))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }

                if (checkWords())
                    valid++; 

            }


            return await Task.FromResult($"{valid}");
        }
    }
}