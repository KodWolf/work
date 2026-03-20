namespace DecoratorPattern
{
    public class Mocha : Beverage
    {
        public override double Cost()
        {
            return 220.0;
        }

        public override string ToString()
        {
            return "Мокко";
        }
    }
}