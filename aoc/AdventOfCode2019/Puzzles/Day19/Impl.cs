/*
 * Day 19: Tractor Beam
 * https://adventofcode.com/2019/day/19
 * Part 1: 121
 * Part 2: 15090773         (for 10x10: 1420073)
 */
namespace AdventOfCode2019.Puzzles.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Threading.Tasks;
    using Base;
    using Base.IntCode;

    public class Impl : Puzzle<string, int>
    {        
        private readonly bool _render;
        private int _rows;

        public Impl() : this (false) { }
        
        public Impl(bool render) :  base("Day 19: Tractor Beam", ".\\Puzzles\\Day19\\Input.txt") => _render = render;

        public override async Task<int> RunPart1Async()
            => await Task.Run(() =>
            {
                var initState = Inputs[0] != null
                    ? Array.ConvertAll(Inputs[0].Split(','), BigInteger.Parse)
                    : Array.Empty<BigInteger>();
                var resCounter = 0;
                
                for (var y = 0; y < 30; y++)
                {
                    bool rowStarted = false, rowEnded = false;
                    for (var x = 0; x < 50; x++)
                    {
                        var vm = new IntCodeVm(initState);
                        vm.SetInput(x);
                        vm.SetInput(y);
                        vm.RunProgramUntilHalt();

                        var result = vm.GetOutput();
                        
                        if (result == 1)
                        {
                            resCounter++;
                            if (!rowStarted) rowStarted= true;
                        }
                        
                        if (rowStarted && result == 0)
                            rowEnded = true;
                        

                        if (_render)
                            Console.Write((result == 1 ? '#' : '.') + ((rowEnded || x == 49) ? Environment.NewLine : ""));

                        if(rowEnded) break;
                    }

                    _rows++;
                }

                _rows++;
                return resCounter;
            });

        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var initState = Inputs[0] != null
                    ? Array.ConvertAll(Inputs[0].Split(','), BigInteger.Parse)
                    : Array.Empty<BigInteger>();
                
                var coords = new HashSet<(int x, int y)>();

                (int x, int y) pos = (0, 0);
                int recsize = 100, bound = recsize - 1,prevXStart = 0;

                for (var y = 0; y < 10000; y++)
                {
                    bool rowStarted = false, rowEnded = false;
                    
                    for (var x = prevXStart; x < 10000; x++)
                    {
                        var vm = new IntCodeVm(initState);
                        vm.SetInput(x);
                        vm.SetInput(y);
                        vm.RunProgramUntilHalt();

                        var result = vm.GetOutput();

                        if (result == 1 && !rowStarted)
                        {
                            rowStarted = true;
                            coords.Add((x, y));
                            prevXStart = x;
                        }

                        if (rowStarted && result == 0)
                        { 
                            coords.Add((x -1, y));
                            rowEnded = true;
                        }

                        if (_render)
                            Console.Write((result == 1 ? '#' : '.') + ((rowEnded) ? Environment.NewLine : ""));

                        if (coords.Contains((x - bound, y)) && coords.Contains((x, y - bound)))
                        {
                            pos = (x - bound, y - bound);
                            goto end;
                        }
                        
                        if(rowEnded) break;
                    }
                }

                end:

                if(_render)
                    for (var yy = _rows + pos.y; yy < _rows + pos.y + recsize; yy++)
                    for (var xx = pos.x; xx < pos.x + recsize; xx++)
                    {
                        Console.SetCursorPosition(xx, yy);
                        Console.WriteLine('O');
                    }

                return (pos.x * 10000) + pos.y; //1420073
            });
    }
}