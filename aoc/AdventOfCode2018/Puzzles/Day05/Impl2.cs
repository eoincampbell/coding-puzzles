namespace AdventOfCode2018.Puzzles.Day5
{
    using Base;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl2 : BasePuzzle
    {
        public Impl2() : base("Day 5b", ".\\Puzzles\\Day05\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var chars = Inputs.First().ToCharArray().ToList();

            var result = BackTrackingReduction(chars);

            return await Task.FromResult($"Len: {result.Count}");
        }

        public override async Task<string> RunPart2()
        {
            var bestShortLength = int.MaxValue;
            var removed = "";
            for (int i = 'A'; i <= 'Z'; i++)
            {
                var j = i;
                var chars = Inputs.First().Where(f => f != (char)j && f != (char)(j + 32)).ToList();
                var result = BackTrackingReduction(chars);
                if (result.Count >= bestShortLength)
                    continue;

                bestShortLength = result.Count;
                removed = $"{(char)i}/{(char)(i + 32)}";
            }

            return await Task.FromResult($"Removed: {removed} | Len: {bestShortLength}");
        }

        private bool CharCompare(char a, char b)
        {
            return (a+32 == b || a-32 == b);
        }

        private List<char> BackTrackingReduction(List<char> chars)
        {
            for (var i = 0; i < chars.Count -1; i++)
            {
                if (!CharCompare(chars[i], chars[i + 1])) continue;

                chars.RemoveAt(i + 1);
                chars.RemoveAt(i);

                i = (i - 1 >= 0 ? i - 1 : 0) - 1;
            }

            return chars;
        }
    }
}