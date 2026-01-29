using System;
using System.Collections.Generic;
using System.Text;

namespace ListTheElementsA
{
    internal class UserProgramCode
    {
        public static List<int> GetElements(List<int> input1, int input2)
        {
            List<int> result = input1.Where(x => x < input2)
                                      .OrderByDescending(x => x)
                                      .ToList();

            if (result.Count == 0)
                return new List<int> { -1 };

            return result;
        }
    }
}
