namespace DecoratorPattern
{
    public class Syrup : BeverageDecorator
    {
        public Syrup(Beverage beverage) : base(beverage)
        {
        }

        public override double Cost()
        {
            return _beverage.Cost() + 35.0;
        }

        public override string ToString()
        {
            return _beverage.ToString() + " + сироп";
        }
    }
}