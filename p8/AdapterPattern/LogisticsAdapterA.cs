namespace AdapterPattern
{
    public class LogisticsAdapterA : IInternalDeliveryService
    {
        private ExternalLogisticsServiceA _service;

        public LogisticsAdapterA(ExternalLogisticsServiceA service)
        {
            _service = service;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                Console.WriteLine($"[AdapterA] Адаптация заказа {orderId} для ExternalA");

                if (!int.TryParse(orderId.Replace("ORD-", ""), out int itemId))
                {
                    itemId = new Random().Next(1000, 9999);
                    Console.WriteLine($"[AdapterA] Сгенерирован новый ID: {itemId}");
                }

                _service.ShipItem(itemId);
                Console.WriteLine($"[AdapterA] Заказ {orderId} успешно передан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AdapterA] ОШИБКА: {ex.Message}");
                throw;
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                if (int.TryParse(orderId.Replace("ORD-", ""), out int itemId))
                {
                    return _service.TrackShipment(itemId);
                }
                return $"[AdapterA] Не удалось определить ID для {orderId}";
            }
            catch (Exception ex)
            {
                return $"[AdapterA] Ошибка получения статуса: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            try
            {
                if (int.TryParse(orderId.Replace("ORD-", ""), out int itemId))
                {
                    return _service.CalculateShippingCost(itemId);
                }
                return 0;
            }
            catch
            {
                return 1000; // Стоимость по умолчанию при ошибке
            }
        }
    }
}