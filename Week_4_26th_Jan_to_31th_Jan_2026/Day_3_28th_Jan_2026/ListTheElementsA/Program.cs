namespace ListTheElementsA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of elements");
            int n = int.Parse(Console.ReadLine());

            List<int> list = new List<int>();

            Console.WriteLine("Enter the elements");
            for (int i = 0; i < n; i++)
            {
                list.Add(int.Parse(Console.ReadLine()));
            }

            Console.WriteLine("Enter the value");
            int input2 = int.Parse(Console.ReadLine());

            List<int> result = UserProgramCode.GetElements(list, input2);

            Console.WriteLine("Output");
            if (result.Count == 1 && result[0] == -1)
            {
                Console.WriteLine("No element found");
            }
            else
            {
                Console.WriteLine(string.Join(" ", result));
            }
        }
    }
}
