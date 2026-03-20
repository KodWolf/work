namespace AdapterPattern
{
    public class SquarePaymentAdapter : IPaymentProcessor
    {
        private SquarePaymentService _squareService;

        public SquarePaymentAdapter(SquarePaymentService squareService)
        {
            _squareService = squareService;
        }

        public void ProcessPayment(double amount)
        {
            _squareService.Charge((decimal)amount, "USD");
        }
    }
}