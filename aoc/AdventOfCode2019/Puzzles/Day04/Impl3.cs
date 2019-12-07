/*
 * Day 04: Secure Container
 * https://adventofcode.com/2019/day/4
 * Part 1: 1653
 * Part 2: 1133
 */
 namespace AdventOfCode2019.Puzzles.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl3 : BasePuzzle<int, int>
    {
        public Impl3() : base("Day 04: Secure Container (Strings - Optimized)", ".\\Puzzles\\Day04\\Input.txt") { }

        public override async Task<int> RunPart1Async() => await DoWork(CheckPartA);

        public override async Task<int> RunPart2Async() => await DoWork(CheckPartB);

        private async Task<int> DoWork(Func<string, bool> checkFunc) =>
            await Task.Run(() => GetRange(Inputs[0], Inputs[1])
            .Count(x => checkFunc(x.ToString())));
        
        private static bool Check(string s) 
            => (s[0] <= s[1] && s[1] <= s[2] && s[2] <= s[3] && 
             s[3] <= s[4] && s[4] <= s[5]);

        private static bool CheckPartA(string s) 
            => Check(s) && 
            (s[0] == s[1] || s[1] == s[2] || s[2] == s[3] || 
             s[3] == s[4] || s[4] == s[5]);

        private static bool CheckPartB(string s) 
            => Check(s) &&
            (
                                (s[0] == s[1] && s[1] < s[2]) ||
                (s[1] > s[0] && s[1] == s[2] && s[2] < s[3]) ||
                (s[2] > s[1] && s[2] == s[3] && s[3] < s[4]) ||
                (s[3] > s[2] && s[3] == s[4] && s[4] < s[5]) ||
                (s[4] > s[3] && s[4] == s[5])
            );

        private static IEnumerable<int> GetRange(int s, int e)
        {
            while (s <= e)
            {
                var ss = $"{s}";
                if      (ss[0] > ss[1])     s = ((s += 10000) / 10000) * 10000;
                else if (ss[1] > ss[2])     s = ((s += 1000) / 1000) * 1000;
                else if (ss[2] > ss[3])     s = ((s += 100) / 100) * 100;
                else if (ss[3] > ss[4])     s = ((s += 10) / 10) * 10;
                else if (ss[4] > ss[5])     s++;
                else                        yield return s++;
            }
        }

        //private static IEnumerable<int> GetRange2(int s, int e)
        //{
        //    while (s <= e)
        //    {
        //        var ss = $"{s}";
        //        for (int i = 0, j = PasswordLength - 2, p = 10.Pow(PasswordLength - 2);
        //                 i < PasswordLength - 1;
        //                 i++, j--, p = 10.Pow(j))
        //        {
        //            if (ss[i] <= ss[i + 1]) continue;
        //            s = ((s += p) / p) * p;
        //            goto skip;
        //        }
        //        yield return s++;
        //    skip:;
        //    }
        //}
    }
}
