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
    using Dict = System.Collections.Generic.Dictionary<Asteroid, System.Collections.Generic.Dictionary<double, System.Collections.Generic.List<Asteroid>>>;
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 10: Monitoring Station", ".\\Puzzles\\Day10\\Input.txt") { }

        public override async Task<int> RunPart1Async()
            => await Task.Run(() => 
            { 
                var asteroids = GetAsteroids();
                return asteroids.ToList().OrderByDescending(o => o.Value.Count())
                    .First().Value.Count();
            });
        

        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var asteroids = GetAsteroids();
                var src = asteroids.ToList().OrderByDescending(o => o.Value.Count())
                    .First().Key;

                var angles = asteroids[src];
                var targets = new List<Asteroid>();
                foreach (var ang in angles.Values)
                {
                    var i = 0;
                    foreach (var asteroid in ang.OrderBy(s => s.Distance))
                    {
                        asteroid.SweepAngle = asteroid.Angle + (360 * i++);
                        targets.Add(asteroid);
                    }
                }

                return targets.OrderBy(t => t.SweepAngle).Skip(199).Take(1).Select(s => (s.X * 100) + s.Y).First();
            });
    
        private Dict GetAsteroids()
        {
            var asteroids = new Dict();
            
            for (var y = 0; y < Inputs.Length; y++)
            for (var x = 0; x < Inputs[y].Length; x++)
                if (Inputs[y][x] == '#')
                    asteroids.Add(new Asteroid(x,y), new Dictionary<double, List<Asteroid>>());

            foreach (var src in asteroids.Keys)
            foreach (var (t,a) in asteroids.Keys.Where(s => s != src).Select(t => (t, GetAngle(src, t))))
            {
                var newTrg = new Asteroid(t.X, t.Y, GetDist(src, t), a);
                if (asteroids[src].ContainsKey(a))
                    asteroids[src][a].Add(newTrg);
                else
                    asteroids[src].Add(a, new List<Asteroid> {newTrg});
            }

            return asteroids;
        }

        private static double GetAngle(Asteroid s, Asteroid t)
            => FixAngle(Math.Atan2(t.Y - s.Y, t.X - s.X) * 180 / Math.PI);

        private static double FixAngle(double angle)
            => (angle < 0 ? angle + 450 : angle + 90) % 360;

        private static double GetDist(Asteroid src, Asteroid trg)
            => Math.Sqrt(Math.Pow(trg.X - src.X, 2) + Math.Pow(trg.Y - src.Y, 2));
    }

    public class Asteroid
    {
        public Asteroid(int x, int y) { X = x; Y = y; }
        public Asteroid(int x, int y, double d, double a) : this(x,y) { Distance = d; Angle = a; }
        public int X {get; }
        public int Y{get; }
        public double Angle{get; set;}
        public double Distance{get; set;}
        public double SweepAngle{get; set;}
        public static bool operator ==(Asteroid s, Asteroid t) => s.Equals(t);
        public static bool operator !=(Asteroid s, Asteroid t) => !s.Equals(t);
        public override bool Equals(object o) => (o is Asteroid a) && a?.X == X && a?.Y == Y;
        public override int GetHashCode() => (X * 97) + Y;
    }
}