/*
 * Day 13: 
 * https://adventofcode.com/2019/day/13
 * Part 1: 
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day13
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
        private static Dictionary<int, char> _tiles = new Dictionary<int, char>
        {
            {0, ' '},
            {1, '#'},
            {2, 'u'},
            {3, '_'},
            {4, 'o'}
        };


        public Impl() : base("Day 13: ", ".\\Puzzles\\Day13\\Input.txt") { }

        public override async Task<int> RunPart1Async()
             => await Task.Run(() =>
             {
                 var vm = new IntCodeVm(Inputs[0]);

                 var halted = vm.RunProgram();

                 var results = vm.GetOutputs().ToList();

                 var tiles = new Dictionary<(int x, int y), int>();

                 int maxx = 0, minx = int.MaxValue, maxy = 0, miny = int.MaxValue;
                 int blocks = 0;

                 for (var i = 0; i < results.Count; i+=3)
                 {
                     var x = (int)results[i];
                     var y = (int)results[i+1];
                     var t = (int)results[i+2];
                     tiles.Add((x, y), t);

                     if(x < minx) minx = x;
                     if(x > maxx) maxx = x;
                     if(y < miny) miny = y;
                     if(y > maxy) maxy = y;

                     if (t == 2) blocks++;
                 }

                 for(var yy = miny; yy <= maxy; yy++)
                 {
                     for (var xx = minx; xx <= maxx; xx++)
                     {
                         if (tiles.ContainsKey((xx, yy)))
                         {
                             var tt = tiles[(xx,yy)];
                             Console.Write(_tiles[tt]);
                         }
                         else
                         {
                             Console.Write(" ");
                         }

                         if (xx == maxx)
                             Console.WriteLine("");
                     }
                 }


                 return blocks;
             });


        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var vm = new IntCodeVm(Inputs[0]);

                var halted = vm.RunProgram();

                var results = vm.GetOutputs().ToList();

                var tiles = new Dictionary<(int x, int y), int>();

                int maxx = 0, minx = int.MaxValue, maxy = 0, miny = int.MaxValue;
                int blocks = 0;

                for (var i = 0; i < results.Count; i+=3)
                {
                    var x = (int)results[i];
                    var y = (int)results[i+1];
                    var t = (int)results[i+2];
                    tiles.Add((x, y), t);

                    if(x < minx) minx = x;
                    if(x > maxx) maxx = x;
                    if(y < miny) miny = y;
                    if(y > maxy) maxy = y;

                    if (t == 2) blocks++;
                }

                for(var yy = miny; yy <= maxy; yy++)
                {
                    for (var xx = minx; xx <= maxx; xx++)
                    {
                        if (tiles.ContainsKey((xx, yy)))
                        {
                            var tt = tiles[(xx,yy)];
                            Console.Write(_tiles[tt]);
                        }
                        else
                        {
                            Console.Write(" ");
                        }

                        if (xx == maxx)
                            Console.WriteLine("");
                    }
                }


                return blocks;
            });
    }
}