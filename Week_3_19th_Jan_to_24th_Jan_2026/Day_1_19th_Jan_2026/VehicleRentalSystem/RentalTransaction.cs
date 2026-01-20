using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRentalSystem
{
    internal class RentalTransaction
    {
        private Vehicle vehicle;
        private Customer customer;
        private int days;

        public RentalTransaction(Vehicle vehicle, Customer customer, int days)
        {
            this.vehicle = vehicle;
            this.customer = customer;
            this.days = days;
            vehicle.Rent();
        }

        public double CalculateCharge()
        {
            return vehicle.RatePerDay * days;
        }

        public void CloseRental()
        {
            vehicle.Return();
        }
    }
}
