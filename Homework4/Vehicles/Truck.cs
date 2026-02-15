using System;

namespace TransportFactoryApp
{
    public class Truck : IVehicle
    {
        private double loadCapacity;
        private int axles;

        public Truck(double loadCapacity, int axles)
        {
            this.loadCapacity = loadCapacity;
            this.axles = axles;
        }

        public void Drive()
        {
            Console.WriteLine("Грузовик грузоподъемностью " + loadCapacity + " тонн едет.");
        }

        public void Refuel()
        {
            Console.WriteLine("Грузовик с " + axles + " осями заправляется.");
        }
    }
}
