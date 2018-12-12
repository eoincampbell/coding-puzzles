namespace AdventOfCode2017.Puzzles.Day8
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 08 ", ".\\Puzzles\\Day08\\Input.txt") { }
        private int _part2;
        public override async Task<string> RunPart1()
        {
            var (instructions, dict) = ProcessInputs(Inputs);

            var maxSize = 0;
            foreach (var inst in instructions)
            {
                var currentValue = dict[inst.Reg];
                var diff = (inst.PosNeg * inst.Value);

                switch (inst.CType)
                {
                    case "<":
                        if (dict[inst.CReg] < inst.CValue) currentValue += diff;
                        break;
                    case "<=":
                        if (dict[inst.CReg] <= inst.CValue) currentValue += diff;
                        break;
                    case "==":
                        if (dict[inst.CReg] == inst.CValue) currentValue += diff;
                        break;
                    case ">=":
                        if (dict[inst.CReg] >= inst.CValue) currentValue += diff;
                        break;
                    case ">":
                        if (dict[inst.CReg] > inst.CValue) currentValue += diff;
                        break;
                    case "!=":
                        if (dict[inst.CReg] != inst.CValue) currentValue += diff;
                        break;

                }
                var newMax = dict.Values.Max();
                if (newMax > _part2) _part2 = newMax;

                dict[inst.Reg] = currentValue; 
            }
            var answer = dict.Values.Max();
            return await Task.FromResult($"{answer}");
        }

        

        public override async Task<string> RunPart2()
        {

            return await Task.FromResult($"{_part2}");
        }

        public (List<Instruction>, Dictionary<string, int>) ProcessInputs(IEnumerable<string> inputs)
        {
            var r = new Regex(@"^(\w+) (inc|dec) (-?\d+) if (\w+) ([\<\>\=\!]{1,2}) (-?\d+)");
            var l = new List<Instruction>();
            var d = new Dictionary<string, int>();

            foreach(var v in inputs)
            {
                var mg = r.Match(v).Groups;

                var instruction = new Instruction
                {
                    Reg = mg[1].Value,
                    PosNeg = mg[2].Value == "inc" ? 1 : -1,
                    Value = int.Parse(mg[3].Value),
                    CReg= mg[4].Value,
                    CType = mg[5].Value,
                    CValue = int.Parse(mg[6].Value)
                };
                l.Add(instruction);
                d[instruction.Reg] = 0;
                d[instruction.CReg] = 0;
            }

            return (l, d);
        }

        public class Instruction
        {
            public string Reg;
            public int PosNeg;
            public int Value;
            public string CReg;
            public string CType;
            public int CValue;
        }
    }
}