namespace CountOfElements
{
    internal class Program
    {
        static int GetCount(int size, string[] arr, char ch)
        {
            int count = 0;
            ch = char.ToLower(ch);

            for (int i = 0; i < size; i++)
            {
                foreach (char c in arr[i])
                {
                    if (!char.IsLetter(c))
                        return -2;
                }

                if (char.ToLower(arr[i][0]) == ch)
                    count++;
            }

            if (count == 0)
                return -1;

            return count;
        }

        static void Main(string[] args)
        {
            Console.Write("Enter size of array: ");
            int n = int.Parse(Console.ReadLine());

            string[] arr = new string[n];
            Console.WriteLine("Enter array elements:");
            for (int i = 0; i < n; i++)
                arr[i] = Console.ReadLine();

            Console.Write("Enter character: ");
            char ch = char.Parse(Console.ReadLine());

            int result = GetCount(n, arr, ch);

            if (result == -1)
                Console.WriteLine("No elements Found");
            else if (result == -2)
                Console.WriteLine("Only alphabets should be given");
            else
                Console.WriteLine(result);
        }
    }
}
