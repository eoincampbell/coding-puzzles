namespace AdventOfCode2018.Puzzles.Day8
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl2 : BasePuzzle
    {
        //public const string FILE = ".\\Puzzles\\Day08\\InputSimple.txt";
        public const string FILE = ".\\Puzzles\\Day08\\Input.txt";

        public Impl2() : base("Day 8b", FILE) { }

        public override async Task<string> RunPart1()
        {
            int part1 = 0;
            ProcessQueue(Numbers, ref part1, out int part2);
            return await Task.FromResult($"{part1}");
        }

        public override async Task<string> RunPart2()
        {
            int part1 = 0;
            ProcessQueue(Numbers, ref part1, out int part2);
            return await Task.FromResult($"{part2}");
        }

        public void ProcessQueue(Queue<int> q, ref int part1, out int part2)
        {
            int cCount = q.Dequeue(), mCount = q.Dequeue(), thisN = 0;     //Grab the headers
            var cNodes = new List<int>();                   
            for (int c = 0; c < cCount; c++)
            {
                ProcessQueue(q, ref part1, out int b);              //recursively calc the child node values
                cNodes.Add(b);                                      //stash part b for later indexing
            }

            for (int m = 0; m < mCount; m++)                        //foreach metadata value
            {
                var md = q.Dequeue();                               
                thisN += (cCount == 0) ? md : 0;                    //no child nodes? increment this node by the amount
                thisN += (md <= cNodes.Count) ? cNodes[md - 1] : 0; //Or use the child node value at that indexer-1
                part1 += md;                                        //part1 is always accumulating
            }
            part2 = thisN;                                          //part 2 is only ever the value of this node down
        }

        public Queue<int> Numbers => new Queue<int>(Inputs.First().Split(' ').Select(int.Parse));
    }
}