using System;

namespace TransportSystem
{
    abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        protected Vehicle(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        public virtual void StartEngine()
        {
            Console.WriteLine($"{Brand} {Model}: двигатель запущен");
        }

        public virtual void StopEngine()
        {
            Console.WriteLine($"{Brand} {Model}: двигатель остановлен");
        }

        public override string ToString()
        {
            return $"{Brand} {Model} ({Year})";
        }
    }
}
