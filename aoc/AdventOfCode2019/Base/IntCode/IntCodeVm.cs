namespace AdventOfCode2019.Base.IntCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public enum VmState
    {
        Running,
        Halted,
        Paused,
        PausedAwaitingInput
    }

    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<BigInteger> _in = new Queue<BigInteger>();
        private readonly Queue<BigInteger> _out = new Queue<BigInteger>();
        public Action<string> LogAction { get; set; }
        private BigInteger _lastOutput;
        public VmState State {get; private set; }
        //public bool IsHalted { get; private set; }
        //public bool IsPaused { get; private set; }

        public IntCodeVm(string memory) : this(Array.ConvertAll(memory?.Split(','), BigInteger.Parse)) { }

        public IntCodeVm(IEnumerable<BigInteger> memory)
        {
            var mem = memory
                .Select((value, index) => new { value, index })
                .ToDictionary(k => new BigInteger(k.index), v => v.value);

            _sm = new StateMachine(mem);
        }

        #region Program Control Flow

        public VmState RunProgram()
        {
            State = VmState.Running;
            while (State != VmState.Halted) ProcessNextCommand();

            return State;
        }

        public VmState RunProgramPauseAtOutput()
        {
            State = VmState.Running;
            while (State != VmState.Halted && State != VmState.Paused) ProcessNextCommand();

            return State;
        }

        private void ProcessNextCommand()
        {
            var oc = _sm.GetValue() % 100;
            var com = Commands.Get(oc);
            com.Execute(this, _sm);
            LogAction?.Invoke($"{oc:00} {_sm.GetPointer():0000} {com.CommandName}");            
        }

        public void Halt() => State = VmState.Halted;
        public void Pause() => State = VmState.Paused;
        public void PauseAwaitingInput() => State = VmState.PausedAwaitingInput;

        #endregion

        #region IO
        
        //Values
        public BigInteger GetValue(BigInteger p) => _sm.GetValue(p);
        public void SetValue(BigInteger p, BigInteger val) => _sm.SetValue(p, val);

        //Inputs
        public BigInteger GetInput(out bool success)
        {
            success = _in.TryDequeue(out var temp);
            return success ? temp : 0;
        }

        public void SetInput(BigInteger input) => _in.Enqueue(input);
        
        //Output
        public BigInteger GetOutput() => _out.TryDequeue(out var temp) ? _lastOutput = temp : _lastOutput;
        
        public IEnumerable<BigInteger> GetOutputs()
        {
            while (_out.Any())
                yield return (_lastOutput = _out.Dequeue());
        }
        public void SetOutput(BigInteger output) => _out.Enqueue(output);


        #endregion
    }
}