using System;
using PaymentSystem;
using ObserverSystem;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("ПАТТЕРНЫ ПРОЕКТИРОВАНИЯ");
            Console.WriteLine("========================");
            Console.WriteLine("1. Паттерн Стратегия - Система оплаты");
            Console.WriteLine("2. Паттерн Наблюдатель - Курсы валют");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PaymentProgram.Run();
                    break;
                case "2":
                    ObserverProgram.Run();
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