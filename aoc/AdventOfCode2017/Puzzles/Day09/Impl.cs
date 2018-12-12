namespace AdventOfCode2017.Puzzles.Day9
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 09 ", ".\\Puzzles\\Day09\\Input.txt") { }
        private int _part2;
        public override async Task<string> RunPart1()
        {
            int last = 0;
            foreach (var a in Inputs)
            {
                char[] stream = a.ToCharArray();

                bool isG = false;
                int lvl = 1, s = 0, gCount = 0;
                for (int i = 0; i < stream.Length; i++)
                {
                    char c = stream[i];                         //get the next char in the stream
                    if (c == '!') i++;                          //! means skip the next 
                    else if (c != '>' && isG) gCount++;         //if it's not closing garbage, and we're in garbage, bump the counter
                    else if (c == '<') isG = true;              //we're in garbage
                    else if (c == '>') isG = false;             //we're out of garbage
                    else if (c == '{' && !isG) s += (lvl++);    //we're in a new group so add the level to our score and bump it by one
                    else if (c == '}' && !isG) lvl--;           //we're leaving a group so come back down a level
                }
                last = s;
                _part2 = gCount;
                Console.WriteLine($"{s}");
            }

            return await Task.FromResult($"{last}");
        }
        
        public override async Task<string> RunPart2()
        {
            return await Task.FromResult($"{_part2}");
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