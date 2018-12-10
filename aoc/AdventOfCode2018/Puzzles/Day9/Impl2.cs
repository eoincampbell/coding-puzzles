namespace AdventOfCode2018.Puzzles.Day9
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
        //public const string FILE = ".\\Puzzles\\Day9\\InputSimple.txt";
        public const string FILE = ".\\Puzzles\\Day9\\Input.txt";

        public Impl2() : base("Day 9b", FILE) { }

        public override async Task<string> RunPart1()
        {
            var score = Process(Inputs.First(), 1);
            return await Task.FromResult($"{score}");
        }

        public override async Task<string> RunPart2()
        {
            var score = Process(Inputs.First(), 100);
            return await Task.FromResult($"{score}");
        }

        private long Process(string input, int multi)
        {
            var (players, marbles) = GetInputs(input);
            marbles *= multi;
            var list = new LinkedList<long>();
            var dict = new Dictionary<int, long>();
            for (int p = 1; p <= players; p++) dict.Add(p, 0);

            for (int m = 0, p = 1; m <= marbles; m++, p = (p == players) ? 1 : p + 1)
            {
                if (m % 23 == 0 && m != 0)
                {
                    RotateList(list, -8);
                    var e = list.First.Value;
                    list.RemoveFirst();
                    var s = e + m;
                    RotateList(list, 1);
                    dict[p] += s;
                }
                else
                {
                    RotateList(list, 1);
                    list.AddLast(m);
                }
            }

            long bestScore = dict.Values.Max<long>();
            //Print(queue);
            //Console.WriteLine($"{players} players; last marble is worth {marbles} points: high score is {bestScore}"); 
            return bestScore;
        }

        private void RotateList(LinkedList<long> list, int v)
        {
            if(v > 0)
            {
                for(int i = 0; i < v && list.Any(); i++)
                {
                    var n = list.First.Value;
                    list.RemoveFirst();
                    list.AddLast(n);
                }
            }
            else if (v < 0)
            {
                
                for (int i = 0; i < Math.Abs(v); i++)
                {
                    var n = list.Last.Value;
                    list.RemoveLast();
                    list.AddFirst(n);
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