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

        public Impl() : this(false) { }

        public Impl(bool render) : base("Day 19: Tractor Beam", ".\\Puzzles\\Day19\\Input.txt") => _render = render;

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
        {
            var vm = new IntCodeVm(Inputs[0]);
            var c = 0;
            const int yL = 30;
            const int xL = 50;
            for (var y = 0; y < yL; y++)
            {
                bool rStart = false, rEnd = false;
                for (var x = 0; x < xL; x++)
                {
                    vm.Reset();
                    vm.SetInput(x);
                    vm.SetInput(y);
                    vm.RunProgramUntilHalt();
                    var res = vm.GetOutput();

                    c += (int) res;
                    if (res == 1 && !rStart) rStart = true;
                    
                    if (rStart && res == 0) rEnd = true;

                    if (_render) Console.Write((res == 1 ? '#' : '.') + ((rEnd || x == xL-1) ? "\r\n" : ""));

                    if (rEnd) break;
                }
            }

            return c;
        });

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            var coords = new HashSet<(int x, int y)>();
            (int X, int Y) pos;
            const int bound = 99; //for a rect of size 100 ;
            var vm = new IntCodeVm(Inputs[0]);

            for (int y = 0, prevXStart = 0, prevXEnd = 0;; y++)
            {
                bool rStart = false, rEnd = false;
                for (var x = prevXStart;; x++)
                {
                    vm.Reset();
                    vm.SetInput(x);
                    vm.SetInput(y);
                    vm.RunProgramUntilHalt();
                    var result = vm.GetOutput();

                    if (result == 1 && !rStart) {
                        rStart = true;
                        coords.Add((x, y));
                        prevXStart = x;
                    }

                    if (result == 0 && rStart) {
                        rEnd = true;
                        coords.Add((x - 1, y));
                        prevXEnd = x - 1;
                    }

                    if (coords.Contains((x - bound, y)) && coords.Contains((x, y - bound))) {
                        pos = (x - bound, y - bound);
                        goto end;
                    }

                    
                    if (rEnd) break;
                }
                if (prevXEnd - prevXStart < 100)
                    y+=100;

            }

            end: return (pos.X * 10000) + pos.Y; //1420073
        });
    }
}

/*
 * 
    if (_render)
                Console.Write((result == 1 ? '#' : '.') + ((rowEnded) ? Environment.NewLine : ""));

 *
 * if(_render)
                for (var yy = _rows + pos.y; yy < _rows + pos.y + recsize; yy++)
                for (var xx = pos.x; xx < pos.x + recsize; xx++)
                {
                    Console.SetCursorPosition(xx, yy);
                    Console.WriteLine('O');
                }
 *
 */