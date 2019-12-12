/*
 * Day 02: 1202 Program Alarm
 * https://adventofcode.com/2019/day/2
 * Part 1: 4462686
 * Part 2: 5936
 */

namespace AdventOfCode2019.Puzzles.Day02
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;

    public class Impl : Puzzle<string,int>
    {
        public Impl() : base("Day 02: 1202 Program Alarm", ".\\Puzzles\\Day02\\Input.txt") {}
        
        public override async Task<int> RunPart1Async() 
            => await RunIntCode(GetIntCode(12,2));

        public override async Task<int> RunPart2Async()
        {
            for(var noun = 0; noun <= 99; noun++)
                for(var verb = 0; verb <= 99; verb++)
                    if (await RunIntCode(GetIntCode(noun, verb)) == 19690720)
                        return (noun * 100) + verb;
            
            return 0;
        }

        public int[] GetIntCode(int noun, int verb)
        {
            var arr = Array.ConvertAll(Inputs[0].Split(','), int.Parse);
            arr[1] = noun;
            arr[2] = verb;
            return arr;
        }

        public async static Task<int> RunIntCode(int[] c, int i = 0)
            => await Task.Run(() =>
            {
                if (c == null || !c.Any()) 
                    throw new ArgumentNullException(nameof(c));

                while (c[i] != 99)
                {
                    c[c[i + 3]] = c[i] switch
                        {
                            1 => c[c[i + 1]] + c[c[i + 2]],
                            2 => c[c[i + 1]] * c[c[i + 2]],
                            _ => throw new NotSupportedException()
                        };
                    i += 4;
                }
            
                return c[0];
            });
    }
}
