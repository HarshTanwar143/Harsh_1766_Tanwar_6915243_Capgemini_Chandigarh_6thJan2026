namespace Donation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of elements");
            int n = int.Parse(Console.ReadLine());

            string[] input1 = new string[n];

            Console.WriteLine("Enter the elements");
            for (int i = 0; i < n; i++)
            {
                input1[i] = Console.ReadLine();
            }

            Console.WriteLine("Enter location code");
            int input2 = int.Parse(Console.ReadLine());

            int result = UserProgramCode.getDonation(input1, input2);

            Console.WriteLine("Output");
            Console.WriteLine(result);
        }
    }
}
