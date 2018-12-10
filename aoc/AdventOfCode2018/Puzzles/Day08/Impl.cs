namespace AdventOfCode2018.Puzzles.Day8
{
    using System;
    using Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        //public const string FILE = ".\\Puzzles\\Day08\\InputSimple.txt";
        public const string FILE = ".\\Puzzles\\Day08\\Input.txt";

        public Impl() : base("Day 8 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            var numbers = ProcessInputs(Inputs);

            var result = ProcessQueue(numbers, 0);

            return await Task.FromResult($"{result}");
        }

        public int ProcessQueue(Queue<int> q, int result)
        {
            int cCount = q.Dequeue(), mCount = q.Dequeue();     //Grab the headers
            for (int c = 0; c < cCount; c++)                    //foreach childnode recursively process node
                result = ProcessQueue(q, result);                   //update result
            for(int m = 0; m < mCount; m++)                     //foreach metadata increment result by amount
                result += q.Dequeue();
            return result;
        }

        public override async Task<string> RunPart2()
        {
            var numbers = ProcessInputs(Inputs);
            var result = ProcessQueueB(numbers);
            return await Task.FromResult($"{result}");
        }

        public int ProcessQueueB(Queue<int> q)
        {
            int cCount = q.Dequeue(), mCount = q.Dequeue(), thisNode = 0;     //Grab the headers
            
            if (cCount == 0)                                    //If just a metadata node
                for (int m = 0; m < mCount; m++)
                    thisNode += q.Dequeue();                    //calc just this nodes value
            else
            {
                var cNodeResults = new List<int>();             //if contains child nodes
                for (int c = 0; c < cCount; c++)
                    cNodeResults.Add(ProcessQueueB(q));         //recursively calc the child node value

                for (int m = 0; m < mCount; m++)
                {
                    var i = q.Dequeue();                        //foreach metadata value
                    if (i <= cNodeResults.Count)                //if it's a valid indexer
                        thisNode += cNodeResults[i - 1];        //use the child node at that index (offbyone)
                }
            }

            return thisNode;
        }

        private static Queue<int> ProcessInputs(IEnumerable<string> inputs)
        {
            return new Queue<int>(inputs.First().Split(' ').Select(int.Parse));
        }
    }
}