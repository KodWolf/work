using System;
using TravelSystem;
using StockSystem;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("ПАТТЕРНЫ ПРОЕКТИРОВАНИЯ");
            Console.WriteLine("========================");
            Console.WriteLine("1. Паттерн Стратегия - Система бронирования путешествий");
            Console.WriteLine("2. Паттерн Наблюдатель - Биржевая система");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    TravelProgram.Run();
                    break;
                case "2":
                    StockProgram.Run().Wait();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}