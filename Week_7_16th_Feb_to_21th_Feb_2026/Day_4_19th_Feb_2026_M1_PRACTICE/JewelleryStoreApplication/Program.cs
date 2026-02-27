
namespace JewelleryStoreApplication
{

    public class Jewellery
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Material { get; set; }
        public int Price { get; set; }
    }

    public class JewelleryUtility
    {
        public void GetJewelleryDetails(Jewellery j)
        {
            Console.WriteLine($"Details: {j.Id} {j.Type} {j.Price}");
        }

        public void UpdateJewelleryPrice(Jewellery j, int newPrice)
        {
            j.Price = newPrice;
            Console.WriteLine($"Updated Price: {j.Price}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');

            Jewellery jewellery = new Jewellery
            {
                Id = input[0],
                Type = input[1],
                Price = int.Parse(input[2])
            };

            JewelleryUtility utility = new JewelleryUtility();

            while (true)
            {
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    utility.GetJewelleryDetails(jewellery);
                }
                else if (choice == 2)
                {
                    int newPrice = int.Parse(Console.ReadLine());
                    utility.UpdateJewelleryPrice(jewellery, newPrice);
                    utility.GetJewelleryDetails(jewellery);
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Thank You");
                    break;
                }
            }
        }
    }
}
