namespace AdventOfCode2019.Base
{
    using System;

    internal enum Mode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2
    }

    internal class StateMachine
    {
        private readonly long[] _tape;
        
        public StateMachine(long[] tape) => _tape = tape;
        
        public int Pointer { get; private set; }

        private int _relativePointerBase = 0;
        
        public void SetPointer(long pointer) => Pointer = (int)pointer;

        public void ModifyRelativeBasePoint(long value) => _relativePointerBase += ((int)value);
        
        public long GetValue() => _tape[Pointer];
        
        public long GetValue(int pointer) => _tape[pointer];
        
        public long GetValueOffset(int offset, Mode mode)
        {
            return mode switch
            {
                Mode.Position => _tape[_tape[Pointer + offset]],
                Mode.Immediate => _tape[Pointer + offset],
                Mode.Relative => _tape[_relativePointerBase + _tape[Pointer + offset]],
                _ => throw new NotSupportedException()
            };
        }

        public void SetValue(int pointer, long value) => _tape[pointer] = value;
        
        public void SetValueOffset(int offset, Mode[] modes, long value)
        {
            var idx = modes[offset - 1] switch
            {
                Mode.Position => _tape[Pointer + offset],
                Mode.Immediate => Pointer + offset,
                Mode.Relative => _relativePointerBase + _tape[Pointer + offset],
                _ => throw new NotSupportedException()
            };
            
            _tape[idx] = value;
        }
    }
}