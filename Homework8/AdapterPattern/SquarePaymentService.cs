namespace AdapterPattern
{
    public class SquarePaymentService
    {
        public void Charge(decimal amount, string currency)
        {
            Console.WriteLine($"[Square] Списание суммы {amount} {currency}");
            Console.WriteLine($"[Square] Платеж успешно обработан");
        }
    }
}