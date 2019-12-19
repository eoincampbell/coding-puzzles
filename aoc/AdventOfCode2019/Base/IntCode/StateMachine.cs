namespace AdventOfCode2019.Base.IntCode
{
    using System;
    using System.Numerics;
    using Memory = System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>;

    internal class StateMachine
    {
        private readonly Memory _memory;

        private BigInteger _relativeBasePointer = 0;
        private BigInteger _pointer = 0;

        public StateMachine(Memory memory) => _memory = memory;
        
        #region Pointer Stuff 
        public BigInteger GetPointer() => _pointer;

        public void SetPointer(BigInteger pointer) => _pointer = pointer;
        
        public BigInteger GetPointer(int offset, Mode mode) => mode switch
            {
                Mode.Position => GetValue(_pointer + offset),
                Mode.Immediate => _pointer + offset,
                Mode.Relative => _relativeBasePointer + GetValue(_pointer + offset),
                _ => throw new NotSupportedException()
            };

        public void SetRelativeBasePointer(BigInteger value) => _relativeBasePointer += value;

        #endregion

        #region Value Stuff

        public BigInteger GetValue(BigInteger pointer)
        {
            if (!_memory.TryGetValue(pointer, out var val)) _memory.Add(pointer, 0);
            return val;
        }

        public BigInteger GetValue() => GetValue(_pointer);

        public BigInteger GetValueOffset(int offset, Mode mode)  => GetValue(GetPointer(offset, mode));
        

        public void SetValue(BigInteger pointer, BigInteger value)
        {
            if (!_memory.ContainsKey(pointer)) _memory.Add(pointer, value);
            else _memory[pointer] = value;
        }

        public void SetValueOffset(int offset, Mode[] modes, BigInteger value) => SetValue(GetPointer(offset, modes[offset - 1]), value);

        #endregion
    }
}