namespace DeviceProject
{
    internal class Device
    {
        public enum DeviceCategory
        {
            Laptop,
            Tablet,
            Mobile
        }

        public struct Specification
        {
            public int Ram;
            public int Storage;
            public int Ports;
        }

        public const string Company = "TechWorld";
        public static int DeviceCount;
        public readonly string serialNumber;

        public int price;
        public string brand;
        public string model;

        static Device()
        {
            DeviceCount = 0;
        }

        public Device()
        {
            DeviceCount++;
        }

        public Device(int price, string brand, string model, string serialNumber)
        {
            this.price = price;
            this.brand = brand;
            this.model = model;
            this.serialNumber = serialNumber;
            DeviceCount++;
        }

        public Device(Device obj)
        {
            price = obj.price;
            brand = obj.brand;
            model = obj.model;
            serialNumber = obj.serialNumber;
            DeviceCount++;
        }

        public void setDetails()
        {
            Console.Write("Enter brand :: ");
            brand = Console.ReadLine();

            Console.Write("Enter model :: ");
            model = Console.ReadLine();

            Console.Write("Enter price :: ");
            price = int.Parse(Console.ReadLine());
        }
    }
}
