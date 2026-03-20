namespace DecoratorPattern
{
    public static class CoffeeShopDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== КАФЕ: Система заказов ===\n");

            // Тест 1: Базовые напитки
            Console.WriteLine("1. БАЗОВЫЕ НАПИТКИ:");
            Console.WriteLine(new string('-', 40));

            Beverage espresso = new Espresso();
            Console.WriteLine($"{espresso} - {espresso.Cost()} руб.");

            Beverage tea = new Tea();
            Console.WriteLine($"{tea} - {tea.Cost()} руб.");

            // Тест 2: Дополнительные напитки
            Console.WriteLine("\n2. ДОПОЛНИТЕЛЬНЫЕ НАПИТКИ:");
            Console.WriteLine(new string('-', 40));

            Beverage latte = new Latte();
            Console.WriteLine($"{latte} - {latte.Cost()} руб.");

            Beverage mocha = new Mocha();
            Console.WriteLine($"{mocha} - {mocha.Cost()} руб.");

            // Тест 3: Напитки с добавками
            Console.WriteLine("\n3. НАПИТКИ С ДОБАВКАМИ:");
            Console.WriteLine(new string('-', 40));

            // Эспрессо с молоком
            Beverage espressoWithMilk = new Milk(new Espresso());
            Console.WriteLine($"{espressoWithMilk} - {espressoWithMilk.Cost()} руб.");

            // Чай с сахаром
            Beverage teaWithSugar = new Sugar(new Tea());
            Console.WriteLine($"{teaWithSugar} - {teaWithSugar.Cost()} руб.");

            // Латте со сливками
            Beverage latteWithCream = new WhippedCream(new Latte());
            Console.WriteLine($"{latteWithCream} - {latteWithCream.Cost()} руб.");

            // Тест 4: Несколько добавок
            Console.WriteLine("\n4. НЕСКОЛЬКО ДОБАВОК:");
            Console.WriteLine(new string('-', 40));

            // Эспрессо с молоком и сахаром
            Beverage espressoWithMilkAndSugar = new Sugar(new Milk(new Espresso()));
            Console.WriteLine($"{espressoWithMilkAndSugar} - {espressoWithMilkAndSugar.Cost()} руб.");

            // Чай со сливками и сиропом
            Beverage teaWithCreamAndSyrup = new Syrup(new WhippedCream(new Tea()));
            Console.WriteLine($"{teaWithCreamAndSyrup} - {teaWithCreamAndSyrup.Cost()} руб.");

            // Тест 5: Дополнительные добавки
            Console.WriteLine("\n5. ДОПОЛНИТЕЛЬНЫЕ ДОБАВКИ:");
            Console.WriteLine(new string('-', 40));

            // Мокко с шоколадом
            Beverage mochaWithChocolate = new Chocolate(new Mocha());
            Console.WriteLine($"{mochaWithChocolate} - {mochaWithChocolate.Cost()} руб.");

            // Латте с сиропом и шоколадом
            Beverage latteWithSyrupAndChocolate = new Chocolate(new Syrup(new Latte()));
            Console.WriteLine($"{latteWithSyrupAndChocolate} - {latteWithSyrupAndChocolate.Cost()} руб.");

            // Тест 6: Все добавки (демонстрация гибкости)
            Console.WriteLine("\n6. ВСЕ ДОБАВКИ (максимальный напиток):");
            Console.WriteLine(new string('-', 40));

            Beverage megaDrink = new Espresso();
            megaDrink = new Milk(megaDrink);
            megaDrink = new Sugar(megaDrink);
            megaDrink = new WhippedCream(megaDrink);
            megaDrink = new Syrup(megaDrink);
            megaDrink = new Chocolate(megaDrink);

            Console.WriteLine($"{megaDrink}");
            Console.WriteLine($"Итоговая стоимость: {megaDrink.Cost()} руб.");

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Демонстрация завершена");
        }
    }
}