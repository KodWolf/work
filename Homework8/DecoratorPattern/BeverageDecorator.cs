namespace DecoratorPattern
{
    public abstract class BeverageDecorator : Beverage
    {
        protected Beverage _beverage;

        public BeverageDecorator(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override string ToString()
        {
            return _beverage.ToString();
        }
    }
}