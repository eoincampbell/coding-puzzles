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
        public const string FILE = ".\\Puzzles\\Day10\\InputVas.txt";
        //public const string FILE = ".\\Puzzles\\Day10\\Input.txt";
        private string _part2Answer = "";

        public Impl() : base("Day10 ", FILE) { }
        
        public override async Task<string> RunPart1()
        {
            var s = ProcessInput();                                           //vars to track previous and current bounds of the rectangle, 
            int timecode = 0, pxb, pyb, nxb = int.MaxValue, nyb = int.MaxValue;     //assumes it's at it's tightest when aligned. 
            do {                                                                    //no idea if that's a valid assumption, or holds true in other inputs.
                s.ForEach(f => f.Update(timecode));                                 //update all the stars based on the current timecode
                pxb = nxb;                                                          //set the previous bounds
                pyb = nyb;
                nxb = s.Max(m => m.NextX) - s.Min(m => m.NextX);                    //recalculate the next set of bounds
                nyb = s.Max(m => m.NextY) - s.Min(m => m.NextY);
            } while (nxb < pxb && nyb < pyb && ++timecode > 0);                     //break when we've started increasing the bounding rectangle again

            using (var sw = new StreamWriter(".\\day10.txt"))                       //write to file
                for (var y = s.Min(m => m.Y); y <= s.Max(m =>m.Y); y++)             //foreach row in the sky
                    for (var x = s.Min(m => m.X); x <= s.Max(m => m.X); x++)        //foreach col in the sky
                        sw.Write((s.Any(a => (a.X == x && a.Y == y)) ? "#" : " ")   //print a # if x,y point is in that position
                            + (x == s.Max(m => m.X) ? Environment.NewLine : ""));   //+ a CRLF if it's the last point on the row
            
            _part2Answer = $"{timecode}";

            return await Task.FromResult("See Day10.txt in output directory");
        }

        public override async Task<string> RunPart2() 
            => await Task.FromResult($"{_part2Answer}");

        public List<Star> ProcessInput()
        {
            var r = new Regex(@"position=<([- ]{0,2}\d+), ([- ]{0,2}\d+)> velocity=<([- ]?\d+), ([- ]?\d+)>");

            return Inputs
                .Select(s => r.Match(s).Groups)
                .Select(g => new Star{
                    StartX = int.Parse(g[1].Value),
                    StartY = int.Parse(g[2].Value),
                    VelX = int.Parse(g[3].Value),
                    VelY = int.Parse(g[4].Value)
                })
                .ToList();
        }

        public class Star
        {
            public int StartX;
            public int StartY;
            public int VelX;
            public int VelY;
            public int X;
            public int Y;
            public int NextX => X + VelX;
            public int NextY => Y + VelY;

            public void Update(int timecode)
            {
                X = StartX + timecode * VelX;
                Y = StartY + timecode * VelY;
            }
        }
    }
}