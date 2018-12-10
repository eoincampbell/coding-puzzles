namespace AdventOfCode2018.Puzzles.Day9
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
        public const string FILE = ".\\Puzzles\\Day9\\InputSimple.txt";
        //public const string FILE = ".\\Puzzles\\Day9\\Input.txt";

        public Impl() : base("Day 9 ", FILE) { }

        public override async Task<string> RunPart1()
        {
            var lastScore = 0;
            foreach (var input in Inputs)
            {
                lastScore = Process(input, 1);
            }
            return await Task.FromResult($"{lastScore}");
        }

        public override async Task<string> RunPart2()
        {
            var lastScore = 0;
            foreach (var input in Inputs)
            {
                lastScore = Process(input, 100);
            }
            return await Task.FromResult($"{lastScore}");
        }


        private int Process(string input, int multi)
        {
            var (players, marbles) = GetInputs(input);
            marbles *= multi;
            var queue = new Queue<int>();
            var dict = new Dictionary<int, int>();
            for (int p = 1; p <= players; p++) dict.Add(p, 0);

            for (int m = 0, p = 1; m <= marbles; m++, p = (p == players) ? 1 : p + 1)
            {
                if (m % 23 == 0 && m != 0)
                {
                    RotateQueue(queue, -8);
                    var e = queue.Dequeue();
                    var s = e + m;
                    RotateQueue(queue, 1);
                    dict[p] += s;
                }
                else
                {
                    RotateQueue(queue, 1);
                    queue.Enqueue(m);
                }
            }

            int bestScore = dict.Values.Max();
            //Print(queue);
            //Console.WriteLine($"{players} players; last marble is worth {marbles} points: high score is {bestScore}"); 
            return bestScore;
        }

        private void RotateQueue(Queue<int> queue, int v)
        {
            if(v > 0)
            {
                for(int i = 0; i < v && queue.Any(); i++)
                {
                    var e = queue.Dequeue();
                    queue.Enqueue(e);
                }
            }
            else if (v < 0)
            {
                int loop = queue.Count + v;
                for (int i = 0; i < loop; i++)
                {
                    var e = queue.Dequeue();
                    queue.Enqueue(e);
                }
            }
        }

        public (int,int) GetInputs(string input)
        {
            Regex r = new Regex(@"(\d+) players; last marble is worth (\d+) points");

            var m = r.Match(input);

            return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
        }

        public void Print(Queue<int> list)
        {
            foreach(var l in list)
            {
                Console.Write($"{l:00} ");
            }
            Console.WriteLine("");
        }
    }
}