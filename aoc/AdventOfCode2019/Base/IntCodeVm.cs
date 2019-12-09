namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    using Memory = System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>;

    using CommandDict = System.Collections.Generic.Dictionary<Commands, ICommand>;
    
    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<BigInteger> _in = new Queue<BigInteger>();
        private readonly Queue<BigInteger> _out = new Queue<BigInteger>();
        private readonly CommandDict _commands;
        private BigInteger _lastOutput;
        public Action<string> LogAction;
        public bool IsHalted { get; private set; }
        public bool IsPaused { get; private set; }

        public IntCodeVm(string memory) : this(Array.ConvertAll(memory.Split(','), BigInteger.Parse)) { }
        public IntCodeVm(BigInteger [] memory)
        {
            var mem = memory
                .Select((value, index) => new { value, index })
                .ToDictionary(pair => new BigInteger(pair.index), pair => pair.value);

            
            _sm = new StateMachine(mem);
            _commands = new CommandDict
            {
                {Commands.ADD, new AddCommand(this, _sm)},
                {Commands.MUL, new MultiplyCommand(this, _sm)},
                {Commands.INP, new InputCommand(this, _sm)},
                {Commands.OUT, new OutputCommand(this, _sm)},
                {Commands.JIT, new JumpIfTrueCommand(this, _sm)},
                {Commands.JIF, new JumpIfFalseCommand(this, _sm)},
                {Commands.LES, new LessThanCommand(this, _sm)},
                {Commands.EQU, new EqualToCommand(this, _sm)},
                {Commands.REL, new RelativeBaseCommand(this, _sm)},
                {Commands.HLT, new HaltCommand(this, _sm)},
            };
        }

        public void RunProgram()
        {
            while (!IsHalted) ProcessNextCommand();
        }

        public void RunProgramPauseAtOutput()
        {
            IsPaused = false;
            while (!IsPaused && !IsHalted) ProcessNextCommand();
        }

        private void ProcessNextCommand()
        {
            var oc = (int) (_sm.GetValue() % 100);
            var com = _commands[(Commands)oc];
            com.Execute();
            LogAction?.Invoke($"{oc:00} {_sm.Pointer:0000} {com.CommandName}");            
        }

        public void Halt() => IsHalted = true;
        public void Pause() => IsPaused = true;
        public void SetValue(BigInteger ptr, BigInteger val) => _sm.SetValue(ptr, val);
        public void AddInput(BigInteger input) => _in.Enqueue(input);
        public void AddOutput(BigInteger output) => _out.Enqueue(output);
        public BigInteger GetValue(BigInteger ptr) => _sm.GetValue(ptr);
        public BigInteger GetNextInput() => _in.Dequeue();
        public BigInteger GetNextOutput()
        {
            if (_out.TryDequeue(out BigInteger temp)) _lastOutput = temp;
            return _lastOutput;
        }
        public IEnumerable<BigInteger> GetOutputs()
        {
            while (_out.Any())
            {
                _lastOutput = _out.Dequeue();
                yield return _lastOutput;
            }
        }
    }
}