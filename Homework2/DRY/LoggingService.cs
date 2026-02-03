namespace DesignPrinciplesDemo.DRY
{
    public class LoggingService
    {
        public void Log(string message)
        {
            string connectionString = Configuration.ConnectionString;
            Console.WriteLine($"Запись лога в БД: {message}");
            // Логика записи лога в базу данных
        }
    }
}