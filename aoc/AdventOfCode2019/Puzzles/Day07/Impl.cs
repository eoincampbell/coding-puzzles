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
    
    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 07: Amplification Circuit", ".\\Puzzles\\Day07\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            var c = new Permutations<int>(new int[] { 0, 1, 2, 3, 4 }, GenerateOption.WithoutRepetition);
            var bestOutput = 0;

            foreach (var phaseSettings in c)
            {
                var nextInput = 0;
                foreach (var ps in phaseSettings)
                {
                    var inputs = new Queue<int>(new int[] { ps, nextInput });
                    var vm = new IntCodeVm(Inputs[0], inputs);
                    nextInput = vm.RunProgram().Last();
                }

                if (nextInput > bestOutput)
                    bestOutput = nextInput;
            }
            return bestOutput;

        }

        public override async Task<int> RunPart2Async()
        {
            var c = new Permutations<int>(new int[] { 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition);
            var bestOutput = 0;

            foreach (var phaseSettings in c)
            {
                var inputs = GetInputs(phaseSettings);
                var vms = GetVms(inputs);
                var result = 0;

                while (!vms[0].IsHalted)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var nextOutput = vms[i].RunProgramPauseAtOutput();
                        if (i == 4 && vms[0].IsHalted)
                            result = nextOutput;
                        else
                        {
                            var nextInputIdx = i + 1 == 5 ? 0 : i + 1;
                            inputs[nextInputIdx].Enqueue(nextOutput);
                        }
                    }
                }
                
                if (result > bestOutput)
                    bestOutput = result;
            }
            return bestOutput;
        }

        private IList<IntCodeVm> GetVms(IList<Queue<int>> inputs) 
            => Enumerable.Range(0, 5)
                    .Select(r => new IntCodeVm(Inputs[0], inputs[r]))
                    .ToList();

        private IList<Queue<int>> GetInputs(IList<int> phaseSettings)
        {
            var inputs = new List<Queue<int>>
                {
                    new Queue<int>(new int[] { phaseSettings[0], 0 })
                };
            inputs.AddRange(
                Enumerable.Range(1, 4)
                .Select(r => new Queue<int>(new int[] { phaseSettings[r] }))
                );

            return inputs;
        }
    }
}