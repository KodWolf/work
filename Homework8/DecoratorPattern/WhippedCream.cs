namespace DecoratorPattern
{
    public class WhippedCream : BeverageDecorator
    {
        public WhippedCream(Beverage beverage) : base(beverage)
        {
        }

        public override double Cost()
        {
            return _beverage.Cost() + 40.0;
        }

        public override string ToString()
        {
            return _beverage.ToString() + " + взбитые сливки";
        }
    }
}