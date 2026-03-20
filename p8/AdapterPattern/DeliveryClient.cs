namespace AdapterPattern
{
    public static class AdapterDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== ПАТТЕРН АДАПТЕР ===\n");

            var factory = new DeliveryServiceFactory();

            while (true)
            {
                Console.WriteLine("\nДоступные службы доставки:");
                Console.WriteLine("1 - Внутренняя служба");
                Console.WriteLine("2 - External Service A (через адаптер)");
                Console.WriteLine("3 - External Service B (через адаптер)");
                Console.WriteLine("4 - External Service C (дополнительная)");
                Console.WriteLine("0 - Назад в главное меню");
                Console.Write("Выберите службу: ");

                var choice = Console.ReadLine();
                if (choice == "0") break;

                string serviceType = choice switch
                {
                    "1" => "internal",
                    "2" => "external_a",
                    "3" => "external_b",
                    "4" => "external_c",
                    _ => null
                };

                if (serviceType == null)
                {
                    Console.WriteLine("Неверный выбор");
                    continue;
                }

                try
                {
                    var service = factory.GetDeliveryService(serviceType);
                    TestDeliveryService(service);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании службы: {ex.Message}");
                }
            }
        }

        private static void TestDeliveryService(IInternalDeliveryService service)
        {
            Console.WriteLine($"\nТестирование {service.GetType().Name}:");
            Console.WriteLine(new string('-', 40));

            string[] testOrders = { "ORD-001", "ORD-002", "ORD-999" };

            foreach (var orderId in testOrders)
            {
                Console.WriteLine($"\nЗаказ: {orderId}");

                try
                {
                    // Проверка статуса
                    string status = service.GetDeliveryStatus(orderId);
                    Console.WriteLine(status);

                    // Расчет стоимости
                    decimal cost = service.CalculateDeliveryCost(orderId);
                    Console.WriteLine($"Стоимость доставки: {cost} руб.");

                    // Отправка заказа
                    service.DeliverOrder(orderId);

                    // Повторная проверка статуса
                    status = service.GetDeliveryStatus(orderId);
                    Console.WriteLine(status);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обработке заказа {orderId}: {ex.Message}");
                }

                Console.WriteLine(new string('-', 20));
            }
        }
    }
}