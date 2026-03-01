using System;
using System.Collections.Generic;

namespace TravelSystem
{
    public class TripDetails
    {
        public double Distance { get; set; }
        public int Passengers { get; set; }
        public string ServiceClass { get; set; }
        public bool HasDiscount { get; set; }
        public string DiscountType { get; set; }
        public int ChildrenCount { get; set; }
        public int PensionersCount { get; set; }
        public bool HasLuggage { get; set; }
        public int LuggageCount { get; set; }
        public bool IsGroupTrip { get; set; }
        public int GroupSize { get; set; }
        public bool HasTransfers { get; set; }
        public int TransferCount { get; set; }

        public TripDetails()
        {
            Passengers = 1;
            ServiceClass = "economy";
            ChildrenCount = 0;
            PensionersCount = 0;
        }
    }

    public interface ICostCalculationStrategy
    {
        double CalculateCost(TripDetails trip);
        string GetTransportType();
    }

    public class PlaneCostStrategy : ICostCalculationStrategy
    {
        public double CalculateCost(TripDetails trip)
        {
            double baseCost = trip.Distance * 8.0;

            if (trip.ServiceClass.ToLower() == "business")
                baseCost *= 2.5;

            if (trip.HasLuggage)
                baseCost += trip.LuggageCount * 500;

            if (trip.HasTransfers)
                baseCost += trip.TransferCount * 300;

            double total = baseCost * trip.Passengers;

            if (trip.IsGroupTrip && trip.GroupSize >= 5)
                total *= 0.85;

            if (trip.ChildrenCount > 0)
                total -= (baseCost * 0.3 * trip.ChildrenCount);

            if (trip.PensionersCount > 0)
                total -= (baseCost * 0.15 * trip.PensionersCount);

            total += 1500;

            return total;
        }

        public string GetTransportType() => "Самолет";
    }

    public class TrainCostStrategy : ICostCalculationStrategy
    {
        public double CalculateCost(TripDetails trip)
        {
            double baseCost = trip.Distance * 3.5;

            if (trip.ServiceClass.ToLower() == "business")
                baseCost *= 1.8;

            if (trip.HasLuggage)
                baseCost += trip.LuggageCount * 200;

            if (trip.HasTransfers)
                baseCost += trip.TransferCount * 100;

            double total = baseCost * trip.Passengers;

            if (trip.IsGroupTrip && trip.GroupSize >= 10)
                total *= 0.7;

            if (trip.ChildrenCount > 0)
                total -= (baseCost * 0.5 * trip.ChildrenCount);

            if (trip.PensionersCount > 0)
                total -= (baseCost * 0.2 * trip.PensionersCount);

            return total;
        }

        public string GetTransportType() => "Поезд";
    }

    public class BusCostStrategy : ICostCalculationStrategy
    {
        public double CalculateCost(TripDetails trip)
        {
            double baseCost = trip.Distance * 1.2;

            if (trip.ServiceClass.ToLower() == "business")
                baseCost *= 1.3;

            if (trip.HasLuggage)
                baseCost += trip.LuggageCount * 50;

            if (trip.HasTransfers)
                baseCost += trip.TransferCount * 30;

            double total = baseCost * trip.Passengers;

            if (trip.IsGroupTrip && trip.GroupSize >= 15)
                total *= 0.6;

            if (trip.ChildrenCount > 0)
                total -= (baseCost * 0.4 * trip.ChildrenCount);

            if (trip.PensionersCount > 0)
                total -= (baseCost * 0.25 * trip.PensionersCount);

            return total;
        }

        public string GetTransportType() => "Автобус";
    }

    public class TravelBookingContext
    {
        private ICostCalculationStrategy _strategy;
        public TripDetails CurrentTrip { get; set; }

        public TravelBookingContext()
        {
            CurrentTrip = new TripDetails();
        }

        public void SetStrategy(ICostCalculationStrategy strategy)
        {
            _strategy = strategy;
        }

        public double CalculatePrice()
        {
            if (_strategy == null)
                throw new InvalidOperationException("Стратегия не выбрана");

            if (CurrentTrip.Distance <= 0)
                throw new ArgumentException("Расстояние должно быть больше 0");

            if (CurrentTrip.Passengers <= 0)
                throw new ArgumentException("Количество пассажиров должно быть больше 0");

            if (CurrentTrip.ChildrenCount + CurrentTrip.PensionersCount > CurrentTrip.Passengers)
                throw new ArgumentException("Количество льготников превышает общее число пассажиров");

            return _strategy.CalculateCost(CurrentTrip);
        }

        public string GetStrategyInfo()
        {
            return _strategy?.GetTransportType() ?? "Не выбрано";
        }
    }

    public class TravelProgram
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("СИСТЕМА БРОНИРОВАНИЯ ПУТЕШЕСТВИЙ");
            Console.WriteLine("================================");

            var context = new TravelBookingContext();
            var strategies = new List<ICostCalculationStrategy>
            {
                new PlaneCostStrategy(),
                new TrainCostStrategy(),
                new BusCostStrategy()
            };

            bool running = true;

            while (running)
            {
                try
                {
                    Console.WriteLine("\nВведите данные поездки:");

                    Console.Write("Расстояние (км): ");
                    if (!double.TryParse(Console.ReadLine(), out double distance) || distance <= 0)
                    {
                        Console.WriteLine("Ошибка: неверное расстояние");
                        continue;
                    }
                    context.CurrentTrip.Distance = distance;

                    Console.Write("Общее количество пассажиров: ");
                    if (!int.TryParse(Console.ReadLine(), out int passengers) || passengers <= 0)
                    {
                        Console.WriteLine("Ошибка: неверное количество пассажиров");
                        continue;
                    }
                    context.CurrentTrip.Passengers = passengers;

                    Console.Write("Количество детей: ");
                    if (!int.TryParse(Console.ReadLine(), out int children))
                    {
                        Console.WriteLine("Ошибка: неверное количество детей");
                        continue;
                    }
                    context.CurrentTrip.ChildrenCount = children;

                    Console.Write("Количество пенсионеров: ");
                    if (!int.TryParse(Console.ReadLine(), out int pensioners))
                    {
                        Console.WriteLine("Ошибка: неверное количество пенсионеров");
                        continue;
                    }
                    context.CurrentTrip.PensionersCount = pensioners;

                    Console.Write("Класс обслуживания (economy/business): ");
                    string serviceClass = Console.ReadLine();
                    if (serviceClass.ToLower() == "business" || serviceClass.ToLower() == "economy")
                    {
                        context.CurrentTrip.ServiceClass = serviceClass.ToLower();
                    }

                    Console.Write("Есть багаж? (да/нет): ");
                    context.CurrentTrip.HasLuggage = Console.ReadLine().ToLower() == "да";

                    if (context.CurrentTrip.HasLuggage)
                    {
                        Console.Write("Количество мест багажа: ");
                        if (!int.TryParse(Console.ReadLine(), out int luggage))
                        {
                            Console.WriteLine("Ошибка: неверное количество багажа");
                            continue;
                        }
                        context.CurrentTrip.LuggageCount = luggage;
                    }

                    Console.Write("Это групповая поездка? (да/нет): ");
                    context.CurrentTrip.IsGroupTrip = Console.ReadLine().ToLower() == "да";

                    if (context.CurrentTrip.IsGroupTrip)
                    {
                        Console.Write("Размер группы: ");
                        if (!int.TryParse(Console.ReadLine(), out int groupSize) || groupSize < 2)
                        {
                            Console.WriteLine("Ошибка: неверный размер группы");
                            continue;
                        }
                        context.CurrentTrip.GroupSize = groupSize;
                    }

                    Console.Write("Есть пересадки? (да/нет): ");
                    context.CurrentTrip.HasTransfers = Console.ReadLine().ToLower() == "да";

                    if (context.CurrentTrip.HasTransfers)
                    {
                        Console.Write("Количество пересадок: ");
                        if (!int.TryParse(Console.ReadLine(), out int transfers) || transfers <= 0)
                        {
                            Console.WriteLine("Ошибка: неверное количество пересадок");
                            continue;
                        }
                        context.CurrentTrip.TransferCount = transfers;
                    }

                    Console.WriteLine("\nВыберите транспорт:");
                    for (int i = 0; i < strategies.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {strategies[i].GetTransportType()}");
                    }

                    Console.Write("Выбор: ");
                    if (!int.TryParse(Console.ReadLine(), out int choice) ||
                        choice < 1 || choice > strategies.Count)
                    {
                        Console.WriteLine("Ошибка: неверный выбор");
                        continue;
                    }

                    context.SetStrategy(strategies[choice - 1]);
                    double price = context.CalculatePrice();

                    Console.WriteLine("\n=== РЕЗУЛЬТАТ РАСЧЕТА ===");
                    Console.WriteLine($"Транспорт: {context.GetStrategyInfo()}");
                    Console.WriteLine($"Расстояние: {context.CurrentTrip.Distance} км");
                    Console.WriteLine($"Пассажиров: {context.CurrentTrip.Passengers}");
                    Console.WriteLine($"Детей: {context.CurrentTrip.ChildrenCount}");
                    Console.WriteLine($"Пенсионеров: {context.CurrentTrip.PensionersCount}");
                    Console.WriteLine($"Класс: {context.CurrentTrip.ServiceClass}");
                    Console.WriteLine($"Багаж: {(context.CurrentTrip.HasLuggage ? $"Да ({context.CurrentTrip.LuggageCount})" : "Нет")}");
                    Console.WriteLine($"Групповая поездка: {(context.CurrentTrip.IsGroupTrip ? $"Да (размер: {context.CurrentTrip.GroupSize})" : "Нет")}");
                    Console.WriteLine($"Пересадки: {(context.CurrentTrip.HasTransfers ? $"Да ({context.CurrentTrip.TransferCount})" : "Нет")}");
                    Console.WriteLine($"ИТОГОВАЯ ЦЕНА: {price:C}");
                    Console.WriteLine("===========================");

                    Console.Write("\nРассчитать еще одну поездку? (да/нет): ");
                    running = Console.ReadLine().ToLower() == "да";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.Write("Повторить попытку? (да/нет): ");
                    running = Console.ReadLine().ToLower() == "да";
                }
            }
        }
    }
}