namespace AdventOfCode2018.Puzzles.Day6
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using MoreLinq;

    public class Impl2 : BasePuzzle
    {
        public Impl2() : base("Day 6b", ".\\Puzzles\\Day6\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var (points, width,height) = GetCoordinates(Inputs);
            var dict = points.ToDictionary(k => k.Id, v => v);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    int xx = x, yy = y;
                    var mp = points.MinBy(mb => GetDist(mb, xx, yy));
                    var lp = mp.First();

                    if (mp.Count() != 1) continue;

                    var point = dict[lp.Id];
                    if (IsInf(x, y, width, height) || point.TouchesInfinity)
                    {
                        point.TouchesInfinity = true;
                        continue;
                    }
                    point.Count++;
                }
            }

            var result = points
                .OrderByDescending(o => o.Count)
                .First(p => !p.TouchesInfinity)
                .Count;

            return await Task.FromResult($"{result}");
        }

        public override async Task<string> RunPart2()
        {
            var (points, width,height) = GetCoordinates(Inputs);
            var count = 0;
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    if (points.Sum(s => GetDist(s, x, y)) < 10000)
                        count++;
            
            return await Task.FromResult($"{count}");
        }

        private static bool IsInf(int x, int y, int width, int height)
        {
            return (x == 0 || y == 0 || x == width - 1 || x == height - 1);
        }

        private static int GetDist(Point a, int x, int y)
        {
            return Math.Abs(a.X - x) + Math.Abs(a.Y - y);
        }
        
        private static (List<Point>,int,int) GetCoordinates(IEnumerable<string> inputs)
        {
            var id = 0;
            var coords = inputs
                .Select(i => i.Split(','))
                .Select(s => new Point
                {
                    Id = id++,
                    X = int.Parse(s[0]),
                    Y = int.Parse(s[1])
                }).ToList();
            
            return (coords, coords.Max(m => m.X), coords.Max(m => m.Y));
        }

        private class Point
        {
            public int Id;
            public int X;
            public int Y;
            public bool TouchesInfinity;
            public int Count;
        }
    }
}