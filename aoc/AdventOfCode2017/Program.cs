﻿namespace AdventOfCode2017
{
    using Base;
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            var puzzles = new List<IPuzzle>
            {
                //new Puzzles.Day1.Impl(),
                //new Puzzles.Day2.Impl(),
                //new Puzzles.Day3.Impl(),
                //new Puzzles.Day4.Impl(),
                //new Puzzles.Day5.Impl(),
                //new Puzzles.Day6.Impl(),
                //new Puzzles.Day7.Impl(),
                //new Puzzles.Day8.Impl(),
                new Puzzles.Day9.Impl(),
            };

            puzzles.ForEach(f => f.RunBothParts());

            Console.WriteLine("----- press enter to end -----");
            Console.ReadLine();
        }
    }
}
