namespace AdapterPattern
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }
}