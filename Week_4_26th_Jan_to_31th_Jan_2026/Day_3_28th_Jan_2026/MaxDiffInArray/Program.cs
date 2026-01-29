namespace MaxDiffInArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of elements");
            int n = int.Parse(Console.ReadLine());

            int[] input1 = new int[n];

            Console.WriteLine("Enter the elements");
            for (int i = 0; i < n; i++)
            {
                input1[i] = int.Parse(Console.ReadLine());
            }

            int result = UserProgramCode.diffIntArray(input1);

            Console.WriteLine("Output : " + result);
            Console.ReadLine();
        }
    }
}
