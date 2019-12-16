/*
 * Day 08: Space Image Format
 * https://adventofcode.com/2019/day/8
 * Part 1: 1560
 * Part 2: PRINTS UGCUH
 */
namespace AdventOfCode2019.Puzzles.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, int>
    {
        private readonly bool _render;
        public Impl() : this(false) { }
        public Impl(bool render) : base("Day 08: Space Image Format", ".\\Puzzles\\Day08\\Input.txt") => _render = render;

        public override async Task<int> RunPart1Async()
            => await Task.Run(() =>
            {
                var (ones, twos) = (from l in GetLayers(Inputs[0])
                         orderby l.Count(s => s == '0')
                         select (ones: l.Count(s => s == '1'), twos: l.Count(s => s == '2'))).First();

                return ones * twos;
            });

        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var layers = GetLayers(Inputs[0]);
                for (var i = 0; i < 150; i++)
                    foreach (var layer in layers)
                        if (layer[i] != '2')
                        {
                            if(_render) Console.Write((layer[i] == '0' ? " " : "#") + ((i % 25 == 24) ? Environment.NewLine : ""));
                            break;
                        }

                return Inputs[0].Length;
            });
        public static IList<string> GetLayers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            return Enumerable.Range(0, input.Length / 150)
                  .Select(i => input[(i * 150)..((i * 150) + 150)])
                  .ToList();
        }
    }
}