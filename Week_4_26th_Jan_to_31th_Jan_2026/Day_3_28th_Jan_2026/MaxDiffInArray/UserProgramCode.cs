using System;
using System.Collections.Generic;
using System.Text;

namespace MaxDiffInArray
{
    internal class UserProgramCode
    {
        public static int diffIntArray(int[] input1)
        {
            int n = input1.Length;

            if (n == 1 || n > 10)
                return -2;

            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < n; i++)
            {
                if (input1[i] < 0)
                    return -1;

                if (!set.Add(input1[i]))
                    return -3;
            }

            int minVal = input1[0];
            int maxDiff = input1[1] - input1[0];

            for (int i = 1; i < n; i++)
            {
                if (input1[i] - minVal > maxDiff)
                    maxDiff = input1[i] - minVal;

                if (input1[i] < minVal)
                    minVal = input1[i];
            }

            return maxDiff;
        }
    }
}
