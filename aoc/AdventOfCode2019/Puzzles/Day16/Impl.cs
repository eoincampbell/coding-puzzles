/*
 * Day 16: Flawed Frequency Transmission
 * https://adventofcode.com/2019/day/16
 * Part 1: 27229269
 * Part 2: 26857164
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

                for (var p = 1; p <= numPhases; p++)
                {
                    var outputList = new int[len];
                    for (var o = 0; o < len; o++)
                    {
                        var pat = patterns[o];
                        var digit = 0;
                        for (var i = 0; i < len; i++)
                            digit += (input[i] * pat[i]);

                        outputList[o] = Math.Abs(digit % 10);
                    }
                    input = outputList;
                }

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

                for (var i = 0; i < 100; i++)
                {
                    var p_sum = 0;
                    for (var j = offset; j < len; j++) p_sum += input[j];
                    for (var k = offset; k < len; k++)
                    {
                        var temp = p_sum;
                        p_sum -= input[k];
                        input[k] = temp * ((temp >= 0) ? 1 : -1) % 10;
                    }
                }

                var result = input[offset..(offset+8)];

                return int.Parse(string.Join("", result));
            });

        private int [] GetPattern(int element, int length)
        {
            var fzeros = Enumerable.Repeat(0, element + 1).ToList();
            var ones =  Enumerable.Repeat(1, element + 1).ToList();
            var sZeros = Enumerable.Repeat(0, element + 1).ToList();
            var negOnes=  Enumerable.Repeat(-1, element + 1).ToList();
            var pattern = fzeros.Concat(ones).Concat(sZeros).Concat(negOnes).ToList();
            
            var repeater = (length / pattern.Count) + 1;

            var result = new List<int>();

            for (var i = 0; i <= repeater; i++)
                result.AddRange(pattern);


            return result.ToArray()[1..(length+1)];
        }
    }
}