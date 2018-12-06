namespace DailyCodingProblems.Puzzles.Day2
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Impl : BasePuzzle
    {
        public Impl() : base("Day 002") { }

        protected async override Task<string> ExecuteImpl()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5 };   //[120, 60, 40, 30, 24]
            var a2 = new int[] { 3, 2, 1 };         //[2, 3, 6]
            var a3 = new int[] { 1, 0, 4 };         //[0, 4, 0]
            var a4 = new int[] { 1, 0, 3, 0 };      //[0, 0, 0, 0]

            var r = ImplA(a1) + " " + ImplB(a1) + " " + ImplC(a1) + Environment.NewLine +
                "\t  " + ImplA(a2) + " " + ImplB(a2) + " " + ImplC(a2) + Environment.NewLine +
                "\t  " + ImplA(a3) + " " + ImplB(a3) + " " + ImplC(a3) + Environment.NewLine +
                "\t  " + ImplA(a4) + " " + ImplB(a4) + " " + ImplC(a4) + Environment.NewLine;

            return await Task.FromResult(r);
        }

        private string ImplA(int[] arr)
        {
            var arr2 = new int[arr.Length];

            var p = 1;
            bool f0 = false, s0 = false ;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    if (!f0) f0 = true;
                    else if (!s0)
                    {
                        s0 = true;
                        p = 0;
                        break;
                    }
                }
                else
                {
                    p *= arr[i];
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (f0)
                {
                    if (arr[i] == 0)
                        arr2[i] = p;
                    else
                        arr2[i] = 0;
                }
                else
                    arr2[i] = p / arr[i];
            }

            return $"[{string.Join(",", arr2)}]";
        }

        private string ImplB(int [] arr)
        {
            var arr2 = new int[arr.Length];
            //Without Division
            for (int i = 0; i < arr.Length; i++)
            {
                arr2[i] = 1;
                for (int j = 0; j < arr2.Length; j++)
                {
                    arr2[i] *= (j == i ? 1 : arr[j]);
                }
            }
            return $"[{string.Join(",", arr2)}]";
        }

        private string ImplC(int[] arr)
        { 
            var n = arr.Length;
            var arr2 = new int[n];

            var lArr = new int[n];
            lArr[0] = 1;

            var rArr = new int[n];
            rArr[n - 1] = 1;

            int lProd = 1, rProd = 1;

            for (int i = 1; i < n; i++)
            {
                lArr[i] = lProd *= arr[i-1];
            }

            for (int i = n-2; i >= 0; i--)
            {
                rArr[i] = rProd *= arr[i+1];
            }
            
            for (int i = 0; i < n; ++i) arr2[i] = lArr[i] * rArr[i];

            return $"[{string.Join(",", arr2)}]";
        }
    }
}
