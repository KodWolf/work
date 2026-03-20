namespace AdapterPattern
{
    public class LogisticsAdapterB : IInternalDeliveryService
    {
        private ExternalLogisticsServiceB _service;

        public LogisticsAdapterB(ExternalLogisticsServiceB service)
        {
            _service = service;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                Console.WriteLine($"[AdapterB] Адаптация заказа {orderId} для ExternalB");
                string packageInfo = $"Order:{orderId}";
                _service.SendPackage(packageInfo);
                Console.WriteLine($"[AdapterB] Заказ {orderId} успешно передан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AdapterB] ОШИБКА: {ex.Message}");
                throw;
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                string trackingCode = $"PKG{orderId.Replace("ORD-", "")}";
                return _service.CheckPackageStatus(trackingCode);
            }
            catch (Exception ex)
            {
                return $"[AdapterB] Ошибка получения статуса: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            try
            {
                return _service.GetPackagePrice($"Order:{orderId}");
            }
            catch
            {
                return 1000;
            }
        }
    }
}