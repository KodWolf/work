namespace AdapterPattern
{
    public class DeliveryServiceFactory
    {
        public IInternalDeliveryService GetDeliveryService(string serviceType)
        {
            Console.WriteLine($"[Factory] Создание службы: {serviceType}");

            return serviceType.ToLower() switch
            {
                "internal" => new InternalDeliveryService(),
                "external_a" => new LogisticsAdapterA(new ExternalLogisticsServiceA()),
                "external_b" => new LogisticsAdapterB(new ExternalLogisticsServiceB()),
                "external_c" => new LogisticsAdapterC(new ExternalLogisticsServiceC()),
                _ => throw new ArgumentException($"Неизвестная служба: {serviceType}")
            };
        }
    }
}