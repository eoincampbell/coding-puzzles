﻿/*
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
        public Impl() : base("Day 12: The N-Body Problem", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<long> RunPart1Async() => await Task.Run(() =>
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

        public override async Task<long> RunPart2Async() => await Task.Run(() =>
        {
            var mm = GetMoons();
            var xHashes = new MoonHash();
            var yHashes = new MoonHash();
            var zHashes = new MoonHash();
            int xi = 0, yi = 0, zi = 0;
            bool xF = false, yF = false, zF = false;
            while (true)
            {
                var xs = (mm[0].X, mm[0].Vx, mm[1].X, mm[1].Vx, mm[2].X, mm[2].Vx, mm[3].X, mm[3].Vx);
                var ys = (mm[0].Y, mm[0].Vy, mm[1].Y, mm[1].Vy, mm[2].Y, mm[2].Vy, mm[3].Y, mm[3].Vy);
                var zs = (mm[0].Z, mm[0].Vz, mm[1].Z, mm[1].Vz, mm[2].Z, mm[2].Vz, mm[3].Z, mm[3].Vz);

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

        private static long LeastCommonMultiple(long a, long b) => (a / GreatestCommonFactor(a, b)) * b;

        private class Moon
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Z { get; private set; }
            public int Vx { get; private set; }
            public int Vy { get; private set; }
            public int Vz { get; private set; }
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
                if (other.X > X) Vx++;
                if (other.X < X) Vx--;
                if (other.Y > Y) Vy++;
                if (other.Y < Y) Vy--;
                if (other.Z > Z) Vz++;
                if (other.Z < Z) Vz--;
            }

            public void UpdatePosition()
            {
                X += Vx;
                Y += Vy;
                Z += Vz;
            }

            private int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
            private int KineticEnergy => Math.Abs(Vx) + Math.Abs(Vy) + Math.Abs(Vz);
            public int TotalEnergy => PotentialEnergy * KineticEnergy;
            public override string ToString() => $"{Name}: Pos: {{{X},{Y},{Z}}} Vel: {{{Vx},{Vy},{Vz}}}";
        }
    }
}