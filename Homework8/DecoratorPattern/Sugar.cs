namespace DecoratorPattern
{
    public class Sugar : BeverageDecorator
    {
        public Sugar(Beverage beverage) : base(beverage)
        {
        }

        public override double Cost()
        {
            return _beverage.Cost() + 5.0;
        }

        public override string ToString()
        {
            return _beverage.ToString() + " + сахар";
        }
    }
}