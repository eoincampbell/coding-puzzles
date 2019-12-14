/*
 * Day 10: Monitoring Station
 * https://adventofcode.com/2019/day/10
 * Part 1: 214
 * Part 2: 502
 */
namespace AdventOfCode2019.Puzzles.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Dict = System.Collections.Generic.Dictionary<Asteroid, System.Collections.Generic.Dictionary<double, System.Collections.Generic.SortedList<double, Asteroid>>>;
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 10: Monitoring Station", ".\\Puzzles\\Day10\\Input.txt") { }

        public override async Task<int> RunPart1Async()
            => await Task.Run(() => 
            { 
                var asteroids = GetAsteroids();
                return asteroids.ToList().OrderByDescending(o => o.Value.Count)
                    .First().Value.Count;
            });

        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var asteroids = GetAsteroids();
                var src = asteroids.ToList().OrderByDescending(o => o.Value.Count)
                    .First().Key;

                var targets = new List<Asteroid>();

                foreach (var ang in asteroids[src])
                    ang.Value
                    .Select((ast, idx) => (Asteroid: ast.Value, Angle: ast.Value.Angle + (360 * idx)))
                    .ToList()
                    .ForEach(f =>
                    {
                        f.Asteroid.SweepAngle = f.Angle;//angle;
                        targets.Add(f.Asteroid);
                    });

                return targets.OrderBy(t => t.SweepAngle).Skip(199).Take(1).Select(s => (s.X * 100) + s.Y).First();
            });
    
        private Dict GetAsteroids()
        {
            var asteroids = new Dict();
            
            for (var y = 0; y < Inputs.Count; y++)
            for (var x = 0; x < Inputs[y].Length; x++)
                if (Inputs[y][x] == '#')
                    asteroids.Add(new Asteroid(x,y), new Dictionary<double, SortedList<double, Asteroid>>());

            foreach (var src in asteroids.Keys)
            foreach (var (t,d,a) in asteroids.Keys.Where(s => s != src).Select(t => (t, GetDist(src, t), GetAngle(src, t))))
            {
                var newTrg = new Asteroid(t.X, t.Y, d, a);
                if (asteroids[src].ContainsKey(a))
                    asteroids[src][a].Add(d,newTrg);
                else
                    asteroids[src].Add(a, new SortedList<double, Asteroid> { { d, newTrg } });
            }

            return asteroids;
        }

        private static double GetAngle(Asteroid s, Asteroid t)
            => ((Math.Atan2(t.Y - s.Y, t.X - s.X) * 180 / Math.PI) + 450) % 360;
        private static double GetDist(Asteroid src, Asteroid trg)
            => Math.Sqrt(Math.Pow(trg.X - src.X, 2) + Math.Pow(trg.Y - src.Y, 2));
    }

    public class Asteroid : IEquatable<Asteroid>
    {
        public Asteroid(int x, int y) { X = x; Y = y; }
        public Asteroid(int x, int y, double d, double a) : this(x,y) { Distance = d; Angle = a; }
        public int X {get; }
        public int Y{get; }
        public double Angle{get; set;}
        public double Distance{get; set;}
        public double SweepAngle{get; set;}
        public override int GetHashCode() => (X, Y).GetHashCode();
        public static bool operator ==(Asteroid s, Asteroid t)
        {
            if (ReferenceEquals(s, t)) return true;
            if (s is null || t is null) return false;
            return (s.X == t.X && s.Y == t.Y);
        }

        public static bool operator !=(Asteroid s, Asteroid t) => !(s == t);
        public bool Equals(Asteroid other) => this == other;
        public override bool Equals(object? obj) => (obj is Asteroid a) && this == a; 

    }
}