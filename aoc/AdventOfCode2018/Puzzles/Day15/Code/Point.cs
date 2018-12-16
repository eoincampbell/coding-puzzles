namespace AdventOfCode2018.Puzzles.Day15.Code
{
    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x; Y = y;
        }

        public override string ToString() => $"{{{X}, {Y}}}";
        public override bool Equals(object obj) => (obj is Point && ((Point)obj).X == X && ((Point)obj).Y == Y);
        public static bool operator ==(Point a, Point b) => a.Equals(b);
        public static bool operator !=(Point a, Point b) => !a.Equals(b);


        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode *= -1521134295 + base.GetHashCode();
            hashCode *= -1521134295 + X.GetHashCode();
            hashCode *= -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }

    public struct Location
    {
        public Location(Point coord, int dist) : this(coord, null, dist) { }
        public Location(Point coord, Point? firstStep, int dist)
        {
            Coordinate = coord;
            Distance = dist;
            FirstStep = firstStep;
        }

        public Point Coordinate;
        public Point? FirstStep;
        public int Distance;
    }
}