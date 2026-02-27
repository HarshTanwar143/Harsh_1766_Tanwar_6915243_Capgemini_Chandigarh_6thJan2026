using System.Text.RegularExpressions;

namespace ProductCodeValidation
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] arr = new string[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = Console.ReadLine();
            }

            int[] result = stringVal(arr);

            foreach (int val in result)
            {
                Console.WriteLine(val);
            }
        }

        public static int[] stringVal(string[] arr)
        {
            int[] result = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                string s = arr[i];

                if (s.Length >= 3 &&
                    char.IsLetter(s[0]) &&
                    char.IsDigit(s[s.Length - 1]))
                {
                    result[i] = 1;
                }
                else
                {
                    result[i] = 0;
                }
            }

            return result;
        }
    }
}
