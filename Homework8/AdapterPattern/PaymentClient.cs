namespace AdapterPattern
{
    public static class PaymentDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== ПЛАТЕЖНАЯ СИСТЕМА: Адаптер ===\n");

            // Тест 1: PayPal (существующая система)
            Console.WriteLine("1. ОПЛАТА ЧЕРЕЗ PAYPAL:");
            Console.WriteLine(new string('-', 40));
            IPaymentProcessor paypal = new PayPalPaymentProcessor();
            paypal.ProcessPayment(1500.50);

            // Тест 2: Stripe через адаптер
            Console.WriteLine("\n2. ОПЛАТА ЧЕРЕЗ STRIPE (через адаптер):");
            Console.WriteLine(new string('-', 40));
            StripePaymentService stripeService = new StripePaymentService();
            IPaymentProcessor stripeAdapter = new StripePaymentAdapter(stripeService);
            stripeAdapter.ProcessPayment(2500.75);

            // Тест 3: Square через адаптер (дополнительная система)
            Console.WriteLine("\n3. ОПЛАТА ЧЕРЕЗ SQUARE (через адаптер):");
            Console.WriteLine(new string('-', 40));
            SquarePaymentService squareService = new SquarePaymentService();
            IPaymentProcessor squareAdapter = new SquarePaymentAdapter(squareService);
            squareAdapter.ProcessPayment(3200.00);

            // Тест 4: Использование всех систем через единый интерфейс
            Console.WriteLine("\n4. ВСЕ СИСТЕМЫ ЧЕРЕЗ ЕДИНЫЙ ИНТЕРФЕЙС:");
            Console.WriteLine(new string('-', 40));

            IPaymentProcessor[] processors = new IPaymentProcessor[]
            {
                new PayPalPaymentProcessor(),
                new StripePaymentAdapter(new StripePaymentService()),
                new SquarePaymentAdapter(new SquarePaymentService())
            };

            double[] amounts = { 500.00, 1200.50, 850.25 };

            for (int i = 0; i < processors.Length; i++)
            {
                Console.WriteLine($"\n--- Система {i + 1} ---");
                processors[i].ProcessPayment(amounts[i]);
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Все платежные системы работают через единый интерфейс IPaymentProcessor!");
        }
    }
}