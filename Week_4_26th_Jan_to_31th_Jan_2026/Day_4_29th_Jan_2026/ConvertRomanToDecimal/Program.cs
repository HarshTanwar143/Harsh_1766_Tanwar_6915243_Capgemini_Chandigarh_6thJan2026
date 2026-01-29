namespace ConvertRomanToDecimal
{
    internal class Program
    {
       
        static int romanToDecimal(string input)
        {
            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int curr = getValue(input[i]);
                if (curr == -1) return -1;

                if (i + 1 < input.Length)
                {
                    int next = getValue(input[i + 1]);
                    if (next == -1) return -1;

                    if (curr < next)
                        sum -= curr;
                    else
                        sum += curr;
                }
                else
                {
                    sum += curr;
                }
            }

            return sum;
        }

        static int getValue(char ch)
        {
            switch (ch)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
                default: return -1;
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Enter Roman number: ");
            string input = Console.ReadLine();

            int result = romanToDecimal(input);

            Console.WriteLine("Output: " + result);
        }
    }
}
