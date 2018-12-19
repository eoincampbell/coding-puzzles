namespace DailyCodingProblems.Puzzles.Day4
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 004") { }

        protected override async Task<string> ExecuteImpl()
        {
            var arr = new[] {3, 4, -1, 1};
            var found = int.MaxValue;
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] <= found && arr[i] > 0)
                {
                    found = arr[i] - 1;
                }
            }

            return await Task.FromResult($"{found}");
        }
    }
}
