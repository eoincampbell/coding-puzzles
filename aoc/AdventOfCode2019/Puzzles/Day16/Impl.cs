/*
 * Day 16: Flawed Frequency Transmission
 * https://adventofcode.com/2019/day/16
 * Part 1: 27229269
 * Part 2: 
 */
namespace AdventOfCode2019.Puzzles.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 16: Flawed Frequency Transmission", ".\\Puzzles\\Day16\\Input.txt") { }

        public override async Task<int> RunPart1Async()
            => await Task.Run(() =>
            {
                var numPhases = int.Parse(Inputs[0]);
                var input = Array.ConvertAll(Inputs[1].ToCharArray().Select(s => s.ToString()).ToArray(), int.Parse);
                var len = input.Length;
                var patterns = new List<int[]>();

                for (var i = 0; i < len; i++) patterns.Add(GetPattern(i, len));

                Console.Write(string.Join("", input) + " => ");

                var outputList = new int[len];

                for (var p = 1; p <= numPhases; p++)
                {
                    outputList = new int[len];
                    for (var o = 0; o < len; o++)
                    {
                        var pat = patterns[o];
                        var digit = 0;
                        for (var i = 0; i < len; i++)
                        {
                            digit += (input[i] * pat[i]);
                        }

                        outputList[o] = Math.Abs(digit % 10);
                    }

                    input = outputList;
                    //Console.WriteLine(string.Join("", input));
                }
                Console.WriteLine(string.Join("", input));

                return int.Parse(string.Join("", input[0..8]));
            });
        

        public override async Task<int> RunPart2Async()
            => await Task.Run(() =>
            {
                var numPhases = int.Parse(Inputs[0]);

                var offset = int.Parse(Inputs[1].Substring(0, 7));

                var inp = string.Join("", Enumerable.Repeat(Inputs[1], 10000));

                var input = Array.ConvertAll(inp.ToCharArray().Select(s => s.ToString()).ToArray(), int.Parse);
                var len = input.Length;
                var patterns = new List<int[]>();

                for (var i = 0; i < len; i++) patterns.Add(GetPattern(i, len));

                //Console.Write(string.Join("", input) + " => ");

                var outputList = new int[len];

                for (var p = 1; p <= numPhases; p++)
                {
                    outputList = new int[len];
                    for (var o = 0; o < len; o++)
                    {
                        var pat = patterns[o];
                        var digit = 0;
                        for (var i = 0; i < len; i++)
                        {
                            digit += (input[i] * pat[i]);
                        }

                        outputList[o] = Math.Abs(digit % 10);
                    }

                    input = outputList;
                    //Console.WriteLine(string.Join("", input));
                }
                //Console.WriteLine(string.Join("", input));



                return 0;
            });

        private int [] GetPattern(int element, int length)
        {
            var zeros = Enumerable.Repeat(0, element + 1).ToList();
            var ones =  Enumerable.Repeat(1, element + 1).ToList();
            var negOnes=  Enumerable.Repeat(-1, element + 1).ToList();

            var pattern = zeros.Concat(ones).Concat(zeros).Concat(negOnes).ToList();

            var repeater = (length / pattern.Count) + 1;

            var result = new List<int>();

            for (var i = 0; i <= repeater; i++)
                result.AddRange(pattern);


            return result.ToArray()[1..(length+1)];
        }
    }
}