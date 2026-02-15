using System;

namespace TransportFactoryApp
{
    public class Motorcycle : IVehicle
    {
        private string type;
        private int engineVolume;

        public Motorcycle(string type, int engineVolume)
        {
            this.type = type;
            this.engineVolume = engineVolume;
        }

        public void Drive()
        {
            Console.WriteLine("Мотоцикл типа " + type + " едет.");
        }

        public void Refuel()
        {
            Console.WriteLine("Мотоцикл с объемом двигателя " + engineVolume + " заправляется.");
        }
    }
}
