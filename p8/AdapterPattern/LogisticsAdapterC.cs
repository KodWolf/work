namespace AdapterPattern
{
    public class LogisticsAdapterC : IInternalDeliveryService
    {
        private ExternalLogisticsServiceC _service;

        public LogisticsAdapterC(ExternalLogisticsServiceC service)
        {
            _service = service;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                Console.WriteLine($"[AdapterC] Адаптация заказа {orderId} для ExternalC");
                string deliveryId = $"DEL-{orderId.Replace("ORD-", "")}";
                _service.StartDelivery(deliveryId, "Адрес доставки");
                Console.WriteLine($"[AdapterC] Заказ {orderId} успешно передан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AdapterC] ОШИБКА: {ex.Message}");
                throw;
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                string deliveryId = $"DEL-{orderId.Replace("ORD-", "")}";
                return _service.GetDeliveryDetails(deliveryId);
            }
            catch (Exception ex)
            {
                return $"[AdapterC] Ошибка получения статуса: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            try
            {
                string deliveryId = $"DEL-{orderId.Replace("ORD-", "")}";
                return _service.CalculatePrice(deliveryId, false);
            }
            catch
            {
                return 1000;
            }
        }
    }
}