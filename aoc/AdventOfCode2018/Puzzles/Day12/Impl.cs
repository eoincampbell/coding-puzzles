namespace AdventOfCode2018.Puzzles.Day12
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public const string FILE = ".\\Puzzles\\Day12\\Input.txt";
        //public const string FILE = ".\\Puzzles\\Day12\\InputSimple.txt";

        private long _part1 = 0;
        private long _part2 = 0;
        public Impl() : base("Day12 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            int generations = 300, stableCounter = 0, stableValue = 0;
            var (state, hash) = GetState(Inputs, generations);
            int total = state.Where(w => w.V == '#').Sum(s => s.Ix);

            using (StreamWriter sw = new StreamWriter(".\\Day12.txt")) 
            {
                PrintHeader(state, sw);
                Print(state, 0, total, sw);
                for (int i = 1; i <= generations; i++)
                {
                    //Calculate the new state value
                    var newState = new Pot[state.Length];                   //in each gen, make a new state holder
                    newState[0] = state[0];                                 //i noticed in my input ..... => . so I'm assuming that's true for all 
                    newState[1] = state[1];                                 //and growth can spring up at random in the outer buffers i've created 
                    newState[state.Length - 1] = state[state.Length - 1];   //around the initial set of pots
                    newState[state.Length - 2] = state[state.Length - 2];   //so I can copy the first 2 and last 2 verbatim as these aren't calculable

                    for (int j = 2; j < state.Length - 2; j++)              //for each calculable value
                    {
                        var segment = state.Skip(j - 2).Take(5)
                            .Select(s => s.V).ToS();                         //at each calculable pot, grab a 5 element segment around it
                        newState[j] = state[j];                             //copy the state down to the new state
                        newState[j].V = hash.Contains(segment) ? '#' : '.'; //check if that segment results in a growth
                    }

                    state = newState;

                    //Calculate the new row value, and compare it to last time.
                    var newTotal = state.Where(w => w.V == '#').Sum(s => s.Ix);

                    if (newTotal - total == stableValue) stableCounter++;   //Keep count where the row value stops changing
                    else
                    {
                        stableValue = newTotal - total;                     //Or reset
                        stableCounter = 0;
                    }
                    total = newTotal;                                       //Prep for next round
                    Print(state, i, total, sw);

                    if(i == 20) _part1 = total;                             //Capture answer 1 at gen 20

                    if (stableCounter < 50) continue;
                    long targetGeneration = 50000000000;                    //otherwise calc answer 2 and break
                    _part2 = total + ((targetGeneration - i) * stableValue);
                    break;
                }

                return await Task.FromResult($"{_part1}");
            }
        }
        
        public override async Task<string> RunPart2()
        {
            //didn't dawn on me to check for a pattern until i saw what some other 
            //people were suggesting on the reddit thread
            
            //I ended up generating a print visualisation of this in part 1.
            //After about 150 generations it stablises the pattern moving left to right
            //For my input, once it hits a stabilisation point, it grows by 33 each generation.
            long inc = 33;
            //so I cherry picked a position where it was stable.
            long generation = 168;
            long value = 5599;
            //and then can figure out the value at the target generation
            long targetGeneration = 50000000000;
            long result = value + ((targetGeneration - generation) * inc);

            return await Task.FromResult($"{result} {_part2}");
        }


        public void PrintHeader(Pot[] state, StreamWriter sw)
        {
            sw.Write("              ");
            for (int i = state.Min(m => m.Ix); i <= state.Max(m => m.Ix); i++)
                sw.Write(Math.Abs(i % 10));
            sw.WriteLine("");
        }

        public void Print(Pot[] state, int generation, int total, StreamWriter sw)
        {
            sw.Write($"{generation:00000}: {total:00000}: ");
            foreach (var s in state) sw.Write(s);
            sw.WriteLine("");
        }

        public (Pot [], HashSet<string>) GetState(IEnumerable<string> inputs, int buf)
        {
            //For my input set it never expanded backwards more thana few elements so I capped it at 50;
            var b = new List<Pot>();
            b.AddRange(Enumerable.Range(-50, 50).Select(i => new Pot { Ix = i, V = '.' }));

            //Grab all the inputs states and then create indices 0..Len
            int a = -1;
            var p = inputs
                .First()
                .Replace("initial state: ", "")
                .ToCharArray()
                .Select(c => new Pot { Ix = ++a, V = c })
                .ToList();
            b.AddRange(p);

            //Further pad the collection with enough space to grow by Generations + 2;
            b.AddRange(Enumerable.Range(p.Count(), buf + 2).Select(i => new Pot { Ix = i, V = '.' }));
           
            //Grab all the configurations that lead to a successful pot growth
            //Can calc the others by omission/default
            var h = new HashSet<string>();

            inputs.Skip(2)
                .Where(r => r[9] == '#')
                .ToList()
                .ForEach(f => h.Add(f.Substring(0, 5)));
                
            return (b.ToArray(), h);                
        }

        public struct Pot
        {
            public int Ix;              //Index
            public char V;              //Value
            public bool G => V == '#';  //Growing
            public override string ToString() => V.ToString();
        }
    }
}