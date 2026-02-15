namespace TransportFactoryApp
{
    public class MotorcycleFactory : VehicleFactory
    {
        private string type;
        private int engineVolume;

        public MotorcycleFactory(string type, int engineVolume)
        {
            this.type = type;
            this.engineVolume = engineVolume;
        }

        public override IVehicle CreateVehicle()
        {
            return new Motorcycle(type, engineVolume);
        }
    }
}
