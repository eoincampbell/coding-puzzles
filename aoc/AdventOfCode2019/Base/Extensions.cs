using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Base
{
    public static class Extensions
    {
        public static int Pow(this int n, int exponent)
        {
            int ret = 1;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    ret *= n;
                n *= n;
                exponent >>= 1;
            }
            return ret;
        }
    }
}
