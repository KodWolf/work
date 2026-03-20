namespace DecoratorPattern
{
    public class Chocolate : BeverageDecorator
    {
        public Chocolate(Beverage beverage) : base(beverage)
        {
        }

        public override double Cost()
        {
            return _beverage.Cost() + 45.0;
        }

        public override string ToString()
        {
            return _beverage.ToString() + " + шоколад";
        }
    }
}