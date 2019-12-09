namespace AdventOfCode2019.Base
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Memory = System.Collections.Generic.Dictionary<System.Numerics.BigInteger, System.Numerics.BigInteger>;

    internal enum Mode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2
    }

    internal class StateMachine
    {
        private readonly Memory _memory;
        
        public StateMachine(Memory tape) => _memory = tape;
        
        public BigInteger Pointer { get; private set; }

        private BigInteger _relativePointerBase = 0;
        
        public void SetPointer(BigInteger pointer) => Pointer = pointer;

        public void ModifyRelativeBasePoint(BigInteger value) => _relativePointerBase += value;

        public BigInteger GetValue()
        {
            if(!_memory.ContainsKey(Pointer))
                _memory.Add(Pointer, 0);

            return _memory[Pointer];
        }

        public BigInteger GetValue(BigInteger pointer)
        {
            if(!_memory.ContainsKey(pointer))
                _memory.Add(pointer, 0);

            return _memory[pointer];
        }

        public BigInteger GetValueOffset(int offset, Mode mode)
        {
            return mode switch
            {
                Mode.Position => GetValue(GetValue(Pointer + offset)),
                Mode.Immediate => GetValue(Pointer + offset),
                Mode.Relative => GetValue(_relativePointerBase + GetValue(Pointer + offset)),
                _ => throw new NotSupportedException()
            };
        }

        public void SetValue(BigInteger pointer, BigInteger value) => _memory[pointer] = value;
        
        public void SetValueOffset(int offset, Mode[] modes, BigInteger value)
        {
            var idx = modes[offset - 1] switch
            {
                Mode.Position => GetValue(Pointer + offset),
                Mode.Immediate => Pointer + offset,
                Mode.Relative => _relativePointerBase + GetValue(Pointer + offset),
                _ => throw new NotSupportedException()
            };

            if (!_memory.ContainsKey(idx)) 
                _memory.Add(idx, 0);

            _memory[idx] = value;
        }
    }
}