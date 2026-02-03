using System;
using System.Collections.Generic;

namespace TransportSystem
{
    class Fleet
    {
        private List<Garage> garages = new List<Garage>();

        public void AddGarage(Garage garage)
        {
            garages.Add(garage);
            Console.WriteLine("Гараж добавлен в автопарк");
        }

        public void RemoveGarage(Garage garage)
        {
            garages.Remove(garage);
            Console.WriteLine("Гараж удалён из автопарка");
        }

        public void FindVehicle(string model)
        {
            foreach (Garage garage in garages)
            {
                foreach (Vehicle vehicle in garage.GetVehicles())
                {
                    if (vehicle.Model == model)
                    {
                        Console.WriteLine($"Найдено транспортное средство: {vehicle}");
                        return;
                    }
                }
            }

            Console.WriteLine("Транспортное средство не найдено");
        }
    }
}
