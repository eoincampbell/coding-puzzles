/*
 * Day 07: Amplification Circuit
 * https://adventofcode.com/2019/day/7
 * Part 1: 22012
 * Part 2: 4039164
 */
namespace AdventOfCode2019.Puzzles.Day07
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Combinatorics.Collections;
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 07: Amplification Circuit", ".\\Puzzles\\Day07\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            return await Task.Run(() =>
            {
                var c = new Permutations<int>(new int[] { 0, 1, 2, 3, 4 }, GenerateOption.WithoutRepetition);
                var bestOutput = 0;

                foreach (var phaseSettings in c)
                {
                    var nextInput = 0;
                    foreach (var ps in phaseSettings)
                    {
                        var vm = new IntCodeVm(Inputs[0]);
                        vm.AddInput(ps);
                        vm.AddInput(nextInput);
                        vm.RunProgram();
                        nextInput = vm.GetNextOutput();
                    }

                    if (nextInput > bestOutput)
                        bestOutput = nextInput;
                }
                return bestOutput;
            });
        }

        public override async Task<int> RunPart2Async()
        {
            return await Task.Run(() =>
            {
                var c = new Permutations<int>(new int[] { 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition);
                var bestOutput = 0;

                foreach (var phaseSettings in c)
                {
                    //var inputs = GetInputs(phaseSettings);
                    var vms = GetVms(phaseSettings);
                    var result = 0;

                    while (!vms[0].IsHalted)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            vms[i].RunProgramPauseAtOutput();
                            var nextOutput = vms[i].GetNextOutput();
                                
                            if (i == 4 && vms[4].IsHalted)
                                result = nextOutput;
                            else
                            {
                                var nextInputIdx = i + 1 == 5 ? 0 : i + 1;
                                vms[nextInputIdx].AddInput(nextOutput);
                            }
                        }
                    }

                    if (result > bestOutput)
                        bestOutput = result;
                }
                return bestOutput;
            });
        }

        private IList<IntCodeVm> GetVms(IList<int> phaseSettings)
        {
            return Enumerable.Range(0, 5)
                    .Select(r =>
                    {
                        var vm = new IntCodeVm(Inputs[0]);
                        vm.AddInput(phaseSettings[r]);
                        if (r == 0)
                            vm.AddInput(0);

                        return vm;
                    })
                    .ToList();
        }
    }
}