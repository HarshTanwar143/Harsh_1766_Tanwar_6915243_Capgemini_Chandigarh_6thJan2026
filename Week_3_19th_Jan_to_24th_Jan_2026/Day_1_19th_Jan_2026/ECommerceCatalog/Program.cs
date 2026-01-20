namespace ECommerceCatalog
{
    internal class Program
    {
        static void Main()
        {
            Electronics e1 = new Electronics(1, "Laptop", 75000, 5);
            Clothing c1 = new Clothing(2, "Jacket", 3500, 10);
            Books b1 = new Books(3, "C# Guide", 1200, 20);

            Customer customer = new Customer(101, "Harsh");

            Console.WriteLine("Customer Details");
            Console.WriteLine("ID: " + customer.CustomerId);
            Console.WriteLine("Name: " + customer.Name);
            Console.WriteLine();

            customer.Cart.AddProduct(e1);
            customer.Cart.AddProduct(b1);

            Console.WriteLine("Cart Items");
            foreach (var p in customer.Cart.Products)
                Console.WriteLine(p.Name + " - " + p.Price);

            Order order = new Order(customer.Cart);

            Console.WriteLine();
            Console.WriteLine("Order Date: " + order.OrderDate);
            Console.WriteLine("Total Amount: " + order.GetOrderAmount());
        }
    }
}
