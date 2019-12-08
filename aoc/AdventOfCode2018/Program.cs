namespace AdventOfCode2018
{
    using Base;
    using System;
    using System.Collections.Generic;
    
    public class Program
    {
        public static void Main()
        {
            var puzzles = new List<IPuzzle>
            {
                //new Puzzles.Day1.Impl()
                //, new Puzzles.Day2.Impl()
                //, new Puzzles.Day3.Impl()
                //, new Puzzles.Day3.Impl2()
                //, new Puzzles.Day4.Impl()
                //            //, new Puzzles.Day5.Impl() //recursive is way to slow
                //, new Puzzles.Day5.Impl2()
                //, new Puzzles.Day5.Impl3()
                //new Puzzles.Day6.Impl(),
                //new Puzzles.Day6.Impl2(),
                //new Puzzles.Day7.Impl(),
                //new Puzzles.Day8.Impl(),
                //new Puzzles.Day8.Impl2(),
                //new Puzzles.Day9.Impl(),
                //new Puzzles.Day9.Impl2(),
                //new Puzzles.Day10.Impl(),
                //new Puzzles.Day11.Impl(),
                //new Puzzles.Day12.Impl(),
                //new Puzzles.Day13.Impl(),
                //new Puzzles.Day14.Impl(),
                //  new Puzzles.Day15.Impl(),
                //new Puzzles.Day15.Impl2.Impl(),
                //new Puzzles.Day15.Impl(),
                //new Puzzles.Day16.Impl()
                new Puzzles.Day17.Impl()
            };

            puzzles.ForEach(f => f.RunBothParts());

            Console.WriteLine("----- press enter to end -----");
            Console.ReadLine();
        }
    }
}
