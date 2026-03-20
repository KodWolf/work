namespace AdapterPattern
{
    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"[Stripe] Создание транзакции на сумму ${totalAmount}");
            Console.WriteLine($"[Stripe] Платеж успешно обработан");
        }
    }
}