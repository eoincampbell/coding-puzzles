namespace AdventOfCode2018.Puzzles.Day5
{
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl3 : BasePuzzle
    {
        public Impl3() : base("Day 5s", ".\\Puzzles\\Day5\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var chars = Inputs.First().ToCharArray().ToList();

            var result = StackReduction(chars);

            return await Task.FromResult($"Len: {result}");
        }

        public override async Task<string> RunPart2()
        {
            var bestShortLength = int.MaxValue;
            var removed = "";
            for (int i = 'A'; i <= 'Z'; i++)
            {
                var j = i;
                var chars = Inputs.First().Where(f => f != (char)j && f != (char)(j + 32)).ToList();
                var result = StackReduction(chars);
                if (result >= bestShortLength)
                    continue;

                bestShortLength = result;
                removed = $"{(char)i}/{(char)(i + 32)}";
            }

            return await Task.FromResult($"Removed: {removed} | Len: {bestShortLength}");
        }

        private int StackReduction(List<char> chars)
        {
            var stack = new Stack<char>();

            chars.ForEach(c =>
            {
                var tip = stack.FirstOrDefault();
                if (c + 32 == tip || c - 32 == tip)
                    stack.Pop();
                else
                    stack.Push(c);
            });

            return stack.Count;
        }
    }
}