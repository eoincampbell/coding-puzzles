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
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 10: Monitoring Station", ".\\Puzzles\\Day10\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            return await Task.Run(() => { 
                var asteroids = GetAsteroids();

                return asteroids.ToList()
                    .OrderByDescending(o => o.Value.Count())
                    .Select(s => s.Value.Count())
                    .First();
            });
        }


        private Dictionary<Asteroid, Dictionary<double, List<Asteroid>>> GetAsteroids()
        {
            var asteroids = new Dictionary<Asteroid, Dictionary<double, List<Asteroid>>>();
            
            for(var y = 0; y < Inputs.Length; y++)
                for (var x = 0; x < Inputs[y].Length; x++)
                    if (Inputs[y][x] == '#')
                        asteroids.Add(new Asteroid(x,y), new Dictionary<double, List<Asteroid>>());

            foreach (var src in asteroids.Keys)
            foreach (var trg in asteroids.Keys.Where(t => t != src))
            {
                var angle = GetAngle(src, trg);
                var dist = GetDist(src, trg);

                var newTrg = new Asteroid(trg.X,trg.Y)
                {
                    DistanceFromSource = dist,
                    AngleFromVerticalToSource = angle
                };
                if (asteroids[src].ContainsKey(angle))
                    asteroids[src][angle].Add(newTrg);
                else
                    asteroids[src].Add(angle, new List<Asteroid> {newTrg});
            }

            return asteroids;
        }

        private double GetAngle(Asteroid src, Asteroid trg)
        {
            //this took some pen & paper... the above formula gives an angle from the horizontal in a clockwise direction.
            var angle = Math.Round(Math.Atan2(trg.Y - src.Y, trg.X - src.X) * 180 / Math.PI, 6);
            //for negative angles push them into the positive with 1 full turn.
            if (angle < 0)      
                angle += 360;
            //rotate all angles 90 deg, and then reset the ones that have gone into a subsequent turn.
            return (angle + 90) % 360; 
        }

        private double GetDist(Asteroid src, Asteroid trg)
            => Math.Sqrt(Math.Pow(trg.X - src.X, 2) + Math.Pow(trg.Y - src.Y, 2));
        

        public override async Task<int> RunPart2Async()
        {
            return await Task.Run(() =>
            {
                var asteroids = GetAsteroids();

                var src = asteroids.ToList()
                    .OrderByDescending(o => o.Value.Count())
                    .First().Key;

                var angles = asteroids[src];
                var allTargets = new List<Asteroid>();
                foreach (var ang in angles)
                {
                    var i = 0;
                    foreach (var t in ang.Value.OrderBy(s => s.DistanceFromSource))
                    {
                        t.SweepAngleFromSource = t.AngleFromVerticalToSource + (360 * i++);
                        allTargets.Add(t);
                    }
                }

                var answer = allTargets.OrderBy(t => t.SweepAngleFromSource).Skip(199).Take(1).First();

                return (answer.X * 100) + answer.Y;
            });
        }
    }

    public class Asteroid
    {
        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X {get; }
        public int Y{get; }
        public double AngleFromVerticalToSource{get; set;}
        public double DistanceFromSource{get; set;}
        public double SweepAngleFromSource{get; set;}
        
        public static bool operator ==(Asteroid s, Asteroid t) => s.Equals(t);
        public static bool operator !=(Asteroid s, Asteroid t) => !s.Equals(t);

        public override bool Equals(object obj)
        {
            if(!(obj is Asteroid)) return false;

            var t = (Asteroid) obj;

            return t.X == X && t.Y == Y;
        }

        public override int GetHashCode() => (X * 97) + Y;
    }
}