namespace AdventOfCode2018.Puzzles.Day11
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
        //public const string FILE = ".\\Puzzles\\Day11\\InputSimple.txt";
        //public const string FILE = ".\\Puzzles\\Day11\\InputVas.txt";
        public const string FILE = ".\\Puzzles\\Day11\\Input.txt";

        public Impl() : base("Day11 ", FILE) { }
        
        public override async Task<string> RunPart1()
        {
            var serial = GetSerialNumber(Inputs);
            var arr = GenerateArray(serial);
            var (bestX, bestY, bestSize) = FindBestSize(arr, 3, 3);
            return await Task.FromResult($"{bestX},{bestY}");
        }

        public override async Task<string> RunPart2()
        {
            var serial = GetSerialNumber(Inputs);
            var arr = GenerateArray(serial);
            var (bestX, bestY, bestSize) = FindBestSize(arr, 1, 300);
            return await Task.FromResult($"{bestX},{bestY},{bestSize}");
        }
        
        public long[,] GenerateArray(int serial)    //this builds the array as per https://en.wikipedia.org/wiki/Summed-area_table
        {
            var arr = new long[300, 300];
            for (var x = 1; x <= arr.GetLength(0); x++)                     
            {
                for (var y = 1; y <= arr.GetLength(1); y++)
                {
                    long ox = x - 1, oy = y - 1, p = CalcPower(x, y, serial);       //offset the indices and calc power for this cell
                    if (ox == 0 && oy == 0) arr[ox, oy] = p;                        //if were on the top left the value is just that
                    else if (ox == 0) arr[ox, oy] = p + arr[ox, oy - 1];            //if were on the top horizontal it's ME + the previous on this line
                    else if (oy == 0) arr[ox, oy] = p + arr[ox - 1, oy];            //if we're on the left vertical it's ME + the previous above me
                    else arr[ox, oy] = p + arr[ox - 1, oy] + arr[ox, oy - 1] - arr[ox - 1, oy - 1]; 
                    //else it's me + previous on this line + previous above - the one diagonally up and over
                }
            }
            return arr;
        }
        
        public (long, long, long) FindBestSize(long[,] arr, int start, int end)
        {
            long bestSoFar = 0, bestX = 0, bestY = 0, bestSize = 0, 
                xn = arr.GetLength(0), yn = arr.GetLength(1);
            for (var size = start; size <= end; size++)                             //foreach size
            {
                for (var x = 1; x <= xn - (size - 1); x++)                          //foreach x up to the point where the size wont fit
                {
                    for (var y = 1;y <= yn - (size - 1); y++)                       //foreach x up to the point where the size wont fit
                    {
                        var cellPower = GetPoweCellSumAreaTable(x, y, size, arr);   //work out the sum for this square

                        if (cellPower <= bestSoFar) continue; //skip while no improvement found

                        bestSoFar = cellPower;
                        bestX = x;
                        bestY = y;
                        bestSize = size;
                    }
                }
            }
            return (bestX, bestY, bestSize);
        }

        public long CalcPower(long x, long y, long serialNumber)
        {
            long rackId = x + 10, power = rackId * y;
            power += serialNumber;
            power *= rackId;
            power /= 100;
            power %= 10;
            power -= 5;

            return power;
        }

        public long GetPoweCellSumAreaTable(int x, int y, int size, long[,] arr)
        {
            int bx = x - 1, by = y - 1, s = size - 1;                       //offset the x,y,size so they're easier to work with
                                                                            //see this image: https://en.wikipedia.org/wiki/Summed-area_table#/media/File:Integral_image_application_example.svg
            var topLeft = (bx == 0 || by == 0) ? 0 : arr[bx - 1, by - 1];   //this one up and over from the start of your square
            var bottomRight = arr[bx + s, by + s];                          //this is the lower right cell of the square
            var bottomleft = (bx == 0) ? 0 : arr[bx - 1, by + s];           //this is one left of the bottomleft of the square
            var topRight = (by == 0) ? 0 : arr[bx + s, by - 1];             //this is one up from the topright of the square

            var sum = topLeft + bottomRight - bottomleft - topRight;

            return sum;
        }

        public int GetSerialNumber(IEnumerable<string> inputs)
        {
            return int.Parse(inputs.First());
        }
    }
}