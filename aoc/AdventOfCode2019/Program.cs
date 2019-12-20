using AdventOfCode2019.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public static class Program
    {
        public static async Task Main() => await new Puzzles.Day17.Impl(true).RunBothPartsAsync();

        //public static async Task Main()
        //{
        //    foreach (var p in GetPuzzles())
        //        await p.RunBothPartsAsync();
        //}

        private static IEnumerable<IPuzzle> GetPuzzles() 
            => Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(Puzzle<,>))
            .OrderBy(o => o.Namespace)
            .ThenBy(n => n?.Name)
            .Select(Activator.CreateInstance)
            .Cast<IPuzzle>()
            .ToList();
    }
}
