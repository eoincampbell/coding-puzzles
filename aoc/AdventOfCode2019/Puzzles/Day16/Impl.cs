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
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    
    public class Impl : Puzzle<string, int>
    {
        public Impl() : base("Day 16: Flawed Frequency Transmission", ".\\Puzzles\\Day16\\Input.txt") { }

        public override async Task<int> RunPart1Async() => await Task.Run(() =>
        {
            var numPhases = int.Parse(Inputs[0], CultureInfo.CurrentCulture);
            var input = Array.ConvertAll(Inputs[1].ToCharArray().Select(s => s.ToString(CultureInfo.CurrentCulture)).ToArray(), int.Parse);
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
                    for (var i = 0; i < len; i++) digit += (input[i] * pat[i]);
                    outputList[o] = ((digit < 0) ? digit * -1 : digit) % 10;
                }

                input = outputList;
            }

            return int.Parse(string.Join("", input[0..8]), CultureInfo.CurrentCulture);
        });

        public override async Task<int> RunPart2Async() => await Task.Run(() =>
        {
            var numPhases = int.Parse(Inputs[0], CultureInfo.CurrentCulture);
            var offset = int.Parse(Inputs[1].Substring(0, 7), CultureInfo.CurrentCulture);
            var inp = string.Join("", Enumerable.Repeat(Inputs[1], 10000));
            var input = Array.ConvertAll(
                inp.ToCharArray().Select(s => s.ToString(CultureInfo.CurrentCulture)).ToArray(), int.Parse);
            var len = input.Length;

            for (var i = 0; i < numPhases; i++)
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

            var result = input[offset..(offset + 8)];
            return int.Parse(string.Join("", result), CultureInfo.CurrentCulture);
        });

        private static int[] GetPattern(int element, int length)
        {
            int[] result = new int [length + 1], values = {0, 1, 0, -1};
            
            for (int i = 1, cur = 0, e = element + 1; i <= length + 1; i++)
            {
                result[i-1] = values[cur];
                if(i % e == 0) cur = (cur + 1) % 4;
            }

            return result[1..(length + 1)];
        }
    }
}