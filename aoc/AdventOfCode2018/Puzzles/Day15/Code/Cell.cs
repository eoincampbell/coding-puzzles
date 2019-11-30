namespace AdventOfCode2018.Puzzles.Day15.Code
{
    public abstract class Square
    {
        public abstract char Type { get; }
        protected abstract int GetWeight();
        public Player Occupant { get; set; }
        public int Weight => GetWeight();

        public bool IsFree => (Occupant == null && Type == '.');
    }

    public class Wall : Square
    {
        public override char Type => '#';
        protected override int GetWeight() => 999;
    }

    public class Floor : Square
    {
        public override char Type => '.';
        protected override int GetWeight() => (Occupant != null) ? 999 : 1;
    }
}