using System;
using DesignPrinciplesDemo.DRY;
using DesignPrinciplesDemo.KISS;
using DesignPrinciplesDemo.YAGNI;

namespace DesignPrinciplesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация принципов проектирования ===\n");


            Console.WriteLine("1. Принцип DRY:");
            var logger = new Logger();
            logger.LogError("Ошибка соединения");
            logger.LogInfo("Приложение запущено");

            var dbService = new DatabaseService();
            dbService.Connect();


            Console.WriteLine("\n2. Принцип KISS:");
            var processor = new SimpleNumberProcessor();
            int[] numbers = { -1, 2, 0, 4, -3 };
            processor.ProcessNumbers(numbers);

            Console.WriteLine($"Результат деления: {processor.Divide(10, 2)}");
            Console.WriteLine($"Деление на ноль: {processor.Divide(10, 0)}");

  
            Console.WriteLine("\n3. Принцип YAGNI:");
            var user = new User
            {
                Name = "Иван Иванов",
                Email = "ivan@example.com",
                Address = "ул. Примерная, 1"
            };

            var userRepo = new UserRepository();
            userRepo.SaveToDatabase(user);

            var emailService = new EmailService();
            emailService.SendEmail(user.Email, "Добро пожаловать!");

            var fileReader = new SimpleFileReader();
            fileReader.ReadFile("test.txt");

            var reportGenerator = new PdfReportGenerator();
            reportGenerator.Generate();
        }
    }
}