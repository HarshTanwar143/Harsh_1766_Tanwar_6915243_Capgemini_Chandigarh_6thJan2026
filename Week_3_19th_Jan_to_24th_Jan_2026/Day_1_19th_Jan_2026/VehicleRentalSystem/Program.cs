namespace VehicleRentalSystem
{
    internal class Program
    {
        static void Main()
        {
            Car car = new Car(1, "Honda City", 2000);
            Bike bike = new Bike(2, "Yamaha R15", 800);
            Truck truck = new Truck(3, "Tata Truck", 3500);

            Customer customer = new Customer(101, "Harsh");

            Console.WriteLine("Customer Details");
            Console.WriteLine("ID: " + customer.CustomerId);
            Console.WriteLine("Name: " + customer.Name);
            Console.WriteLine();

            Console.WriteLine("Vehicle Details");
            Console.WriteLine("Vehicle ID: " + car.VehicleId);
            Console.WriteLine("Model: " + car.Model);
            Console.WriteLine("Rate Per Day: " + car.RatePerDay);
            Console.WriteLine("Available: " + car.IsAvailable);
            Console.WriteLine();

            RentalTransaction rental = new RentalTransaction(car, customer, 3);

            Console.WriteLine("Rental Started");
            Console.WriteLine("Days Rented: 3");
            Console.WriteLine("Available After Rent: " + car.IsAvailable);
            Console.WriteLine("Total Charge: " + rental.CalculateCharge());
            Console.WriteLine();

            rental.CloseRental();

            Console.WriteLine("Vehicle Returned");
            Console.WriteLine("Available After Return: " + car.IsAvailable);
        }
    }
}
