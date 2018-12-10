namespace AdventOfCode2017.Puzzles.Day3
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 03 ", ".\\Puzzles\\Day03\\Input.txt") { }

        public override async Task<string> RunPart1()
        {
            var i = int.Parse(Inputs.First());
            int n = 1;
            double steps = 0;
            while (true)
            {
                var lvl = Math.Pow((n * 2) - 1, 2);                 //calc the bottom righ corner (2n-1)^2 = 1,9,25,49
                if (lvl > i)                                        //stop when your at a level that includes your input
                {
                    var prevLvl = Math.Pow(((n - 1) * 2) - 1, 2);   //find the total of the previous level
                    var diff = lvl - prevLvl;                       //calc the num cells on this level only
                    var pos = i - prevLvl;                          //find your cells position on this level
                    var sidePos = pos % (diff/4);                   //find your cells position on one of the 4 sides
                    var stcl = Math.Abs(sidePos - (diff / 8));      //count the steps back to the center line
                    steps = stcl + n - 1;                           //total steps = steps to center line, + level - offbyone 
                    break;
                }
                n++;
            }

            return await Task.FromResult($"{steps}");
        }

        public override async Task<string> RunPart2()
        {
            int dim = 11,
                start = (dim - 1) / 2,
                result = 0,
                i = int.Parse(Inputs.First());
            int[,] arr = new int[dim, dim];
            for(int level = 0; level < start; level++) //track the levels
            {
                if (level == 0) arr[start, start] = 1;
                else
                {
                    int y = start + level,
                        x = start + level,
                        len = level * 2;
                    
                    for (int k = 0; k < len; k++) arr[--x, y] = GetLookAbout(arr, x, y, i, ref result);
                    for (int k = 0; k < len; k++) arr[x, --y] = GetLookAbout(arr, x, y, i, ref result);
                    for (int k = 0; k < len; k++) arr[++x, y] = GetLookAbout(arr, x, y, i, ref result);
                    for (int k = 0; k < len; k++) arr[x, ++y] = GetLookAbout(arr, x, y, i, ref result);
                }
            }
            //Print(arr);
            return await Task.FromResult($"{result}");
        }

        public int GetLookAbout(int [,] arr, int x, int y, int input, ref int result)
        {
            var look = arr[x-1, y-1] + arr[x-1, y] + arr[x-1, y+1]
                 + arr[x, y-1] + arr[x, y+1]
                 + arr[x+1, y-1] + arr[x+1, y] + arr[x+1, y+1];

            if (result == 0 && look > input)
                result = look;

            return look;
        }

        public void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]:0000000} ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}