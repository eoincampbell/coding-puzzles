/*
 * Day 03: Crossed Wires
 * https://adventofcode.com/2019/day/3
 * Part 1: 399
 * Part 2: 15678
 */

namespace AdventOfCode2019.Puzzles.Day03
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Dict = System.Collections.Generic.Dictionary<(int x, int y), int>;

    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 03: Crossed Wires", ".\\Puzzles\\Day03\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            return await Task.Run(() => {
                var (a, b) = GetWires(Inputs[0], Inputs[1]);
                return a.Keys
                    .Intersect(b.Keys)
                    .Min(p => Math.Abs(p.x) + Math.Abs(p.y));
            });
        }

        public override async Task<int> RunPart2Async()
        {
            return await Task.Run(() => {
                var (a, b) = GetWires(Inputs[0], Inputs[1]);
                return a.Keys
                    .Intersect(b.Keys)
                    .Min(key => a[key] + b[key]);
            });
        }

        private (Dict firstWire, Dict secondWire) GetWires(string a, string b) 
            => (GetWire(a), GetWire(b));

        public static (char dir, int dist) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            return (input[0], int.Parse(input[1..], CultureInfo.CurrentCulture));
        }

        public static (int x, int y) GetPoint(char dir, ref int x, ref int y)
        {
            return dir switch
                {
                    'R' => (++x, y),
                    'U' => (x, ++y),
                    'L' => (--x, y),
                    'D' => (x, --y),
                    _ => throw new NotSupportedException()
                };
        }

        public static Dict GetWire(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var inst = input.Split(',').Select(Parse);
            int x = 0, y = 0, d = 0;
            var dict = new Dict();
            foreach (var (dir, dist) in inst)
                for (var l = 0; l < dist; l++)
                    dict.TryAdd(GetPoint(dir, ref x, ref y), ++d);
                
            return dict;
        }
    }
}
