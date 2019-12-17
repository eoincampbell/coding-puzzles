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
        private static readonly string [] Codes = {"A,B,B,A,C,A,C,A,C,B\n", "R,6,R,6,R,8,L,10,L,4\n", "R,6,L,10,R,8\n", "L,4,L,12,R,6,L,10\n", "n\n"};

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
            DrawScaffold(vm.GetOutputs().ToList());
            var scaffolds = _dict.Where(kv => kv.Value == '#').Select(kv => kv.Key);
            var sum = 0;
            foreach (var (sx, sy) in scaffolds)
            {
                var isIntX = true;
                foreach (var (tx, ty) in Pos.Select(p => (p.x + sx, p.y + sy)))
                    if (!_dict.ContainsKey((tx, ty)) || _dict[(tx, ty)] != '#')
                        isIntX = false;

                sum += isIntX ? (sx * sy) : 0;
            }
            return sum;
        });

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            var vm = new IntCodeVm(Inputs[0]);
            vm.SetValue(0, 2);
            foreach (var code in Codes)
                foreach (var chr in code) vm.SetInput((int)chr);

            vm.RunProgramUntilHalt();
            return (int) vm.GetOutputs().ToList().Last();
        });

        private void DrawScaffold(IReadOnlyList<BigInteger> results)
        {
            (int x, int y) p = (0, 0);
            foreach (var v in results)
            {
                _dict.Add(p, (char) v);
                p = (v == 10) ? (0, p.y + 1) : (p.x + 1, p.y);
            }

            if (!_render) return;
            foreach (var v in results) Console.Write((char) v);
        }
    }
}