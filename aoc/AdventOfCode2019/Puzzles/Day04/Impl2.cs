﻿/*
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

    public class Impl2 : Puzzle<int, int>
    {
        public Impl2() : base("Day 04: Secure Container (Ints - Optimized)", ".\\Puzzles\\Day04\\Input.txt") { }

        public override async Task<int> RunPart1Async() => await DoWork(CheckPartA);

        public override async Task<int> RunPart2Async() => await DoWork(CheckPartB);

        public async Task<int> DoWork(Func<int, int, int, int, int, int, bool> checkFunc) => await Task.Run(() => 
            GetRange(Inputs[0], Inputs[1])
                .Select(Split)
                .Count(x => checkFunc(x.a, x.b, x.c, x.d, x.e, x.f)));

        private static bool Check(int a, int b, int c, int d, int e, int f) 
            => (a <= b && b <= c && c <= d && d <= e && e <= f);

        private static bool CheckPartA(int a, int b, int c, int d, int e, int f)
            => Check(a,b,c,d,e,f) && (a == b || b == c || c == d || d == e || e == f);

        private static bool CheckPartB(int a, int b, int c, int d, int e, int f) 
            => Check(a,b,c,d,e,f) && 
            (
                (a == b && b < c) ||
                (b == c && c < d && b > a) ||
                (c == d && d < e && c > b) ||
                (d == e && e < f && d > c) ||
                (e == f          && e > d)
            );

        private static (int a, int b, int c, int d, int e, int f) Split(int i)
        {
            var f = i % 10;
            var e = (i / 10) % 10;
            var d = (i / 100) % 10;
            var c = (i / 1000) % 10;
            var b = (i / 10000) % 10;
            var a = (i / 100000) % 10;
            return (a, b, c, d, e, f);
        }

        public static IEnumerable<int> GetRange(int s, int e)
        {
            while (s <= e)
            {
                if (s / 100000 > s % 100000 / 10000)
                {
                    s += 10000;
                    s = (s / 10000) * 10000;
                }
                else if (s % 100000 / 10000 > s % 10000 / 1000)
                {
                    s += 1000;
                    s = (s / 1000) * 1000;
                }
                else if (s % 100000 % 10000 / 1000 > s % 1000 / 100)
                {
                    s += 100;
                    s = (s / 100) * 100;
                }
                else if (s % 100000 % 10000 % 1000 / 100 > s % 100 / 10)
                {
                    s += 10;
                    s = (s / 10) * 10;
                }
                else if (s % 100000 % 10000 % 1000 % 100 / 10 > s % 10)
                {
                    s++;
                }
                else
                {
                    yield return s;
                    s++;
                }
            }
        }
    }
}
