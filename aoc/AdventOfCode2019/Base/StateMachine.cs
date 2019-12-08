namespace AdventOfCode2019.Base
{
    internal enum Mode
    {
        Position = 0,
        Immediate = 1
    }

    internal class StateMachine
    {
        private readonly int[] _tape;
        public StateMachine(int[] tape) => _tape = tape;
        public int Pointer { get; private set; }
        public void SetPointer(int pointer) => Pointer = pointer;
        public int GetValue() => _tape[Pointer];
        public int GetValue(int pointer) => _tape[pointer];
        public int GetValueOffset(int offset, Mode mode) => (mode == Mode.Position)
                ? _tape[_tape[Pointer + offset]]
                : _tape[Pointer + offset]; 
        public void SetValue(int pointer, int value) => _tape[pointer] = value;
        public void SetValueOffset(int offset, Mode[] modes, int value)
        {
            var idx = (modes[offset - 1] == 0)
                ? _tape[Pointer + offset]
                : Pointer + offset;
            _tape[idx] = value;
        }
    }
}