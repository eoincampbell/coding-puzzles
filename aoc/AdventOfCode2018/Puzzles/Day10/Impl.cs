namespace AdventOfCode2018.Puzzles.Day10
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        //public const string FILE = ".\\Puzzles\\Day10\\InputSimple.txt";
        public const string FILE = ".\\Puzzles\\Day10\\Input.txt";
        private string _part2Answer = "";

        public Impl() : base("Day10 ", FILE) { }
        
        public override async Task<string> RunPart1()
        {
            var stars = ProcessInput(Inputs);
            int timecode = 0, pxb = int.MaxValue, pyb = int.MaxValue; //previous X & Y bounds

            while (true)
            {
                stars.ForEach(f => f.Update(timecode));
                var nxb = stars.Max(m => m.NextX) - stars.Min(m => m.NextX);
                var nyb = stars.Max(m => m.NextY) - stars.Min(m => m.NextY); //new X & Y bounds

                if (nxb > pxb && nyb > pyb) break; //keep going while bounds are shrinking then break. (not sure if this is valid for every data set.

                pxb = nxb;
                pyb = nyb;
                timecode++;
            }

            using (var sw = new StreamWriter(".\\day10.txt"))
                for (var y = stars.Min(m=>m.Y); y <= stars.Max(m =>m.Y); y++)
                    for (var x = stars.Min(m => m.X); x <= stars.Max(m => m.X); x++)
                        sw.Write((stars.Any(s => (s.X == x && s.Y == y)) ? "#" : " ") 
                            + (x == stars.Max(m => m.X) ? Environment.NewLine : ""));
            
            _part2Answer = $"{timecode}";

            return await Task.FromResult($"See Day10.txt in output directory");
        }

        public override async Task<string> RunPart2()
        {
            return await Task.FromResult($"{_part2Answer}");
        }

        public List<Star> ProcessInput(IEnumerable<string> inputs)
        {
            var r = new Regex(@"position=<([- ]?\d+), ([- ]?\d+)> velocity=<([- ]?\d+), ([- ]?\d+)>");

            return Inputs
                .Select(s => r.Match(s))
                .Select(m => new Star
                {
                    StartX = int.Parse(m.Groups[1].Value),
                    StartY = int.Parse(m.Groups[2].Value),
                    VelX = int.Parse(m.Groups[3].Value),
                    VelY = int.Parse(m.Groups[4].Value)
                }).ToList();
        }

        public class Stars : List<Star>
        {
            public int MinX => this.Min(m => m.X);
        }

        public class Star
        {
            public int StartX;
            public int StartY;
            public int VelX;
            public int VelY;
            public int X;
            public int Y;
            public int NextX;
            public int NextY;

            public void Update(int timecode)
            {
                X = StartX + timecode * VelX;
                Y = StartY + timecode * VelY;
                NextX = StartX + (timecode+1) * VelX;
                NextY = StartY + (timecode+1) * VelY;
            }
        }
    }
}