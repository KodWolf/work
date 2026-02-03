using System;

namespace TransportSystem
{
    class Car : Vehicle
    {
        public int DoorsCount { get; set; }
        public string TransmissionType { get; set; }

        public Car(string brand, string model, int year, int doorsCount, string transmissionType)
            : base(brand, model, year)
        {
            DoorsCount = doorsCount;
            TransmissionType = transmissionType;
        }

        public override void StartEngine()
        {
            Console.WriteLine($"Автомобиль {Brand} {Model}: двигатель запущен");
        }
    }
}
