namespace AdventOfCode2017.Puzzles.Day3
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 3 ", ".\\Puzzles\\Day3\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var i = int.Parse(Inputs.First());
            int n = 1;
            double steps = 0;
            while (true)
            {
                var lvl = Math.Pow((n * 2) - 1, 2);                 //calc the bottom righ corner (2n-1)^2 = 1,9,25,49
                if (lvl > i)                                        //stop when your at a level that includes your input
                {
                    var prevLvl = Math.Pow(((n - 1) * 2) - 1, 2);   //find the total of the previous level
                    var diff = lvl - prevLvl;                       //calc the num cells on this level only
                    var pos = i - prevLvl;                          //find your cells position on this level
                    var sidePos = pos % (diff/4);                   //find your cells position on one of the 4 sides
                    var stcl = Math.Abs(sidePos - (diff / 8));      //count the steps back to the center line
                    steps = stcl + n - 1;                           //total steps = steps to center line, + level - offbyone 
                    break;
                }
                n++;
            }

            return await Task.FromResult($"{steps}");
        }

        public override async Task<string> RunPart2()
        {
       

            return await Task.FromResult($"");
        }
    }
}