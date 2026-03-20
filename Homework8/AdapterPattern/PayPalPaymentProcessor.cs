namespace AdapterPattern
{
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"[PayPal] Обработка платежа на сумму ${amount}");
            Console.WriteLine($"[PayPal] Платеж успешно проведен");
        }
    }
}