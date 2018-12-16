namespace AdventOfCode2018.Puzzles.Day15
{
    using System;
    using Base;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CodingPuzzles.Helpers;
    using AdventOfCode2018.Puzzles.Day15.Code;

    public class Impl : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day15\\Samples\\Input.txt";
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple4.txt"; //47 * 590 = 27730
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple5.txt"; //37 * 982 = 36334
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple6.txt"; //46 * 859 = 39514
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple7.txt"; //35 * 793 = 27755
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple8.txt"; //54 * 536 = 28944
        //public const string FILE = ".\\Puzzles\\Day15\\Samples\\InputSimple9.txt"; //20 * 937 = 18740

        public Impl() : base("Day15 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            Game g = new Game(Inputs);
            try
            {
                g.Play();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return await Task.FromResult($"{g.Score}");
        }

        public override async Task<string> RunPart2()
        {
            return await Task.FromResult($"");
        }
    }
}