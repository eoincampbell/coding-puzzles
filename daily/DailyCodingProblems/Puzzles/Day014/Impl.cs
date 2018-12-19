namespace DailyCodingProblems.Puzzles.Day14
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    
    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 014") { }

        protected override async Task<string> ExecuteImpl()
        {
            var r = new Random();
            var sw = new Stopwatch();
            double i = 0, rad = 0.499999999999, lim = 100000000;

            sw.Start();
            for (var c = 0; c < lim; c++)
            {
                double x = r.NextDouble(),
                    y = r.NextDouble(),
                    d = GetDistance(rad, rad, x, y);
                if (d <= rad) i++;
            }
            sw.Stop();

            Console.WriteLine($"{4 * i / lim} in {sw.ElapsedMilliseconds}");

            return await Task.FromResult($"");
        }

        private static double GetDistance(double x1, double y1, double x2, double y2) =>
            Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
    }
}
