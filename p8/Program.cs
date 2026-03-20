using DecoratorPattern;
using AdapterPattern;

namespace Module08_PracticalWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("МОДУЛЬ 08: Структурные паттерны");
            Console.WriteLine("========================================\n");

            while (true)
            {
                Console.WriteLine("\nВыберите демонстрацию:");
                Console.WriteLine("1 - Паттерн Декоратор (система отчетности)");
                Console.WriteLine("2 - Паттерн Адаптер (система логистики)");
                Console.WriteLine("0 - Выход");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DecoratorDemo.Run();
                        break;
                    case "2":
                        AdapterDemo.Run();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}