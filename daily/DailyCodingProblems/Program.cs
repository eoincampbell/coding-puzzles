namespace DailyCodingProblems
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var puzzles = new List<IPuzzle>()
            {
                //new Puzzles.Day1.Impl(),
                //new Puzzles.Day2.Impl(),
                //new Puzzles.Day3.Impl(),
                //new Puzzles.Day4.Impl(),
                new Puzzles.Day4b.Impl(),
            };

            foreach(var p in puzzles)
            {
                var r = await p.Execute();
                Console.WriteLine(r);
            }

            Console.WriteLine("---- press any key to exit ----");
            Console.ReadLine();
        }
    }
}