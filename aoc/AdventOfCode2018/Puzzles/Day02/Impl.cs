namespace AdventOfCode2018.Puzzles.Day2
{
    using Base;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 2 ", ".\\Puzzles\\Day02\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            int twos = 0, threes = 0;
            foreach (var i in Inputs)
            {
                var grouping = i
                    .ToCharArray()
                    .GroupBy(c => c)
                    .Select(g => new {Letter = g.Key, Counter = g.Count()})
                    .ToList();

                twos += (grouping.Any(m => m.Counter == 2) ? 1 : 0);
                threes += (grouping.Any(m => m.Counter == 3) ? 1 : 0);
            }

            return await Task.FromResult((twos * threes).ToString());
        }

        public override async Task<string> RunPart2()
        {
            for (var i = 0; i < Inputs.Length; i++)
            {
                for (var j = 0; j < Inputs.Length; j++)
                {
                    if (i == j) continue;

                    var iChars = Inputs[i].ToCharArray();
                    var jChars = Inputs[j].ToCharArray();
                    var zipped = iChars
                        .Zip(jChars, (l, r) => new {Left = l, Right = r})
                        .ToList();

                    if (zipped.Count(r => r.Left != r.Right) == 1)
                    {
                        return new string(zipped
                            .Where(w => w.Left == w.Right)
                            .Select(s => s.Left)
                            .ToArray());
                    }
                }
            }

            return await Task.FromResult("No Result");
        }
    }
}