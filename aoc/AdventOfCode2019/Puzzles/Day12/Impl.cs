/*
 * Day 12: The N-Body Problem
 * https://adventofcode.com/2019/day/12
 * Part 1: 12644
 * Part 2: 290314621566528
 * Test Data
 * Part 1: 183    (179 for 10 Iterations)
 * Part 2: 2772 
 */
namespace AdventOfCode2019.Puzzles.Day12
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Base;
    using MoonHash = System.Collections.Generic.HashSet<(int, int, int, int, int, int, int, int)>;

    public class Impl : Puzzle<string, long>
    {
        //public Impl() : base("Day 12: The N-Body Problem", ".\\Puzzles\\Day12\\Input-test.txt") { }
        public Impl() : base("Day 12: ", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                var moons = GetMoons();
                for (var s = 1; s <= 1000; s++)
                {
                    for (var m = 0; m < moons.Length; m++)
                        for (var o = 0; o < moons.Length; o++)
                            if (m != o)
                                moons[m].UpdateVelocity(moons[o]);

                    foreach (var m in moons) m.UpdatePosition();
                }

                return moons[0].TotalEnergy + moons[1].TotalEnergy + moons[2].TotalEnergy + moons[3].TotalEnergy;
            });

        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                var mm = GetMoons();
                var xHashes = new MoonHash();
                var yHashes = new MoonHash();
                var zHashes = new MoonHash();
                int xi = 0, yi = 0, zi = 0;
                bool xF = false, yF = false, zF = false;
                while (true)
                {
                    var xs = (mm[0].X, mm[0].VX, mm[1].X, mm[1].VX, mm[2].X, mm[2].VX, mm[3].X, mm[3].VX);
                    var ys = (mm[0].Y, mm[0].VY, mm[1].Y, mm[1].VY, mm[2].Y, mm[2].VY, mm[3].Y, mm[3].VY);
                    var zs = (mm[0].Z, mm[0].VZ, mm[1].Z, mm[1].VZ, mm[2].Z, mm[2].VZ, mm[3].Z, mm[3].VZ);

                    if (xHashes.Contains(xs)) xF = true;
                    else xHashes.Add(xs);
                    if (yHashes.Contains(ys)) yF = true;
                    else yHashes.Add(ys);
                    if (zHashes.Contains(zs)) zF = true;
                    else zHashes.Add(zs);

                    if (xF && yF && zF) break;

                    for (var m = 0; m < mm.Length; m++)
                        for (var o = 0; o < mm.Length; o++)
                            if (m != o)
                                mm[m].UpdateVelocity(mm[o]);

                    foreach (var m in mm) m.UpdatePosition();

                    if (!xF) xi++;
                    if (!yF) yi++;
                    if (!zF) zi++;
                }

                //18, 44, 28 = 2772
                //84032, 286332, 193052 = 290314621566528
                return LeastCommonMultiple(xi, LeastCommonMultiple(yi, zi));
            });

        private Moon[] GetMoons()
            => new[]
            {
                new Moon("Io      ", Inputs[0]),
                new Moon("Europa  ", Inputs[1]),
                new Moon("Ganymede", Inputs[2]),
                new Moon("Callisto", Inputs[3])
            };

        private static long GreatestCommonFactor(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LeastCommonMultiple(long a, long b)
            => (a / GreatestCommonFactor(a, b)) * b;

        private class Moon
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Z { get; private set; }
            public int VX { get; private set; }
            public int VY { get; private set; }
            public int VZ { get; private set; }
            private string Name { get; }

            public Moon(string name, string coords)
            {
                Name = name;
                var c = coords
                    .Replace('<', ' ').Replace('=', ' ').Replace('>', ' ')
                    .Replace('x', ' ').Replace('y', ' ').Replace('z', ' ').Split(",");

                X = int.Parse(c[0], CultureInfo.CurrentCulture);
                Y = int.Parse(c[1], CultureInfo.CurrentCulture);
                Z = int.Parse(c[2], CultureInfo.CurrentCulture);
            }

            public void UpdateVelocity(Moon other)
            {
                if (other.X > X) VX++;
                if (other.X < X) VX--;
                if (other.Y > Y) VY++;
                if (other.Y < Y) VY--;
                if (other.Z > Z) VZ++;
                if (other.Z < Z) VZ--;
            }

            public void UpdatePosition()
            {
                X += VX;
                Y += VY;
                Z += VZ;
            }

            private int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
            private int KineticEnergy => Math.Abs(VX) + Math.Abs(VY) + Math.Abs(VZ);
            public int TotalEnergy => PotentialEnergy * KineticEnergy;
            public override string ToString() => $"{Name}: Pos: {{{X},{Y},{Z}}} Vel: {{{VX},{VY},{VZ}}}";
        }
    }
}