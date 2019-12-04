namespace AdventOfCode2019.Puzzles.Day04
{
    using System;
    using System.Threading.Tasks;
    using Base;

    public class Impl : BasePuzzle<int, int>
    {
        public Impl() : base("Day 04: Secure Container", ".\\Puzzles\\Day04\\Input.txt") { }

        public override async Task<int> RunPart1Async() => DoWork(CheckPartA);

        public override async Task<int> RunPart2Async() => DoWork(CheckPartB);

        public int DoWork(Func<int, int, int, int, int, int, bool> checkFunc)
        {
            var h = 0;
            for (var i = Inputs[0]; i <= Inputs[1]; i++)
            {
                var (a, b, c, d, e, f) = Split(i);
                if (checkFunc(a, b, c, d, e, f)) h++;
            }
            return h;
        }

        public bool CheckPartA(int a, int b, int c, int d, int e, int f) =>
            (a <= b && b <= c && c <= d && d <= e && e <= f) &&
            (a == b || b == c || c == d || d == e || e == f);

        public bool CheckPartB(int a, int b, int c, int d, int e, int f) =>
            (a <= b && b <= c && c <= d && d <= e && e <= f) &&
            (
                (a == b && b < c) ||
                (b == c && c < d && b > a) ||
                (c == d && d < e && c > b) ||
                (d == e && e < f && d > c) ||
                (e == f && e > d)
            );

        public (int a, int b, int c, int d, int e, int f) Split(int i)
        {
            var f = i % 10;
            var e = (i / 10) % 10;
            var d = (i / 100) % 10;
            var c = (i / 1000) % 10;
            var b = (i / 10000) % 10;
            var a = (i / 100000) % 10;

            return (a, b, c, d, e, f);
        }

        
    }
}
