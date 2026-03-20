using DecoratorPattern;
using AdapterPattern;

namespace Module08_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("МОДУЛЬ 08: Структурные паттерны");
            Console.WriteLine("Домашнее задание");
            Console.WriteLine("========================================\n");

            while (true)
            {
                Console.WriteLine("\nВыберите демонстрацию:");
                Console.WriteLine("1 - Паттерн Декоратор (Кафе)");
                Console.WriteLine("2 - Паттерн Адаптер (Платежи)");
                Console.WriteLine("0 - Выход");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CoffeeShopDemo.Run();
                        break;
                    case "2":
                        PaymentDemo.Run();
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