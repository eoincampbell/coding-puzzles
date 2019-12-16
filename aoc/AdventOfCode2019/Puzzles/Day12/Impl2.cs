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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Base;
    using MoonHash = System.Collections.Generic.HashSet<(int, int, int, int, int, int, int, int)>;

    public class Impl2 : Puzzle<string, long>
    {
        public Impl2() : base("Day 12: The N-Body Problem (Concise)", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                var m = GetMoons();
                for (var s = 1; s <= 1000; s++)
                {
                    for (var n = 0; n < m.Length; n++)
                        for (var o = 0; o < m.Length; o++)
                            if (n != o)
                                m[n].UpdateVelocity(m[o]);

                    foreach (var mm in m) mm.UpdatePosition();
                }

                return m[0].TotalEnergy + m[1].TotalEnergy + m[2].TotalEnergy + m[3].TotalEnergy;
            });

        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                var m = GetMoons();
                var h = new[] { new MoonHash(), new MoonHash(), new MoonHash() };
                var c = new[] { 0, 0, 0 };
                var f = new[] { false, false, false };

                while (true)
                {
                    var t = new List<(int, int, int, int, int, int, int, int)>();

                    for (var i = 0; i <= 2; i++)
                        t.Add((m[0].Data[i], m[0].Data[i + 3], m[1].Data[i], m[1].Data[i + 3], m[2].Data[i], m[2].Data[i + 3], m[3].Data[i], m[3].Data[i + 3]));

                    for (var i = 0; i <= 2; i++)
                        if (h[i].Contains(t[i]))
                            f[i] = true;
                        else
                            h[i].Add(t[i]);

                    if (f[0] && f[1] && f[2]) break;

                    for (var n = 0; n < m.Length; n++)
                        for (var o = 0; o < m.Length; o++)
                            if (n != o)
                                m[n].UpdateVelocity(m[o]);

                    foreach (var mm in m) mm.UpdatePosition();

                    for (var i = 0; i <= 2; i++) c[i] += (!f[i] ? 1 : 0);
                }

                //18, 44, 28 = 2772
                //84032, 286332, 193052 = 290314621566528
                return LeastCommonMultiple(c[0], LeastCommonMultiple(c[1], c[2]));
            });

        private Moon[] GetMoons()
            => new[] { new Moon(Inputs[0]), new Moon(Inputs[1]), new Moon(Inputs[2]), new Moon(Inputs[3]) };

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
            public int[] Data = { 0, 0, 0, 0, 0, 0 };

            public Moon(string coords)
            {
                var c = coords.Replace('<', ' ').Replace('=', ' ').Replace('>', ' ').Replace('x', ' ').Replace('y', ' ').Replace('z', ' ').Split(",");
                for (var i = 0; i <= 2; i++) Data[i] = int.Parse(c[i], CultureInfo.CurrentCulture);
            }

            public void UpdateVelocity(Moon other)
            {
                for (var i = 0; i <= 2; i++)
                {
                    if (other.Data[i] > Data[i]) Data[i + 3]++;
                    if (other.Data[i] < Data[i]) Data[i + 3]--;
                }
            }

            public void UpdatePosition() { for (var i = 0; i <= 2; i++) Data[i] += Data[i + 3]; }
            

            public int PotentialEnergy => Math.Abs(Data[0]) + Math.Abs(Data[1]) + Math.Abs(Data[2]);
            public int KineticEnergy => Math.Abs(Data[3]) + Math.Abs(Data[4]) + Math.Abs(Data[5]);
            public int TotalEnergy => PotentialEnergy * KineticEnergy;
        }
    }
}