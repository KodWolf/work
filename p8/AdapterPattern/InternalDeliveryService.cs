namespace AdapterPattern
{
    public class InternalDeliveryService : IInternalDeliveryService
    {
        private Dictionary<string, string> _statuses = new Dictionary<string, string>
        {
            ["ORD-001"] = "Доставлен",
            ["ORD-002"] = "В пути",
            ["ORD-003"] = "Ожидает отправки"
        };

        public void DeliverOrder(string orderId)
        {
            Console.WriteLine($"[Internal] Доставка заказа {orderId}");
            _statuses[orderId] = "В пути";
        }

        public string GetDeliveryStatus(string orderId)
        {
            return _statuses.ContainsKey(orderId)
                ? $"[Internal] Статус {orderId}: {_statuses[orderId]}"
                : $"[Internal] Заказ {orderId} не найден";
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            // Базовая стоимость 500 + случайная надбавка
            return 500 + (orderId.GetHashCode() % 300);
        }
    }
}