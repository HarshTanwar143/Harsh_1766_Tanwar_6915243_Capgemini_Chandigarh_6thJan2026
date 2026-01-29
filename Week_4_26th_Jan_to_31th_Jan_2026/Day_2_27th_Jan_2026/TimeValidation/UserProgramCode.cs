using System;
using System.Collections.Generic;
using System.Text;

namespace TimeValidation
{
    internal class UserProgramCode
    {
        public static int validateTime(string str)
        {


            int hr, min;

            hr = int.Parse(str.Substring(0, 2));
            min = int.Parse(str.Substring(3, 2));
            string suf = str.Substring(5, 3);


            if (hr > 12 || min > 60 || suf != " am" && suf != " pm")
                return -1;
            else
                return 1;


        }

    }
}
