namespace AdapterPattern
{
    public class ExternalLogisticsServiceC
    {
        private Dictionary<string, string> _deliveryInfo = new Dictionary<string, string>
        {
            ["DEL-001"] = "COMPLETED",
            ["DEL-002"] = "IN_PROGRESS",
            ["DEL-003"] = "PENDING"
        };

        public void StartDelivery(string deliveryId, string address)
        {
            Console.WriteLine($"[ExternalC] Начало доставки {deliveryId} по адресу: {address}");
            _deliveryInfo[deliveryId] = "IN_PROGRESS";
        }

        public string GetDeliveryDetails(string deliveryId)
        {
            return _deliveryInfo.ContainsKey(deliveryId)
                ? $"[ExternalC] Доставка {deliveryId}: {_deliveryInfo[deliveryId]}"
                : $"[ExternalC] Доставка {deliveryId} не найдена";
        }

        public decimal CalculatePrice(string deliveryId, bool isExpress)
        {
            decimal basePrice = 600;
            return isExpress ? basePrice * 1.5m : basePrice;
        }
    }
}