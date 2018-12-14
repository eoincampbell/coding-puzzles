namespace AdventOfCode2018.Puzzles.Day14
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CodingPuzzles.Helpers;

    public class Impl : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day14\\Input.txt";

        public Impl() : base("Day14 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            int elfA = 0, elfB = 1, n = 10, i = 765071, t = i + n;
            var recipes = new List<int> { 3, 7 };
            
            while (recipes.Count() < t)
            {
                int rA = recipes[elfA], rB = recipes[elfB];
                recipes.AddRange((rA + rB).ToDigitArray());
                elfA = (elfA + 1 + rA) % recipes.Count;
                elfB = (elfB + 1 + rB) % recipes.Count;
            }

            var result = recipes.Skip(i).Take(n).ToList().ToS();
            return await Task.FromResult($"{result}");
        }


        private int _part2 = 0;
        public override async Task<string> RunPart2()
        {
            //int elfA = 0, elfB = 1, n = 10, i = 59414, t = i + n;
            int elfA = 0, elfB = 1, n = 10, i = 765071, t = i + n;
            var recipes = new List<int> { 3, 7 };
            var stack = new Stack<int>(recipes);
            var comp = i.ToDigitArray();
            while (true)
            {
                int rA = recipes[elfA], rB = recipes[elfB];
                int[] arr = (rA + rB).ToDigitArray();
                foreach(var a in arr)
                {
                    recipes.Add(a);
                    stack.Push(a);
                    if (!CheckEnd(stack, comp)) continue;
                    _part2 = recipes.Count - comp.Length;
                    goto exit;
                }
                elfA = (elfA + 1 + rA) % recipes.Count;
                elfB = (elfB + 1 + rB) % recipes.Count;
            }
exit:            
            return await Task.FromResult($"{_part2}");
        }

        private bool CheckEnd(Stack<int> stack, int[] comparison)
        {
            int ll = stack.Count, cl = comparison.Length;
            if (cl > ll) return false;
            return stack.Take(cl).ToArray().Reverse().SequenceEqual(comparison);
        }

        public void Print(List<int> list, int a, int b)
        {
            for (int i = 0; i < list.Count(); i++)
                if (i == a) Console.Write($"({list[i]}) ");
                else if (i == b) Console.Write($"[{list[i]}] ");
                else Console.Write($"{list[i]} ");

            Console.WriteLine();
        }
    }
}