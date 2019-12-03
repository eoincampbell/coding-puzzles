namespace AdventOfCode2019.Puzzles.Day03
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Dict = System.Collections.Generic.Dictionary<(int x, int y), int>;

    public class Impl : BasePuzzle<string, int>
    {
        public Impl() : base("Day 03: ", ".\\Puzzles\\Day03\\Input.txt") { }

        public (Dict firstWire, Dict secondWire) GetWires(string a, string b) => (GetWire(a), GetWire(b));

        public (char dir, int dist) Parse(string i) => (i[0], int.Parse(i[1..]));

        public (int x, int y) GetPoint(char dir, ref int x, ref int y)
        {
            return dir switch
                {
                    'R' => (++x, y),
                    'U' => (x, ++y),
                    'L' => (--x, y),
                    'D' => (x, --y),
                    _ => throw new NotSupportedException()
                };
        }

        public Dict GetWire(string input)
        {
            var inst = input.Split(',').Select(Parse);
            int x = 0, y = 0, d = 0;
            var dict = new Dict();
            foreach (var (dir, dist) in inst)
                for (var l = 0; l < dist; l++)
                    dict.TryAdd(GetPoint(dir, ref x, ref y), ++d);
                
            return dict;
        }

        public override async Task<int> RunPart1Async()
        {
            var w = GetWires(Inputs[0], Inputs[1]);
            //399
            return w.firstWire.Keys
                .Intersect(w.secondWire.Keys)
                .Min(p => Math.Abs(p.x) + Math.Abs(p.y));
        }

        public override async Task<int> RunPart2Async()
        {
            var w = GetWires(Inputs[0], Inputs[1]);
            //15678
            return w.firstWire.Keys
                .Intersect(w.secondWire.Keys)
                .Min(key => w.firstWire[key] + w.secondWire[key]);
        }
    }
}
