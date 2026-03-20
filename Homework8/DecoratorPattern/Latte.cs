namespace DecoratorPattern
{
    public class Latte : Beverage
    {
        public override double Cost()
        {
            return 200.0;
        }

        public override string ToString()
        {
            return "Латте";
        }
    }
}
