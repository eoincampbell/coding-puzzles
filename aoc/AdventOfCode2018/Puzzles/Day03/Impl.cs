namespace AdventOfCode2018.Puzzles.Day3
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 3 ", ".\\Puzzles\\Day03\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var inputs = LoadInputs();
            var cloth = BuildClothMap(inputs);

            //Fast 2d->1d block copy
            var tmp = new int[cloth.GetLength(0) * cloth.GetLength(1)];
            Buffer.BlockCopy(cloth, 0, tmp, 0, tmp.Length * sizeof(int));
            return await Task.FromResult(tmp.Count(c => c > 1).ToString());
        }

        public override async Task<string> RunPart2()
        {
            var inputs = LoadInputs();
            var cloth = BuildClothMap(inputs);

            foreach (var ci in inputs)
            {
                var counter = 0;

                for (var i = ci.FromLeft; i < ci.FromLeft + ci.Width; i++)
                    for (var j = ci.FromTop; j < ci.FromTop + ci.Height; j++)
                        counter += cloth[i, j];
                
                if (counter == (ci.Width * ci.Height))
                    return ci.Id.ToString();
            }

            return await Task.FromResult("No Result");
        }


        private static int[,] BuildClothMap(IEnumerable<ClothInput> inputs)
        {
            var cloth = new int[1000, 1000];

            foreach (var ci in inputs)
            {
                for (var i = ci.FromLeft; i < ci.FromLeft + ci.Width; i++)
                    for (var j = ci.FromTop; j < ci.FromTop + ci.Height; j++)
                        cloth[i, j] += 1;
            }

            return cloth;
        }

        private List<ClothInput> LoadInputs()
        {
            return Inputs
                .Select(item => Pattern.Match(item))
                .Where(m => m.Success)
                .Select(match => new ClothInput
                {
                    Id = int.Parse(match.Groups[1].Value),
                    FromLeft = int.Parse(match.Groups[2].Value),
                    FromTop = int.Parse(match.Groups[3].Value),
                    Width = int.Parse(match.Groups[4].Value),
                    Height = int.Parse(match.Groups[5].Value)
                })
                .ToList();
        }
        
        private static readonly Regex Pattern = new Regex(@"^#(\d{0,4}) @ (\d{0,4}),(\d{0,4}): (\d{0,4})x(\d{0,4})$");

        private class ClothInput
        {
            public int Id { get; set; }
            public int FromLeft { get; set; }
            public int FromTop { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public override string ToString()
            {
                return $"#{Id} @ {FromLeft},{FromTop}: {Width}x{Height}";
            }
        }
    }
}