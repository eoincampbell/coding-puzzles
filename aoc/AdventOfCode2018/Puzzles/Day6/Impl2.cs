namespace AdventOfCode2018.Puzzles.Day6
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MoreLinq;

    public class Impl2 : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day6\\InputVas.txt";
        public const int P2THRESHOLD = 10000; //switch this to 32 to simple input

        public Impl2() : base("Day 6b", FILE) { }

        public override async Task<string> RunPart1()
        {
            var (points, minx, miny, width, height) = GetCoordinates(Inputs);
            var dict = points.ToDictionary(k => k.Id, v => v);
            for (var x = minx; x <= width; x++)
                for (var y = miny; y <= height; y++)
                {
                    int best = int.MaxValue, bestId = -1, numBest = 0;
                    foreach (var p in points.Select(s => new {Dist = GetDist(s, x, y), Point = s.Id}))
                    {
                        if (p.Dist < best)                  //keep improving the closest point
                        {
                            best = p.Dist;                  //new best
                            bestId = p.Point;               //grab the id and reset the trackers
                            numBest = 1;
                        }
                        else if (best == p.Dist) numBest++; //if we've more than 1 at the shortest distance
                    }

                    if (numBest > 1) continue;

                    if (!dict[bestId].TouchesInfinity
                            && IsInfinite(x, y, minx, miny, width, height)) dict[bestId].TouchesInfinity = true;
                        dict[bestId].Count++;
                }
            
            var r = points
                .Where(p => !p.TouchesInfinity)
                .Max(m => m.Count);
            return await Task.FromResult($"{r}");
        }

        public override async Task<string> RunPart2()
        {
            var (points, minx, miny, width, height) = GetCoordinates(Inputs);
            var count = 0;
            for (var x = minx; x < width; x++)
                for (var y = miny; y < height; y++)
                    if (points.Sum(s => GetDist(s, x, y)) < P2THRESHOLD)
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
            public bool TouchesInfinity;
            public override string ToString()
            {
                return $"{Id}: {Count}";
            }
        }
    }
}