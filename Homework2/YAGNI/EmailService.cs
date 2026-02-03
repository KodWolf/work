namespace DesignPrinciplesDemo.YAGNI
{
    public class EmailService
    {
        public void SendEmail(string to, string message)
        {
            Console.WriteLine($"Отправка email на {to}: {message}");
            // Код для отправки электронного письма
        }
    }
}