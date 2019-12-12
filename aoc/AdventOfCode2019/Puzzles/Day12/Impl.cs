/*
 * Day 12: The N-Body Problem
 * https://adventofcode.com/2019/day/12
 * Part 1: 12644
 * Part 2: 290314621566528
 * Test Data
 * Part 1: 183    (179 - 10 Iterations)
 * Part 2: 2772 
 */
namespace AdventOfCode2019.Puzzles.Day12
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string, long>
    {
        //public Impl() : base("Day 12: ", ".\\Puzzles\\Day12\\Input-test.txt") { }
        public Impl() : base("Day 12: ", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<long> RunPart1Async()
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
                        moon.UpdatePosition();

                }

                foreach (var v in moons) Console.WriteLine(v);

                return io.TotalEnergy + eu.TotalEnergy + ga.TotalEnergy + cl.TotalEnergy;
            });
        }

        public override async Task<long> RunPart2Async()
        {
            return await Task.Run(() =>
            {
                var io = new Moon("Io", Inputs[0]);
                var eu = new Moon("Europa", Inputs[1]);
                var ga = new Moon("Ganymede", Inputs[2]);
                var cl = new Moon("Calisto", Inputs[3]);

                var moons = new[] {io, eu, ga, cl};

                HashSet<(int x1, int xv1, int x2, int xv2, int x3, int xv3, int x4, int xv4)> xHashes =
                    new HashSet<(int, int, int, int, int, int, int, int)>();
                HashSet<(int y1, int yv1, int y2, int yv2, int y3, int yv3, int y4, int yv4)> yHashes =
                    new HashSet<(int, int, int, int, int, int, int, int)>();
                HashSet<(int z1, int zv1, int z2, int zv2, int z3, int zv3, int z4, int zv4)> zHashes =
                    new HashSet<(int, int, int, int, int, int, int, int)>();

                int xi = 0, yi = 0, zi = 0;
                
                while (true)
                {
                    var xs = (io.X, io.vX, eu.X, eu.vX, ga.X, ga.vX, cl.X, cl.vX);
                    if (xHashes.Contains(xs)) break;
                    
                    xHashes.Add(xs);
                    for (var m = 0; m < moons.Length; m++)
                    for (var o = 0; o < moons.Length; o++)
                        if (m != o)
                            moons[m].UpdateVelocity(moons[o], 0);

                    foreach (var moon in moons)
                        moon.UpdatePosition(0);

                    xi++;
                }

                while (true)
                {
                    var ys = (io.Y, io.vY, eu.Y, eu.vY, ga.Y, ga.vY, cl.Y, cl.vY);
                    if (yHashes.Contains(ys)) break;
                    
                    yHashes.Add(ys);
                    for (var m = 0; m < moons.Length; m++)
                    for (var o = 0; o < moons.Length; o++)
                        if (m != o)
                            moons[m].UpdateVelocity(moons[o], 1);

                    foreach (var moon in moons)
                        moon.UpdatePosition(1);

                    yi++;
                }

                while (true)
                {
                    var zs = (io.Z, io.vZ, eu.Z, eu.vZ, ga.Z, ga.vZ, cl.Z, cl.vZ);
                    if (zHashes.Contains(zs)) break;
                    
                    zHashes.Add(zs);
                    for (var m = 0; m < moons.Length; m++)
                    for (var o = 0; o < moons.Length; o++)
                        if (m != o)
                            moons[m].UpdateVelocity(moons[o], 2);

                    foreach (var moon in moons)
                        moon.UpdatePosition(2);

                    zi++;
                }


                return Lcm(xi,Lcm(yi,zi));
            });
        }

        private static long Gcf(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long Lcm(long a, long b)
        {
            return (a / Gcf(a, b)) * b;
        }
    }

    

    public class Moon
    {
        public int X;
        public int Y;
        public int Z;
        public int vX = 0;
        public int vY = 0;
        public int vZ = 0;
        public string Name { get; }

        public Moon(string name, string coords)
        {
            Name = name;
            var c = coords
                .Replace('<', ' ').Replace('=', ' ').Replace('>', ' ')
                .Replace('x', ' ').Replace('y', ' ').Replace('z', ' ').Split(",");

            X = int.Parse(c[0]);
            Y = int.Parse(c[1]);
            Z = int.Parse(c[2]);
        }

        public void UpdateVelocity(Moon other)
        {
            UpdateVelocity(other, 0);
            UpdateVelocity(other, 1);
            UpdateVelocity(other, 2);
        }

        public void UpdateVelocity(Moon other, int dimension)
        {
            if (dimension == 0)
            {
                if (other.X > X) vX++;
                if (other.X < X) vX--;
            }

            if (dimension == 1)
            {
                if (other.Y > Y) vY++;
                if (other.Y < Y) vY--;
            }

            if (dimension == 2)
            {
                if (other.Z > Z) vZ++;
                if (other.Z < Z) vZ--;
            }
        }

        public void UpdatePosition()
        {
            UpdatePosition(0);
            UpdatePosition(1);
            UpdatePosition(2);
        }

        public void UpdatePosition(int dimension)
        {
            switch (dimension)
            {
                case 0:
                    X += vX;
                    break;
                case 1:
                    Y += vY;
                    break;
                case 2:
                    Z += vZ;
                    break;
            }
        }


        public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        public int KineticEnergy => Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);
        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        public override int GetHashCode() => (X, Y, Z, vX, vY, vZ).GetHashCode();
        public override bool Equals(object? obj)
        {
            if (obj is Moon m)
            {
                return m.X == X && m.Y == Y && m.Z == Z && m.vX == vX && m.vY == vY && m.vZ == vZ;
            }

            return false;
        }
        public override string ToString() => $"{Name:0,-10}: Pos: {{{X},{Y},{Z}}} Vel: {{{vX},{vY},{vZ}}}";
    }
}