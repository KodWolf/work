namespace DesignPrinciplesDemo.DRY
{
    public class DatabaseService
    {
        public void Connect()
        {
            string connectionString = Configuration.ConnectionString;
            Console.WriteLine($"Подключение к БД с строкой: {connectionString}");
            // Логика подключения к базе данных
        }
    }
}