namespace DecoratorPattern
{
    public static class DecoratorDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== ПАТТЕРН ДЕКОРАТОР ===\n");

            while (true)
            {
                Console.WriteLine("\nВыберите тип отчета:");
                Console.WriteLine("1 - Отчет по продажам");
                Console.WriteLine("2 - Отчет по пользователям");
                Console.WriteLine("0 - Назад в главное меню");
                Console.Write("Ваш выбор: ");

                var reportChoice = Console.ReadLine();
                if (reportChoice == "0") break;

                IReport report = reportChoice switch
                {
                    "1" => new SalesReport(),
                    "2" => new UserReport(),
                    _ => null
                };

                if (report == null)
                {
                    Console.WriteLine("Неверный выбор");
                    continue;
                }

                // Динамический выбор декораторов
                report = ChooseDecorators(report);

                // Показываем результат
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("СФОРМИРОВАННЫЙ ОТЧЕТ:");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine(report.Generate());

                // Спрашиваем, хочет ли пользователь экспортировать
                AskForExport(report);
            }
        }

        private static IReport ChooseDecorators(IReport report)
        {
            var decorators = new List<string>();
            var parameters = new Dictionary<string, object>();

            while (true)
            {
                Console.WriteLine("\nДоступные декораторы:");
                Console.WriteLine("1 - Фильтр по датам");
                Console.WriteLine("2 - Сортировка");

                if (report is UserReport)
                    Console.WriteLine("3 - Фильтр по характеристикам пользователей");
                else
                    Console.WriteLine("3 - Фильтр по сумме (только для продаж)");

                Console.WriteLine("0 - Завершить выбор");
                Console.Write("Выберите декоратор: ");

                var choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите начальную дату (дд.мм.гггг): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            Console.Write("Введите конечную дату (дд.мм.гггг): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                            {
                                report = new DateFilterDecorator(report, startDate, endDate);
                                Console.WriteLine("✓ Фильтр по датам применен");
                            }
                        }
                        break;

                    case "2":
                        if (report is SalesReport)
                        {
                            Console.Write("Сортировать по (сумма/дата/товар): ");
                            string sortBy = Console.ReadLine();
                            Console.Write("По возрастанию? (да/нет): ");
                            bool asc = Console.ReadLine().ToLower() == "да";
                            report = new SortingDecorator(report, sortBy, asc);
                            Console.WriteLine("✓ Сортировка применена");
                        }
                        else
                        {
                            Console.WriteLine("Сортировка для пользователей будет добавлена в следующей версии");
                        }
                        break;

                    case "3":
                        if (report is UserReport)
                        {
                            Console.Write("Город (оставьте пустым для пропуска): ");
                            string city = Console.ReadLine();

                            Console.Write("Минимальный возраст: ");
                            int? minAge = int.TryParse(Console.ReadLine(), out int min) ? min : null;

                            Console.Write("Максимальный возраст: ");
                            int? maxAge = int.TryParse(Console.ReadLine(), out int max) ? max : null;

                            Console.Write("Минимальная сумма покупок: ");
                            decimal? minSpent = decimal.TryParse(Console.ReadLine(), out decimal spent) ? spent : null;

                            report = new CustomerFilterDecorator(report,
                                string.IsNullOrEmpty(city) ? null : city,
                                minAge, maxAge, minSpent);
                            Console.WriteLine("✓ Фильтр по пользователям применен");
                        }
                        else
                        {
                            Console.WriteLine("Фильтр по сумме будет добавлен в следующей версии");
                        }
                        break;
                }
            }

            return report;
        }

        private static void AskForExport(IReport report)
        {
            Console.WriteLine("\nЭкспортировать отчет?");
            Console.WriteLine("1 - В CSV");
            Console.WriteLine("2 - В PDF (симуляция)");
            Console.WriteLine("0 - Нет");
            Console.Write("Выбор: ");

            var exportChoice = Console.ReadLine();
            string filename = $"report_{DateTime.Now:yyyyMMdd_HHmmss}";

            switch (exportChoice)
            {
                case "1":
                    report = new CsvExportDecorator(report, filename + ".csv");
                    Console.WriteLine(report.Generate());
                    break;
                case "2":
                    report = new PdfExportDecorator(report, filename + ".pdf");
                    Console.WriteLine(report.Generate());
                    break;
            }
        }
    }
}