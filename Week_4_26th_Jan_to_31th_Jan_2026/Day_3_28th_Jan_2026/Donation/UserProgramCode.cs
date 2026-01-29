using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Donation
{
    internal class UserProgramCode
    {
        public static int getDonation(string[] input1, int input2)
        {
            HashSet<string> set = new HashSet<string>();

            foreach (string s in input1)
            {
                if (!s.All(char.IsDigit))
                    return -2;

                if (!set.Add(s))
                    return -1;
            }

            int sum = 0;
            string loc = input2.ToString();

            foreach (string s in input1)
            {
                string locationCode = s.Substring(3, 3);

                if (locationCode == loc)
                {
                    int donation = int.Parse(s.Substring(6));
                    sum += donation;
                }
            }

            return sum;
        }
    }
}
