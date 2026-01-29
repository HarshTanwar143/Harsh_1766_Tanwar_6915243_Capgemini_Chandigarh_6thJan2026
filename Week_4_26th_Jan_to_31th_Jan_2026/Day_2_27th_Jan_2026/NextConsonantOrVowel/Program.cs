namespace NextConsonantOrVowel
{
internal class Program
    {
        public static string nextString(string input1)
        {
            string vowels = "aeiou";
            string result = "";

            foreach (char ch in input1)
            {
                if (!char.IsLetter(ch))
                    return "Invalid input";

                bool isUpper = char.IsUpper(ch);
                char c = char.ToLower(ch);

                if (vowels.Contains(c))
                {
                    char next = (char)(c + 1);
                    while (vowels.Contains(next))
                        next++;

                    result += isUpper ? char.ToUpper(next) : next;
                }
                else
                {
                    char nextVowel = 'a';
                    foreach (char v in vowels)
                    {
                        if (v > c)
                        {
                            nextVowel = v;
                            break;
                        }
                    }
                    result += isUpper ? char.ToUpper(nextVowel) : nextVowel;
                }
            }

            return result;
        }


        static void Main(string[] args)
        {
            Console.Write("Enter input string :: ");
            string input = Console.ReadLine();
            Console.WriteLine("output :: " + nextString(input));
            Console.ReadLine();
        }
    }
}
