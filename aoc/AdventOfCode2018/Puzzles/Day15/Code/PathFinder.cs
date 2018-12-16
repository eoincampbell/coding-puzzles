namespace AdventOfCode2018.Puzzles.Day15.Code
{
    using System.Collections.Generic;
    using System.Linq;

    public static class PathFinder
    {
        public static (int, Point) GetDistance(this Square[,] map, Player current, Player other)
        {
            var source = current.Coordinate;
            var dest = other.Coordinate;

            int height = map.GetLength(1), width = map.GetLength(0);
            bool[,] tracker = new bool[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (map[x, y].Weight > 1)
                    {
                        var t = new Point { X = x, Y = y };
                        if (t != source && t != dest)
                            tracker[x, y] = true;
                    }

            var start = new Location() { Coordinate = source, Distance = 0 };

            var queue = new Queue<Location>();
            queue.Enqueue(start);

            tracker[start.Coordinate.X, start.Coordinate.Y] = true;

            while (queue.Any())
            {
                var p = queue.Dequeue();

                if (p.Coordinate == dest) return (p.Distance, p.FirstStep ?? source);

                if (p.Coordinate.Y - 1 >= 0 && tracker[p.Coordinate.X, p.Coordinate.Y - 1] == false)
                {
                    var np = new Point(p.Coordinate.X, p.Coordinate.Y - 1);
                    var nl = new Location(np, p.FirstStep == null ? np : p.FirstStep, p.Distance + 1);
                    queue.Enqueue(nl);
                    tracker[np.X, np.Y] = true;
                }
                if (p.Coordinate.X - 1 >= 0 && tracker[p.Coordinate.X - 1, p.Coordinate.Y] == false)
                {
                    var np = new Point(p.Coordinate.X - 1, p.Coordinate.Y);
                    var nl = new Location(np, p.FirstStep == null ? np : p.FirstStep, p.Distance + 1);
                    queue.Enqueue(nl);
                    tracker[np.X, np.Y] = true;
                }
                if (p.Coordinate.X + 1 >= 0 && tracker[p.Coordinate.X + 1, p.Coordinate.Y] == false)
                {
                    var np = new Point(p.Coordinate.X + 1, p.Coordinate.Y);
                    var nl = new Location(np, p.FirstStep == null ? np : p.FirstStep, p.Distance + 1);
                    queue.Enqueue(nl);
                    tracker[np.X, np.Y] = true;
                }
                if (p.Coordinate.Y + 1 < height && tracker[p.Coordinate.X, p.Coordinate.Y + 1] == false)
                {
                    var np = new Point(p.Coordinate.X, p.Coordinate.Y +1);
                    var nl = new Location(np, p.FirstStep == null ? np : p.FirstStep, p.Distance + 1);
                    queue.Enqueue(nl);
                    tracker[np.X, np.Y] = true;
                }
            }
            return (int.MaxValue, source);
        }
    } 
}