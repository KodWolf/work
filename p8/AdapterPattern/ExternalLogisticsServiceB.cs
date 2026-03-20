namespace AdapterPattern
{
    public class ExternalLogisticsServiceB
    {
        private Dictionary<string, string> _statuses = new Dictionary<string, string>
        {
            ["PKG001"] = "Delivered",
            ["PKG002"] = "Out for delivery",
            ["PKG003"] = "Processing"
        };

        public void SendPackage(string packageInfo)
        {
            Console.WriteLine($"[ExternalB] Отправка посылки: {packageInfo}");
        }

        public string CheckPackageStatus(string trackingCode)
        {
            return _statuses.ContainsKey(trackingCode)
                ? $"[ExternalB] Статус {trackingCode}: {_statuses[trackingCode]}"
                : $"[ExternalB] Посылка {trackingCode} не найдена";
        }

        public decimal GetPackagePrice(string packageInfo)
        {
            return 550;
        }
    }
}