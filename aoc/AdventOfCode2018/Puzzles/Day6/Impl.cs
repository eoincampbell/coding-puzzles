namespace AdventOfCode2018.Puzzles.Day6
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MoreLinq;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 6 ", ".\\Puzzles\\Day6\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var points = GetCoordinates(Inputs);
            var map = GetMap(points);

            var r1 = FillMap(map, points);
            return await Task.FromResult($"{r1.Item1} | {r1.Item2}");

            ////DrawMap(map);
        }

        private const string Ids = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        //For Debugging
        //private static void DrawMap(Dist[,] map)
        //{
        //    var width = map.GetLength(0);
        //    var height = map.GetLength(1);
        //    for (var x = 0; x < width; x++)
        //    {
        //        for (var y = 0; y < height; y++)
        //        {
        //            Console.Write($"{map[x, y].Id}");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        private static (char,int) FillMap(Dist[,] map, Dictionary<char, Point> points)
        {
            /*Naive initial impl. went points first, and then multiple iterations of the map 
             */
            var width = map.GetLength(0);
            var height = map.GetLength(1);
            
            foreach (var p in points.Values)
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var d = GetManhattanDistance(p, x, y);
                        var isInf = IsInfinite(x, y, width, height);

                        if ((map[x, y] == null) //unmapped
                            || (map[x, y].Id != p.Id && map[x, y].D > d)) //someone else further away
                        {
                            map[x, y] = new Dist {Id = p.Id, D = d, IsInfinite = isInf };
                        }
                        else if (map[x, y].Id != p.Id && map[x, y].D == d ) 
                        {
                            map[x, y] = new Dist { Id = '.', D = d, IsInfinite = isInf };
                        }
                    }
                }
            }

            //Flatten It
            var tmp = new Dist[width * height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    tmp[y * width + x] = map[x, y];

            //Group By Cell Id & coun't the ones that are not touching infinity
            var r = tmp.GroupBy(k => k.Id)
                .Where(w => !w.Any(d => d.IsInfinite))
                .Select(s => new
                {
                    Id = s.Key,
                    Count = s.Count()
                })
                .OrderByDescending(o => o.Count)
                .First();

            return (r.Id, r.Count);
        }


        private static (char, int) FillMap2(Dist[,] map, Dictionary<char, Point> points)
        {
            /*then I started writing this, to go X -> Y -> points, and realised you don't even need the map to store things, its countable as you progress.*/

            var width = map.GetLength(0);
            var height = map.GetLength(1);

            var list = points.Values.ToList();

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var mp = list.MinBy(mb => GetManhattanDistance(mb, x, y));
                    var lp = mp.First();
                    var d = GetManhattanDistance(lp, x, y);

                    if (mp.Count() > 1)
                    {
                        map[x, y] = new Dist { Id = '.', D = d };
                    }
                    else
                    {
                        var point = points[lp.Id];

                        map[x, y] = new Dist { Id = lp.Id, D = d };

                        point.Count++;
                        if (IsInfinite(x, y, width, height))
                            point.TouchesInfinity = true;
                    }
                }
            }

            var result = points.Values.OrderByDescending(o => o.Count).First(p => !p.TouchesInfinity);

            return (result.Id, result.Count);
        }

        private static bool IsInfinite(int x, int y, int width, int height)
        {
            return (x == 0 || y == 0 || x == width - 1 || x == height - 1);
        }

        private static int GetManhattanDistance(Point a, int x, int y)
        {
            return Math.Abs(a.X - x) + Math.Abs(a.Y - y);
        }

        private static Dist[,] GetMap(Dictionary<char, Point> points)
        {
            var maxX = points.Values.Max(m => m.X);
            var maxY = points.Values.Max(m => m.Y);
            return new Dist[maxX, maxY];
        }

        private static Dictionary<char, Point> GetCoordinates(IEnumerable<string> inputs)
        {
            var id = 0;
            return inputs.Select(i =>
            {
                var s = i.Split(',');

                return new Point
                {
                    Id = Ids[id++],
                    X = int.Parse(s[0]),
                    Y = int.Parse(s[1])
                };

            }).ToDictionary(e => e.Id, e => e );
        }

        public override async Task<string> RunPart2()
        {
            return await Task.FromResult("");
        }

        private class Dist
        {
            public char Id;
            public int D;
            public bool IsInfinite;
        }

        private class Point
        {
            public char Id;
            public int X;
            public int Y;
            public bool TouchesInfinity;
            public int Count;
        }
    }
}