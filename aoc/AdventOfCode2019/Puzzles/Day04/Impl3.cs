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

        public override async Task<int> RunPart1Async() => DoWork(CheckPartA);

        public override async Task<int> RunPart2Async() => DoWork(CheckPartB);

        public int DoWork(Func<string, bool> checkFunc)
        {
            return GetRange(Inputs[0], Inputs[1]).Count(x => checkFunc(x.ToString()));
        }

        private static bool Check(string s) => 
            (s[0] <= s[1] && s[1] <= s[2] && s[2] <= s[3] && s[3] <= s[4] && s[4] <= s[5]);

        private static bool CheckPartA(string s) =>
            Check(s) && (s[0] == s[1] || s[1] == s[2] || s[2] == s[3] || s[3] == s[4] || s[4] == s[5]);

        private static bool CheckPartB(string s) =>
            Check(s) &&
            (
                (s[0] == s[1] && s[1] < s[2]) ||
                (s[1] == s[2] && s[2] < s[3] && s[1] > s[0]) ||
                (s[2] == s[3] && s[3] < s[4] && s[2] > s[1]) ||
                (s[3] == s[4] && s[4] < s[5] && s[3] > s[2]) ||
                (s[4] == s[5]                && s[4] > s[3])
            );

        public IEnumerable<int> GetRange(int s, int e)
        {
            while (s <= e)
            {
                var ss = s.ToString();
                if (ss[0] > ss[1]) s += 10000;
                else if (ss[1] > ss[2]) s += 1000;
                else if (ss[2] > ss[3]) s += 100;
                else if (ss[3] > ss[4]) s += 10;
                else if (ss[4] > ss[5]) s++;
                else
                {
                    yield return s;
                    s++;
                }
            }
        }
    }
}
