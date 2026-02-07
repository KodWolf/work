public class PriceCalculator
{
    public double CalculateTotal(Order order)
    {
        return order.Quantity * order.Price * 0.9;
    }
}
