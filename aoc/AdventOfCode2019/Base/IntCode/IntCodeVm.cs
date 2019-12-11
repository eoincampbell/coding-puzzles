namespace AdventOfCode2019.Base.IntCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;

    public class IntCodeVm
    {
        private readonly StateMachine _sm;
        private readonly Queue<BigInteger> _in = new Queue<BigInteger>();
        private readonly Queue<BigInteger> _out = new Queue<BigInteger>();
       
        private BigInteger _lastOutput;
        public Action<string> LogAction;
        public bool IsHalted { get; private set; }
        public bool IsPaused { get; private set; }

        public IntCodeVm(string memory) : this(Array.ConvertAll(memory.Split(','), BigInteger.Parse)) { }

        public IntCodeVm(IEnumerable<BigInteger> memory)
        {
            var mem = memory
                .Select((value, index) => new { value, index })
                .ToDictionary(pair => new BigInteger(pair.index), pair => pair.value);

            _sm = new StateMachine(mem);
        }

        #region Program Control Flow

        public bool RunProgram()
        {
            while (!IsHalted) ProcessNextCommand();

            return IsHalted;
        }

        public bool RunProgramPauseAtOutput()
        {
            IsPaused = false;
            while (!IsPaused && !IsHalted) ProcessNextCommand();

            return IsHalted;
        }

        private void ProcessNextCommand()
        {
            var oc = _sm.GetValue() % 100;
            var com = Commands.Get(oc);
            com.Execute(this, _sm);
            LogAction?.Invoke($"{oc:00} {_sm.GetPointer():0000} {com.CommandName}");            
        }

        public void Halt() => IsHalted = true;
        public void Pause() => IsPaused = true;

        #endregion

        #region IO
        
        //Values
        public BigInteger GetValue(BigInteger ptr) => _sm.GetValue(ptr);
        public void SetValue(BigInteger ptr, BigInteger val) => _sm.SetValue(ptr, val);

        //Inputs
        public BigInteger GetInput() => _in.Dequeue();
        public void SetInput(BigInteger input) => _in.Enqueue(input);
        
        //Output
        public BigInteger GetOutput()
        {
            if (_out.TryDequeue(out var temp))
            {
                _lastOutput = temp;
                return _lastOutput;
            }
            else
            {
                Debug.WriteLine("Possible Bork! Returning a previous output");
                return _lastOutput;
            }
            
        }
        public IEnumerable<BigInteger> GetOutputs()
        {
            while (_out.Any())
            {
                _lastOutput = _out.Dequeue();
                yield return _lastOutput;
            }
        }
        public void SetOutput(BigInteger output) => _out.Enqueue(output);


        #endregion
    }
}