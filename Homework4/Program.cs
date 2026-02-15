using System;

namespace TransportFactoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите тип транспорта:");
            Console.WriteLine("1 - Автомобиль");
            Console.WriteLine("2 - Мотоцикл");
            Console.WriteLine("3 - Грузовик");

            int choice = Convert.ToInt32(Console.ReadLine());

            VehicleFactory factory = null;

            if (choice == 1)
            {
                Console.Write("Марка: ");
                string brand = Console.ReadLine();

                Console.Write("Модель: ");
                string model = Console.ReadLine();

                Console.Write("Тип топлива: ");
                string fuel = Console.ReadLine();

                factory = new CarFactory(brand, model, fuel);
            }
            else if (choice == 2)
            {
                Console.Write("Тип мотоцикла: ");
                string type = Console.ReadLine();

                Console.Write("Объем двигателя: ");
                int volume = Convert.ToInt32(Console.ReadLine());

                factory = new MotorcycleFactory(type, volume);
            }
            else if (choice == 3)
            {
                Console.Write("Грузоподъемность: ");
                double capacity = Convert.ToDouble(Console.ReadLine());

                Console.Write("Количество осей: ");
                int axles = Convert.ToInt32(Console.ReadLine());

                factory = new TruckFactory(capacity, axles);
            }

            if (factory != null)
            {
                IVehicle vehicle = factory.CreateVehicle();
                vehicle.Drive();
                vehicle.Refuel();
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }

            Console.ReadLine();
        }
    }
}
