namespace Is_IsNot_String
{
    internal class Program
    {
        static string negativeString(string value)
        {
            string result = "";
            int i = 0;
            while(i < value.Length)
            {
                if(i + 1 < value.Length && value[i] == 'i' && value[i+1] == 's')
                {
                    bool checkLeft = (i == 0 || !char.IsLetter(value[i - 1]));
                    bool checkRight = (i + 2 == value.Length || !char.IsLetter(value[i + 2]));

                    if(checkLeft && checkRight)
                    {
                        result += "is not";
                        i += 2;
                    }
                    else
                    {
                        result += value[i];
                        i++;
                    }
                }
                else
                {
                    result += value[i];
                    i++;
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter the string :: ");
            string input = Console.ReadLine();

            string output = negativeString(input);

            Console.WriteLine("Output: " + output);
        }
    }
}
