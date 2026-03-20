namespace DecoratorPattern
{
    public class Tea : Beverage
    {
        public override double Cost()
        {
            return 100.0;
        }

        public override string ToString()
        {
            return "Чай";
        }
    }
}