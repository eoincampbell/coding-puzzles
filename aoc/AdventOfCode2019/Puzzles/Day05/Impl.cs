namespace AdventOfCode2019.Puzzles.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 05: ", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            var vm = new IntCodeVM(Inputs[0]);

            var results = vm.RunProgram(1).ToList();

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }

            return results.Last(); //16225258
        }

        public override async Task<int> RunPart2Async()
        { 
            var vm = new IntCodeVM(Inputs[0]);

            var results = vm.RunProgram(5).ToList();

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }

            return results.Last(); //2808771
        }

        
    }

    public class IntCodeVM
    {
        //public static Dictionary<int, Command> _commands = new Dictionary<int, Command>()
        //{
        //    {1, new Command {Name = "Add", OpCode = 1, Advance = 4}},
        //    {2, new Command {Name = "Mul", OpCode = 2, Advance = 4}},
        //    {3, new Command {Name = "Inp", OpCode = 3, Advance = 2}},
        //    {4, new Command {Name = "Out", OpCode = 4, Advance = 2, Output = true}},
        //    {99, new Command {Name = "Halt", OpCode = 99, Advance = 1, Halt = true}}
        //};

        public int[] _tape;
        public int _pointer;

        public IntCodeVM(string tape)
        {
            _tape = Array.ConvertAll(tape.Split(','), int.Parse);
            _pointer = 0;
        }

        public IEnumerable<int> RunProgram(int input)
        {
            var halt = false;

            while (!halt)
            {
                var result = ProcessCommand(_tape, input, ref _pointer, out halt);
                foreach (var v in result)
                {
                    yield return v;
                }

            }

            
        }

        private int GetValue(int[] tape, int pointer, int offset, int mode)
            => (mode == 0) ? _tape[_tape[pointer + offset]] : _tape[pointer + offset];

        private void SetValue(int[] tape, int pointer, int offset, int mode, int value)
        {
            if (mode == 0)
                _tape[_tape[pointer + offset]] = value;
            else
                _tape[pointer + offset] = value;
        }
        
        private int[] ProcessCommand(int[] tape, int input, ref int pointer, out bool halt)
        {
            var parameterisedOpCode = _tape[pointer];
            var opCode = parameterisedOpCode % 100;
            var paramMode = parameterisedOpCode / 100;

            var p1mode = paramMode % 10;
            var p2mode = paramMode / 10 % 10;
            var p3mode = paramMode / 100 % 10;

            int p1, p2;

            halt = false;
            var outputs = new List<int>();

            switch (opCode)
            {
                case 1:
                    p1 = GetValue(_tape, pointer, 1, p1mode);// p1mode == 0 ? _tape[_tape[pointer + 1]] : _tape[pointer + 1];
                    p2 = GetValue(_tape, pointer, 2, p2mode); //p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];
                    SetValue(_tape, pointer, 3, p3mode, p1 + p2);

                    pointer += 4;
                    break;
                case 2:
                    var mul1 = GetValue(_tape, pointer, 1, p1mode);
                    var mul2 = p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];
                    
                    if (p3mode == 0) 
                        _tape[_tape[pointer + 3]] = mul1 * mul2;
                    else
                        _tape[pointer + 3] = mul1 * mul2;

                    pointer += 4;
                    break;
                case 3:
                    _tape[_tape[pointer + 1]] = input;
                    pointer += 2;
                    break;
                case 4:
                    if (p1mode == 0)
                        outputs.Add(_tape[_tape[pointer + 1]]);
                    else 
                        outputs.Add(_tape[pointer + 1]);

                    pointer += 2;
                    break;
                case 5:
                    var jitTest = GetValue(_tape, pointer, 1, p1mode);
                    var jitAddr = p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];

                    if (jitTest != 0)
                        pointer = jitAddr;
                    else
                        pointer += 3;
                    break;
                case 6:
                    var jifTest = GetValue(_tape, pointer, 1, p1mode);
                    var jifAddr = p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];
                    
                    if(jifTest == 0)
                        pointer = jifAddr;
                    else
                        pointer += 3;

                    break;
                case 7:
                    var ltP1 = GetValue(_tape, pointer, 1, p1mode);
                    var ltP2 = p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];

                    var ltRes = (ltP1 < ltP2) ? 1 : 0;
                    
                    if (p3mode == 0) 
                        _tape[_tape[pointer + 3]] = ltRes ;
                    else
                        _tape[pointer + 3] = ltRes ;

                    pointer += 4;
                    break;
                case 8:
                    var eqP1 = GetValue(_tape, pointer, 1, p1mode);
                    var eqP2 = p2mode == 0 ? _tape[_tape[pointer + 2]] : _tape[pointer + 2];

                    var eqRes = (eqP1 == eqP2) ? 1 : 0;
                    
                    if (p3mode == 0) 
                        _tape[_tape[pointer + 3]] = eqRes;
                    else
                        _tape[pointer + 3] = eqRes;

                    pointer += 4;
                    break;
                case 99:
                    halt = true;
                    break;
            }

            return outputs.ToArray();
        }

    }

    //public class CommandResult {}

    //public class Command
    //{
    //    public string Name {get;set;}
    //    public int OpCode { get; set; }
    //    public int Advance { get; set; }
    //    public bool Halt { get; set; }
    //    public bool Output { get; set; }
    //}
}
