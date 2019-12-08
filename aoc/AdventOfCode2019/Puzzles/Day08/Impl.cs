/*
 * Day 08: 
 * https://adventofcode.com/2019/day/8
 * Part 1: 
 * Part 2: 
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
        public Impl() : base("Day 08: ", ".\\Puzzles\\Day08\\Input.txt") { }

        public IList<string> GetLayers(string input)
            => Enumerable.Range(0, input.Length / 150)
                .Select(i => input[(i * 150)..((i * 150) + 150)])
                .ToList();
        

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
            {
                var r = (from l in GetLayers(Inputs[0])
                         orderby l.Count(s => s == '0')
                         select (l.Count(s => s == '1'), l.Count(s => s == '2'))).First();

                return r.Item1 * r.Item2;
            });

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
            {
                var layers = GetLayers(Inputs[0]);
                for(var i = 0; i < 150; i++)
                    foreach(var layer in layers)
                        if (layer[i] != '2') 
                        {
                            Console.Write((layer[i] == '0' ? " " : "#") + ((i % 25 == 24) ? Environment.NewLine : ""));
                            break;
                        }
                
                return Inputs[0].Length;
            });
    }
}