namespace AdapterPattern
{
    public class ExternalLogisticsServiceA
    {
        private Dictionary<int, string> _statuses = new Dictionary<int, string>
        {
            [1001] = "Delivered",
            [1002] = "In Transit",
            [1003] = "Pending"
        };

        public void ShipItem(int itemId)
        {
            Console.WriteLine($"[ExternalA] Отправка товара {itemId}");
            _statuses[itemId] = "Shipped";
        }

        public string TrackShipment(int shipmentId)
        {
            return _statuses.ContainsKey(shipmentId)
                ? $"[ExternalA] Статус {shipmentId}: {_statuses[shipmentId]}"
                : $"[ExternalA] Отправка {shipmentId} не найдена";
        }

        public decimal CalculateShippingCost(int itemId)
        {
            return 450 + (itemId % 200);
        }
    }
}