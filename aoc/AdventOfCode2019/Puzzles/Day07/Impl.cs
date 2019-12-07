/*
 * Day 07: Amplification Circuit
 * https://adventofcode.com/2019/day/7
 * Part 1: 22012
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day07
{
    using System;
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
            //return 0;
            var c = new Permutations<int>(new int[] { 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition);
            var bestOutput = 0;

            foreach (var phaseSettings in c)
            {
                var aInputs = new Queue<int>(new int[] { phaseSettings[0], 0 });
                var bInputs = new Queue<int>(new int[] { phaseSettings[1]  });
                var cInputs = new Queue<int>(new int[] { phaseSettings[2]  });
                var dInputs = new Queue<int>(new int[] { phaseSettings[3]  });
                var eInputs = new Queue<int>(new int[] { phaseSettings[4]  });


                var aVm = new IntCodeVm(Inputs[0], aInputs);
                var bVm = new IntCodeVm(Inputs[0], bInputs);
                var cVm = new IntCodeVm(Inputs[0], cInputs);
                var dVm = new IntCodeVm(Inputs[0], dInputs);
                var eVm = new IntCodeVm(Inputs[0], eInputs);

                var result = 0;

                while (!aVm.IsHalted)
                {
                    var aOuput = aVm.RunProgramPauseAtOutput();
                    bInputs.Enqueue(aOuput);
                    var bOuput = bVm.RunProgramPauseAtOutput();
                    cInputs.Enqueue(bOuput);
                    var cOuput = cVm.RunProgramPauseAtOutput();
                    dInputs.Enqueue(cOuput);
                    var dOuput = dVm.RunProgramPauseAtOutput();
                    eInputs.Enqueue(dOuput);
                    var eOuput = eVm.RunProgramPauseAtOutput();

                    if (aVm.IsHalted) result = eOuput;
                    else aInputs.Enqueue(eOuput);
                    
                }
                
                if (result > bestOutput)
                    bestOutput = result;
            }
            return bestOutput;
        }
    }
}