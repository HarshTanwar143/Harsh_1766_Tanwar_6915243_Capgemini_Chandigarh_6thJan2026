using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimeValidation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter time :: ");
            string str = Console.ReadLine();

            int ans = UserProgramCode.validateTime(str);

            if (ans == 1)
                Console.WriteLine("Valid time format");
            else if (ans == -1)
                Console.WriteLine("Invalid time format");

        }
    }
}
