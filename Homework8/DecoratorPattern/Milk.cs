namespace DecoratorPattern
{
    public class Milk : BeverageDecorator
    {
        public Milk(Beverage beverage) : base(beverage)
        {
        }

        public override double Cost()
        {
            return _beverage.Cost() + 30.0;
        }

        public override string ToString()
        {
            return _beverage.ToString() + " + молоко";
        }
    }
}