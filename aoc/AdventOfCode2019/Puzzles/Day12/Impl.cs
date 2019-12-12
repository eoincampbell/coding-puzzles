/*
 * Day 12: The N-Body Problem
 * https://adventofcode.com/2019/day/12
 * Part 1: 12644
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 12: ", ".\\Puzzles\\Day12\\Input-test.txt") { }
        //public Impl() : base("Day 12: ", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<int> RunPart1Async()
        {
            return await Task.Run(() =>
            {
                var io = new Moon("Io", Inputs[0]);
                var eu = new Moon("Europa", Inputs[1]);
                var ga = new Moon("Ganymede", Inputs[2]);
                var cl = new Moon("Calisto", Inputs[3]);

                var moons = new[] {io, eu, ga, cl};

                foreach (var v in moons) Console.WriteLine(v);

                for (var s = 1; s <= 1000 ; s++)
                {
                    for (var m = 0; m < moons.Length; m++)
                        for (var o = 0; o < moons.Length; o++)
                            if (m != o)
                                moons[m].UpdateVelocity(moons[o]);
                    
                    foreach (var moon in moons)
                        moon.UpdatePosition(s);

                }

                foreach (var v in moons) Console.WriteLine(v);

                return io.TotalEnergy + eu.TotalEnergy + ga.TotalEnergy + cl.TotalEnergy;
            });
        }

        public override async Task<int> RunPart2Async()
        {
            return await Task.Run(() =>
            {
                var io = new Moon("Io", Inputs[0]);
                var eu = new Moon("Europa", Inputs[1]);
                var ga = new Moon("Ganymede", Inputs[2]);
                var cl = new Moon("Calisto", Inputs[3]);

                var moons = new[] {io, eu, ga, cl};

                for (var s = 1; s <= 2772 ; s++)
                {
                    for (var m = 0; m < moons.Length; m++)
                    for (var o = 0; o < moons.Length; o++)
                        if (m != o)
                            moons[m].UpdateVelocity(moons[o]);
                    
                    foreach (var moon in moons)
                        moon.UpdatePosition(s);

                }

                //foreach (var m in moons)
                //{
                //    var xx = m.PrevPositions.Where(kv => kv.Value.Count > 1);
                //    foreach (var r in xx)
                //    {
                //        Console.WriteLine(r.Key + " | " + string.Join(",", r.Value));
                //    }
                //}



                return io.TotalEnergy + eu.TotalEnergy + ga.TotalEnergy + cl.TotalEnergy;
            });
        }
    }

    

    public class Moon
    {
        //public Dictionary<Moon, List<int>> PrevPositions; 
        
        public int X;
        public int Y;
        public int Z;
        public int vX = 0;
        public int vY = 0;
        public int vZ = 0;
        public string Name { get; }

        //private Moon(string name, int x, int y, int z, int vx, int vy, int vz)
        //{
        //    Name = name;
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    vX = vx;
        //    vY = vy;
        //    vZ = vz;
        //    PrevPositions = null;
        //}

        public Moon(string name, string coords)
        {
            Name = name;
            var c = coords
                .Replace('<', ' ').Replace('=', ' ').Replace('>', ' ')
                .Replace('x', ' ').Replace('y', ' ').Replace('z', ' ').Split(",");

            X = int.Parse(c[0]);
            Y = int.Parse(c[1]);
            Z = int.Parse(c[2]);
            
            //PrevPositions = new Dictionary<Moon, List<int>>
            //{
            //    {Clone(), new List<int> {0}}
            //};
        }

        public void UpdateVelocity(Moon other)
        {
            if (other.X > X) vX++;
            if (other.X < X) vX--;
            if (other.Y > Y) vY++;
            if (other.Y < Y) vY--;
            if (other.Z > Z) vZ++;
            if (other.Z < Z) vZ--;
        }

        public void UpdatePosition(int step)
        {
            X += vX;
            Y += vY;
            Z += vZ;

            //var clone = Clone();
            //if (PrevPositions.ContainsKey(clone))
            //{
            //    PrevPositions[clone].Add(step);
            //}
            //else
            //{
            //    PrevPositions.Add(clone, new List<int> {step});
            //}
            
        }

        //public Moon Clone() => new Moon(Name, X, Y, Z, vX, vY, vZ);

        public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        public int KineticEnergy => Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);
        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        //public override int GetHashCode() => (X, Y, Z, vX, vY, vZ).GetHashCode();

        public override string ToString() => $"{Name:0,-10}: Pos: {{{X},{Y},{Z}}} Vel: {{{vX},{vY},{vZ}}}";
    }
}