using System;

namespace TransportSystem
{
    class Motorcycle : Vehicle
    {
        public string BodyType { get; set; }
        public bool HasSidecar { get; set; }

        public Motorcycle(string brand, string model, int year, string bodyType, bool hasSidecar)
            : base(brand, model, year)
        {
            BodyType = bodyType;
            HasSidecar = hasSidecar;
        }

        public override void StartEngine()
        {
            Console.WriteLine($"Мотоцикл {Brand} {Model}: двигатель запущен");
        }
    }
}
