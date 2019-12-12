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
        public static async Task Main()
        {
            await new Puzzles.Day11.Impl().RunBothPartsAsync();
            
            foreach (var puzzle in GetPuzzles()) 
                await puzzle.RunBothPartsAsync();
        }

        private static IEnumerable<IPuzzle> GetPuzzles()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(Puzzle<,>))
                .OrderBy(o => o.Namespace)
                .ThenBy(n => n.Name)
                .Select(s => Activator.CreateInstance(s) as IPuzzle);
        }
    }
}
