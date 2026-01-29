namespace SumLargestNumbersInRange
{
    internal class Program
    {
        public static int largestNumber(int[] input1)
        {
            bool hasNegative = false;
            bool hasInvalid = false;

            HashSet<int> unique = new HashSet<int>();

            foreach (int x in input1)
            {
                if (x < 0) hasNegative = true;
                if (x == 0 || x > 100) hasInvalid = true;
                unique.Add(x);
            }

            if (hasNegative && hasInvalid) return -3;
            if (hasNegative) return -1;
            if (hasInvalid) return -2;

            int sum = 0;

            for (int start = 1; start <= 91; start += 10)
            {
                int end = start + 9;
                int max = -1;

                foreach (int x in unique)
                {
                    if (x >= start && x <= end && x > max)
                        max = x;
                }

                if (max != -1)
                    sum += max;
            }

            return sum;
        }



        static void Main(string[] args)
        {
            Console.Write("Enter n :: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter elements :: ");
            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
                arr[i] = int.Parse(Console.ReadLine());

            int result = largestNumber(arr);
            Console.WriteLine("output :: " + result);
        }
    }
}
