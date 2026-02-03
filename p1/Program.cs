using System;

namespace TransportSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car("Toyota", "Camry", 2022, 4, "Automatic");
            Motorcycle moto1 = new Motorcycle("Yamaha", "R1", 2021, "Sport", false);

            Garage garage1 = new Garage();
            garage1.AddVehicle(car1);
            garage1.AddVehicle(moto1);

            Fleet fleet = new Fleet();
            fleet.AddGarage(garage1);

            car1.StartEngine();
            moto1.StartEngine();

            fleet.FindVehicle("Camry");

            garage1.RemoveVehicle(car1);

            Console.ReadLine();
        }
    }
}
