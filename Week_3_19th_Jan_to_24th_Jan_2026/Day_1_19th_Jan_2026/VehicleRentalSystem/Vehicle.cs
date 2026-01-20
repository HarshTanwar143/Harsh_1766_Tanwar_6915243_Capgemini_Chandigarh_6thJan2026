namespace VehicleRentalSystem
{
    class Vehicle
    {
        private int vehicleId;
        private string model;
        private double ratePerDay;
        private bool isAvailable = true;

        public Vehicle(int vehicleId, string model, double ratePerDay)
        {
            this.vehicleId = vehicleId;
            this.model = model;
            this.ratePerDay = ratePerDay;
        }

        public int VehicleId { get { return vehicleId; } }
        public string Model { get { return model; } }
        public double RatePerDay { get { return ratePerDay; } }
        public bool IsAvailable { get { return isAvailable; } }

        public void Rent()
        {
            isAvailable = false;
        }

        public void Return()
        {
            isAvailable = true;
        }
    }
}
