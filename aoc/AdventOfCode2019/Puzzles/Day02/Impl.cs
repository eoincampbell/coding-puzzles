using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day02
{
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : BasePuzzle<string,int>
    {
        public Impl() : base("Day 02", ".\\Puzzles\\Day02\\Input.txt")
        {
        }

        //4462686
        public override Task<int> RunPart1Async()
        {
            var c = GetCode();

            c[1] = 12;
            c[2] = 2;

            var r = RunIntCode(c);

            return Task.FromResult(r);
        }

        //5936
        public override Task<int> RunPart2Async()
        {
            var c = GetCode();

            var r = Enumerable.Range(0, 100).ToList();

            foreach (var n in r)
            foreach (var v in r)
            {
                var cc = (int[]) c.Clone();
                cc[1] = n;
                cc[2] = v;
                if (RunIntCode(cc) == 19690720)
                    return Task.FromResult((n * 100) + v);
            }

            return Task.FromResult(0);
        }

        public int [] GetCode() => Inputs
            .First()
            .Split(',')
            .Select(s => Convert.ToInt32(s))
            .ToArray();

        public int RunIntCode(int[] c)
        {
            var i = 0; 

            while (c[i] != 99)
            {
                c[c[i + 3]] = c[i] switch
                    {
                        1 => c[c[i + 1]] + c[c[i + 2]],
                        2 => c[c[i + 1]] * c[c[i + 2]],
                        _ => throw new NotSupportedException()
                    };
                i += 4;
            }

            return c[0];
        }
    }
}
