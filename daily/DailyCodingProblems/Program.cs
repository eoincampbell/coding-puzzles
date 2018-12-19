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
                //new Puzzles.Day001.Impl(),
                //new Puzzles.Day002.Impl(),
                //new Puzzles.Day003.Impl(),
                //new Puzzles.Day004.Impl(),
                //new Puzzles.Day004b.Impl(),
                //new Puzzles.Day014.Impl(),
                new Puzzles.Day015.Impl(),
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