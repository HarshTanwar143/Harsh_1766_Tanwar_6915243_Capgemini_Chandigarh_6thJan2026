using System.Text;


namespace StringCompression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the string :: ");
            string s = Console.ReadLine();
            StringBuilder result = new StringBuilder();

            int count = 1;

            for (int i = 1; i <= s.Length; i++)
            {
                if (i < s.Length && s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    result.Append(s[i - 1]);
                    result.Append(count);
                    count = 1;
                }
            }

            Console.WriteLine("Output :: " + result.ToString());
        }
    }
}
