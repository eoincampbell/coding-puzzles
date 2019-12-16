/*
 * Day 12: The N-Body Problem
 * https://adventofcode.com/2019/day/12
 * Leaving this here for posterity...
 * I can't figure out why this doesn't work, but the seperated X/Y/Z co-ords one does.
 * After ~2minutes Part 2 finds each of the 4 planets arriving back in their starting positions/velocities
 * 21189253, 16976651, 9459314, 10421529
 * Day 12:                                            | Part 2 | Exec: 00:02:07.3276351 | 123516899772406094
 */
namespace AdventOfCode2019.Puzzles.Day12
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Base;
    using MoonHash = System.Collections.Generic.HashSet<(int, int, int, int, int, int)>;

    public class Impl3 : Puzzle<string, long>
    {
        //public Impl3() : base("Day 12: The N-Body Problem", ".\\Puzzles\\Day12\\Input-test.txt") { }
        public Impl3() : base("Day 12: The N-Body Problem (Broken)", ".\\Puzzles\\Day12\\Input.txt") { }

        public override async Task<long> RunPart1Async()
            => await Task.Run(() =>
            {
                return 0;
                //var moons = GetMoons();
                //for (var s = 1; s <= 1000; s++)
                //{
                //    for (var m = 0; m < moons.Length; m++)
                //        for (var o = 0; o < moons.Length; o++)
                //            if (m != o)
                //                moons[m].UpdateVelocity(moons[o]);

                //    foreach (var m in moons) m.UpdatePosition();
                //}

                //return moons[0].TotalEnergy + moons[1].TotalEnergy + moons[2].TotalEnergy + moons[3].TotalEnergy;
            });

        public override async Task<long> RunPart2Async()
            => await Task.Run(() =>
            {
                return 0;
                //var ioHashes = new MoonHash();
                //var euHashes = new MoonHash();
                //var gaHashes = new MoonHash();
                //var clHashes = new MoonHash();
                //int ioi = 0, eui = 0, gai = 0, cli = 0;
                //bool ioF = false, euF = false, gaF = false, clF =false;


                //var mm = GetMoons();
                //while (true)
                //{
                //    var ios = (mm[0].X, mm[0].VX, mm[0].Y, mm[0].VY, mm[0].Z, mm[0].VZ);
                //    if (ioHashes.Contains(ios) && !ioF) ioF = true;
                //    else ioHashes.Add(ios);

                //    if (ioF) break;

                //    for (var m = 0; m < mm.Length; m++)
                //        for (var o = 0; o < mm.Length; o++)
                //            if (m != o)
                //                mm[m].UpdateVelocity(mm[o]);

                //    foreach (var m in mm) m.UpdatePosition();

                //    if (!ioF) ioi++;
                //}
                //mm = GetMoons();
                //while (true)
                //{
                //    var eus = (mm[1].X, mm[1].VX, mm[1].Y, mm[1].VY, mm[1].Z, mm[1].VZ);
                //    if (euHashes.Contains(eus) && !euF) euF = true;
                //    else euHashes.Add(eus);

                //    if (euF) break;

                //    for (var m = 0; m < mm.Length; m++)
                //        for (var o = 0; o < mm.Length; o++)
                //            if (m != o)
                //                mm[m].UpdateVelocity(mm[o]);

                //    foreach (var m in mm) m.UpdatePosition();

                //    if (!euF) eui++;
                //}
                //mm = GetMoons();
                //while (true)
                //{
                //    var gas = (mm[2].X, mm[2].VX, mm[2].Y, mm[2].VY, mm[2].Z, mm[2].VZ);
                //    if (gaHashes.Contains(gas) && !gaF) gaF = true;
                //    else gaHashes.Add(gas);

                //    if (gaF) break;

                //    for (var m = 0; m < mm.Length; m++)
                //        for (var o = 0; o < mm.Length; o++)
                //            if (m != o)
                //                mm[m].UpdateVelocity(mm[o]);

                //    foreach (var m in mm) m.UpdatePosition();

                //    if (!gaF) gai++;
                //}
                //mm = GetMoons();
                //while (true)
                //{
                //    var cls = (mm[3].X, mm[3].VX, mm[3].Y, mm[3].VY, mm[3].Z, mm[3].VZ);
                //    if (clHashes.Contains(cls) && !clF) clF = true;
                //    else clHashes.Add(cls);

                //    if (clF) break;

                //    for (var m = 0; m < mm.Length; m++)
                //        for (var o = 0; o < mm.Length; o++)
                //            if (m != o)
                //                mm[m].UpdateVelocity(mm[o]);

                //    foreach (var m in mm) m.UpdatePosition();

                //    if (!clF) cli++;
                //}


                //Console.WriteLine($"{ioi}, {eui}, {gai}, {cli}");
                ////18, 44, 28 = 2772
                ////84032, 286332, 193052 = 290314621566528
                //return LeastCommonMultiple(ioi,
                //        LeastCommonMultiple(eui,
                //        LeastCommonMultiple(gai, cli)));
            });

        //private Moon[] GetMoons()
        //    => new[]
        //    {
        //        new Moon("Io      ", Inputs[0]),
        //        new Moon("Europa  ", Inputs[1]),
        //        new Moon("Ganymede", Inputs[2]),
        //        new Moon("Callisto", Inputs[3])
        //    };

        //private static long GreatestCommonFactor(long a, long b)
        //{
        //    while (b != 0)
        //    {
        //        var temp = b;
        //        b = a % b;
        //        a = temp;
        //    }
        //    return a;
        //}

        //private static long LeastCommonMultiple(long a, long b)
        //    => (a / GreatestCommonFactor(a, b)) * b;

        //private class Moon
        //{
        //    public int X { get; private set; }
        //    public int Y { get; private set; }
        //    public int Z { get; private set; }
        //    public int VX { get; private set; }
        //    public int VY { get; private set; }
        //    public int VZ { get; private set; }
        //    public string Name { get; }

        //    public Moon(string name, string coords)
        //    {
        //        Name = name;
        //        var c = coords
        //            .Replace('<', ' ').Replace('=', ' ').Replace('>', ' ')
        //            .Replace('x', ' ').Replace('y', ' ').Replace('z', ' ').Split(",");

        //        X = int.Parse(c[0], CultureInfo.CurrentCulture);
        //        Y = int.Parse(c[1], CultureInfo.CurrentCulture);
        //        Z = int.Parse(c[2], CultureInfo.CurrentCulture);
        //    }

        //    public void UpdateVelocity(Moon other)
        //    {
        //        if (other.X > X) VX++;
        //        if (other.X < X) VX--;
        //        if (other.Y > Y) VY++;
        //        if (other.Y < Y) VY--;
        //        if (other.Z > Z) VZ++;
        //        if (other.Z < Z) VZ--;
        //    }

        //    public void UpdatePosition()
        //    {
        //        X += VX;
        //        Y += VY;
        //        Z += VZ;
        //    }

        //    public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        //    public int KineticEnergy => Math.Abs(VX) + Math.Abs(VY) + Math.Abs(VZ);
        //    public int TotalEnergy => PotentialEnergy * KineticEnergy;

        //    public override string ToString() => $"{Name}: Pos: {{{X},{Y},{Z}}} Vel: {{{VX},{VY},{VZ}}}";
        //}
    }
}