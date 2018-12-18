namespace AdventOfCode2018.Puzzles.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using System.Threading.Tasks;
 
    public class Impl : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day16\\Input.txt";
        //public const string FILE = ".\\Puzzles\\Day16\\InputSimple.txt";

        public Impl() : base("Day16 ", FILE) { }


        private static Func<int[], int, int, int, int[]> addr = (r, a, b, c) => { r[c] = r[a] + r[b]; return r; };
        private static Func<int[], int, int, int, int[]> addi = (r, a, b, c) => { r[c] = r[a] + b; return r; };
        private static Func<int[], int, int, int, int[]> mulr = (r, a, b, c) => { r[c] = r[a] * r[b]; return r; };
        private static Func<int[], int, int, int, int[]> muli = (r, a, b, c) => { r[c] = r[a] * b; return r; };
        private static Func<int[], int, int, int, int[]> banr = (r, a, b, c) => { r[c] = r[a] & r[b]; return r; };
        private static Func<int[], int, int, int, int[]> bani = (r, a, b, c) => { r[c] = r[a] & b; return r; };
        private static Func<int[], int, int, int, int[]> borr = (r, a, b, c) => { r[c] = r[a] | r[b]; return r; };
        private static Func<int[], int, int, int, int[]> bori = (r, a, b, c) => { r[c] = r[a] | b; return r; };
        private static Func<int[], int, int, int, int[]> setr = (r, a, b, c) => { r[c] = r[a]; return r; };
        private static Func<int[], int, int, int, int[]> seti = (r, a, b, c) => { r[c] = a; return r; };
        private static Func<int[], int, int, int, int[]> gtir = (r, a, b, c) => { r[c] = a > r[b] ? 1 : 0; return r; };
        private static Func<int[], int, int, int, int[]> gtri = (r, a, b, c) => { r[c] = r[a] > b ? 1 : 0; return r; };
        private static Func<int[], int, int, int, int[]> gtrr = (r, a, b, c) => { r[c] = r[a] > r[b] ? 1 : 0; return r; };
        private static Func<int[], int, int, int, int[]> eqir = (r, a, b, c) => { r[c] = a == r[b] ? 1 : 0; return r; };
        private static Func<int[], int, int, int, int[]> eqri = (r, a, b, c) => { r[c] = r[a] == b ? 1 : 0; return r; };
        private static Func<int[], int, int, int, int[]> eqrr = (r, a, b, c) => { r[c] = r[a] == r[b] ? 1 : 0; return r; };

        private static List<Func<int[], int, int, int, int[]>> opcodes = new List<Func<int[], int, int, int, int[]>>
        {
            addr,addi,
            mulr,muli,
            banr,bani,
            borr,bori,
            setr,seti,
            gtir,gtri,gtrr,
            eqir,eqri,eqrr
        };

        public override async Task<string> RunPart1()
        {
            var (processedInputs, remainingInstructions) = GetInputTests(Inputs);
            var dict = new Dictionary<int, Func<int[], int, int, int, int[]>>();
            var count = processedInputs.Count(i => CheckRules(i, dict) >= 3);
            return await Task.FromResult($"{count}");
        }

        public override async Task<string> RunPart2()
        {
            var (processedInputs, remainingInstructions) = GetInputTests(Inputs);
            var dict = new Dictionary<int, Func<int[], int, int, int, int[]>>();
            var registerState = new [] { 0, 0, 0, 0 };

            foreach (var i in processedInputs) CheckRules(i, dict, true);
            
            foreach (var ri in remainingInstructions)
            {
                var op = dict[ri[0]];
                registerState = op(registerState, ri[1], ri[2], ri[3]);
            }

            return await Task.FromResult($"{registerState[0]}");
        }

        private static int CheckRules((int[] before, int[] inst, int[] after) instructions, IDictionary<int, Func<int[], int, int, int, int[]>> dict, bool track =false)
        {
            int count = 0, candidateId = 0;
            var b = new int[4];
            Func<int[], int, int, int, int[]> candidate = null;
            
            foreach (var op in opcodes)
            {
                Array.Copy(instructions.before, b, 4);
                var result = op(b, instructions.inst[1], instructions.inst[2], instructions.inst[3]);
                if (!result.SequenceEqual(instructions.after)) continue;

                candidate = op;
                candidateId = instructions.inst[0];
                count++;
            }

            if (count != 1 || dict.ContainsKey(candidateId) || !track) return count;

            dict.Add(candidateId, candidate);
            opcodes.Remove(candidate);
            return count;

        }

        private static (List<(int[], int[], int[])>, List<int[]>) GetInputTests(string [] inputs)
        {
            var processedInputs = new List<(int[] before, int[] inst, int[] after)>();
            var remainingInstructions = new List<int[]>();
            var i = 0;

            while(true)
            {
                var before = inputs[i];
                if (before.Contains("Before:"))
                {
                    var instructions = inputs[i + 1];
                    var after = inputs[i + 2];
                    var beforeArr = before.Replace("Before: [", "").Replace("]", "").Split(',').Select(int.Parse).ToArray();
                    var instructionsArr = instructions.Split(' ').Select(int.Parse).ToArray();
                    var afterArr = after.Replace("After:  [", "").Replace("]", "").Split(',').Select(int.Parse).ToArray();

                    processedInputs.Add ((beforeArr, instructionsArr, afterArr));

                    i += 4;
                }
                else break;
            }

            i += 2;
            for (; i < inputs.Length; i++)
            {
                remainingInstructions.Add(inputs[i].Split(' ').Select(int.Parse).ToArray());
            }

            return (processedInputs, remainingInstructions);
        }
    }
}