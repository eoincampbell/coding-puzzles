namespace AdventOfCode2018.Puzzles.Day3
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl2 : BasePuzzle
    {
        public Impl2() : base("Day 3b", ".\\Puzzles\\Day03\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var inputs = LoadInputs();
            var cloth = BuildClothMap(inputs);
            return await Task.FromResult(cloth.Count(c => c.Value > 1).ToString());
        }

        public override async Task<string> RunPart2()
        {
            var inputs = LoadInputs();
            var cloth = BuildClothMap(inputs);

            foreach (var ci in inputs)
            {
                for (var i = ci.FromLeft; i < ci.FromLeft + ci.Width; i++)
                {
                    for (var j = ci.FromTop; j < ci.FromTop + ci.Height; j++)
                    {
                        int hash = (i * 1000) + j;
                        if (cloth[hash] > 1) goto skip;
                    }
                }

                return $"{ci.Id}";

                skip:;
            }
            
            return await Task.FromResult("No Result");
        }


        private static Dictionary<int, int> BuildClothMap(IEnumerable<ClothInput> inputs)
        {
            Dictionary<int, int> cloth = new Dictionary<int, int>(); //<hash, count>

            foreach (var ci in inputs)
            {
                for (var i = ci.FromLeft; i < ci.FromLeft + ci.Width; i++)
                {
                    for (var j = ci.FromTop; j < ci.FromTop + ci.Height; j++)
                    {
                        //max width = 1000;
                        int hash = (i * 1000) + j;
                        if (cloth.ContainsKey(hash))
                        {
                            cloth[hash]++;
                        }
                        else
                        {
                            cloth.Add(hash, 1);
                        }
                    }
                }
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