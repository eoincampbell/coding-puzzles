namespace AdventOfCode2018.Puzzles.Day6
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MoreLinq;

    public class Impl3 : BasePuzzle
    {
        public Impl3() : base("Day 6b", ".\\Puzzles\\Day6\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var (points,minx, miny, width, height) = GetCoordinates(Inputs);
            var dict = points.ToDictionary(k => k.Id, v => v);
            
            for (var x = minx; x < width; x++)
                for (var y = miny; y < height; y++)
                {
                    int closest = -1;

                    var shortestDist = points
                        .Select(s => new { PointId = s.Id, Distance = GetDist(s, x, y) })
                        .GroupBy(g => g.Distance)
                        .Select(ss => new { Distance = ss.Key, Points = ss})
                        .OrderBy(o => o.Distance)
                        .FirstOrDefault();

                    if (shortestDist.Points.Count() > 1) continue;
                    closest = shortestDist.Points.First().PointId;
                    dict[closest].Count++;
                }
            var r = points.Max(m => m.Count);
            return await Task.FromResult($"{r}");
        }

        public override async Task<string> RunPart2()
        {
            var (points, minx, miny, width, height) = GetCoordinates(Inputs);
            var count = 0;
            for (var x = minx; x < width; x++)
                for (var y = miny; y < height; y++)
                    if (points.Sum(s => GetDist(s, x, y)) < 10000)
                        count++;
            
            return await Task.FromResult($"{count}");
        }

        private int GetDist(Point a, int x, int y) => 
            Math.Abs(a.X - x) + Math.Abs(a.Y - y);

        //if (!p.TouchesInfinity && IsInfinite(p.X, p.Y, minx, miny, width, height)) { p.TouchesInfinity = true; }
        private bool IsInfinite(int x, int y, int minx, int miny, int width, int height) =>
            (x == minx || y == miny || x == width || x == height );

        private (Point[],int,int,int,int) GetCoordinates(IEnumerable<string> inputs)
        {
            var id = 0;
            var coords = inputs
                .Select(i => i.Split(','))
                .Select(s => new Point { 
                    Id = id++,
                    X = int.Parse(s[0]),
                    Y = int.Parse(s[1])
                }).ToArray();
            
            return (coords, 
                coords.Min(m => m.X), coords.Min(m => m.Y),
                coords.Max(m => m.X), coords.Max(m => m.Y));
        }

        private class Point
        {
            public int Id;
            public int X;
            public int Y;
            public int Count;
            //public bool TouchesInfinity;
            public override string ToString()
            {
                return $"{Id}: {Count}";
            }
        }
    }
}