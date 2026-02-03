using System;
using System.Collections.Generic;

namespace TransportSystem
{
    class Garage
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            Console.WriteLine($"Добавлено в гараж: {vehicle}");
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            Console.WriteLine($"Удалено из гаража: {vehicle}");
        }

        public List<Vehicle> GetVehicles()
        {
            return vehicles;
        }
    }
}
