/*
 * Day 17: Set and Forget
 * https://adventofcode.com/2019/day/17
 * Part 1: 5740
 * Part 2: 1022165
 */
namespace AdventOfCode2019.Puzzles.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {
        private readonly bool _render;
        private readonly Dictionary<(int x, int y), char> _dict;
        private static readonly (int x, int y) [] Pos = {(-1, 0), (0, -1), (1, 0), (0, 1)};

        public Impl() : this (false) { }
        
        public Impl(bool render) : base("Day 17: Set and Forget", ".\\Puzzles\\Day17\\Input.txt")
        {
            _render = render;
            _dict = new Dictionary<(int x, int y), char>();
        }

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
            {
                var vm = new IntCodeVm(Inputs[0]);
                vm.RunProgramUntilHalt();
                var results = vm.GetOutputs();
                DrawScaffold(results);

                var scaffolds = _dict.Where(kv => kv.Value == '#').Select(kv => kv.Key);
                var sum = 0;
                
                foreach (var (sx, sy) in scaffolds)
                {
                    var isIntersection = false;
                    foreach (var (tx, ty) in Pos.Select(p => (p.x + sx, p.y + sy)))
                    {
                        if (!_dict.ContainsKey((tx, ty)) || _dict[(tx, ty)] != '#')
                        {
                            isIntersection = false;
                            break;
                        }
                        isIntersection = true;
                    }
                    if (!isIntersection) continue;
                    sum += (sx * sy);
                }

                return sum;
            });

        private void DrawScaffold(IEnumerable<BigInteger> results)
        {
            if (_render)
            {
                Console.WriteLine("  |01234567890123456789012345678901234567890");
                Console.WriteLine("--+-----------------------------------------");
            }

            (int x, int y) p = (0, 0);
            foreach (var v in results)
            {
                if (_render && p.x == 0) Console.Write($"{p.y:00}|");
                _dict.Add(p, (char) v);
                p = (v == 10) ? (0, p.y + 1) : (p.x + 1, p.y);
                if (_render) Console.Write((char)v);
            }
        }

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            //Pen & Papered this part.
            string[] code = {
                "A,B,B,A,C,A,C,A,C,B\n",
                "R,6,R,6,R,8,L,10,L,4\n",
                "R,6,L,10,R,8\n",
                "L,4,L,12,R,6,L,10\n",
                "n\n"
            };

            var vm = new IntCodeVm(Inputs[0]);
            vm.SetValue(0, 2);
            foreach (var s in code)
            foreach (var c in s)
                vm.SetInput((int) c);

            vm.RunProgramUntilHalt();

            var r = vm.GetOutputs().ToList();
            return (int) r.Last();
        });
    }
}