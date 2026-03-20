namespace DecoratorPattern
{
    public class Espresso : Beverage
    {
        public override double Cost()
        {
            return 150.0;
        }

        public override string ToString()
        {
            return "Эспрессо";
        }
    }
}