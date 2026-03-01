using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ObserverSystem
{
    public class CurrencyRate
    {
        public string Currency { get; set; }
        public double Rate { get; set; }
        public double Change { get; set; }

        public override string ToString()
        {
            string arrow = Change >= 0 ? "▲" : "▼";
            return $"{Currency}: {Rate:F2} {arrow} {Math.Abs(Change):F2}";
        }
    }

    public interface IObserver
    {
        string Name { get; }
        void Update(CurrencyRate rate);
        Dictionary<string, int> GetUpdateCount();
    }

    public interface ISubject
    {
        void Attach(IObserver observer, string currency);
        void Detach(IObserver observer, string currency);
        void DetachAll(IObserver observer);
        void Notify(string currency);
    }

    public class CurrencyExchange : ISubject
    {
        private Dictionary<string, List<IObserver>> _observers = new Dictionary<string, List<IObserver>>();
        private Dictionary<string, CurrencyRate> _rates = new Dictionary<string, CurrencyRate>();
        private List<string> _eventLog = new List<string>();
        private Random _random = new Random();

        public CurrencyExchange()
        {
            _rates["USD"] = new CurrencyRate { Currency = "USD", Rate = 95.50, Change = 0 };
            _rates["EUR"] = new CurrencyRate { Currency = "EUR", Rate = 103.20, Change = 0 };
            _rates["GBP"] = new CurrencyRate { Currency = "GBP", Rate = 120.80, Change = 0 };
            _rates["JPY"] = new CurrencyRate { Currency = "JPY", Rate = 0.65, Change = 0 };
            _rates["CNY"] = new CurrencyRate { Currency = "CNY", Rate = 13.20, Change = 0 };
        }

        public void Attach(IObserver observer, string currency)
        {
            if (!_observers.ContainsKey(currency))
                _observers[currency] = new List<IObserver>();

            if (!_observers[currency].Contains(observer))
            {
                _observers[currency].Add(observer);
                LogEvent($"{observer.Name} подписался на {currency}");
            }
        }

        public void Detach(IObserver observer, string currency)
        {
            if (_observers.ContainsKey(currency) && _observers[currency].Contains(observer))
            {
                _observers[currency].Remove(observer);
                LogEvent($"{observer.Name} отписался от {currency}");
            }
        }

        public void DetachAll(IObserver observer)
        {
            foreach (var currency in _observers.Keys.ToList())
            {
                if (_observers[currency].Contains(observer))
                {
                    _observers[currency].Remove(observer);
                }
            }
            LogEvent($"{observer.Name} отписался от всех валют");
        }

        public void Notify(string currency)
        {
            if (_observers.ContainsKey(currency) && _rates.ContainsKey(currency))
            {
                var rate = _rates[currency];
                foreach (var observer in _observers[currency].ToList())
                {
                    observer.Update(rate);
                }
            }
        }

        public void UpdateRate(string currency, double newRate)
        {
            if (_rates.ContainsKey(currency))
            {
                var oldRate = _rates[currency].Rate;
                _rates[currency].Rate = newRate;
                _rates[currency].Change = newRate - oldRate;

                LogEvent($"Курс {currency} изменен: {oldRate:F2} -> {newRate:F2} ({(newRate - oldRate):F2})");
                Notify(currency);
            }
        }

        public void SimulateRandomChange()
        {
            var currencies = _rates.Keys.ToList();
            string currency = currencies[_random.Next(currencies.Count)];

            double changePercent = (_random.NextDouble() * 4) - 2;
            double newRate = _rates[currency].Rate * (1 + changePercent / 100);

            UpdateRate(currency, newRate);
        }

        public void SimulateEconomicNews(string news, string currency, double impact)
        {
            if (_rates.ContainsKey(currency))
            {
                LogEvent($"НОВОСТЬ: {news}");
                double newRate = _rates[currency].Rate * (1 + impact / 100);
                UpdateRate(currency, newRate);
            }
        }

        public CurrencyRate GetRate(string currency)
        {
            return _rates.ContainsKey(currency) ? _rates[currency] : null;
        }

        public List<string> GetAvailableCurrencies()
        {
            return _rates.Keys.ToList();
        }

        private void LogEvent(string message)
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss}: {message}";
            _eventLog.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void ShowEventLog()
        {
            Console.WriteLine("\n=== ЖУРНАЛ СОБЫТИЙ ===");
            foreach (var log in _eventLog)
            {
                Console.WriteLine(log);
            }
        }

        public void ShowAllRates()
        {
            Console.WriteLine("\n=== ТЕКУЩИЕ КУРСЫ ===");
            foreach (var rate in _rates.Values)
            {
                Console.WriteLine(rate);
            }
        }
    }

    public class BankObserver : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _updateCount;
        private double _alertThreshold;

        public BankObserver(string bankName, double threshold = 1.0)
        {
            Name = $"Банк {bankName}";
            _updateCount = new Dictionary<string, int>();
            _alertThreshold = threshold;
        }

        public void Update(CurrencyRate rate)
        {
            if (!_updateCount.ContainsKey(rate.Currency))
                _updateCount[rate.Currency] = 0;

            _updateCount[rate.Currency]++;

            Console.WriteLine($"[{Name}] ПОЛУЧЕНО ОБНОВЛЕНИЕ: {rate}");

            if (Math.Abs(rate.Change) >= _alertThreshold)
            {
                Console.WriteLine($"[{Name}] ВНИМАНИЕ! Значительное изменение курса {rate.Currency}: {rate.Change:F2} руб.");
            }
        }

        public Dictionary<string, int> GetUpdateCount()
        {
            return _updateCount;
        }
    }

    public class TraderObserver : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _updateCount;
        private Dictionary<string, bool> _positions;

        public TraderObserver(string traderName)
        {
            Name = $"Трейдер {traderName}";
            _updateCount = new Dictionary<string, int>();
            _positions = new Dictionary<string, bool>();
        }

        public void Update(CurrencyRate rate)
        {
            if (!_updateCount.ContainsKey(rate.Currency))
                _updateCount[rate.Currency] = 0;

            _updateCount[rate.Currency]++;

            if (!_positions.ContainsKey(rate.Currency))
                _positions[rate.Currency] = false;

            Console.WriteLine($"[{Name}] Анализ {rate.Currency}: {rate.Rate:F2} (изменение: {rate.Change:F2})");

            if (rate.Change > 1.5 && !_positions[rate.Currency])
            {
                Console.WriteLine($"[{Name}] РЕШЕНИЕ: Продавать {rate.Currency} - курс растет");
                _positions[rate.Currency] = true;
            }
            else if (rate.Change < -1.0 && _positions[rate.Currency])
            {
                Console.WriteLine($"[{Name}] РЕШЕНИЕ: Покупать {rate.Currency} - курс падает");
                _positions[rate.Currency] = false;
            }
        }

        public Dictionary<string, int> GetUpdateCount()
        {
            return _updateCount;
        }
    }

    public class NewsAgencyObserver : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _updateCount;
        private List<string> _newsFeed;

        public NewsAgencyObserver(string agencyName)
        {
            Name = $"Агентство {agencyName}";
            _updateCount = new Dictionary<string, int>();
            _newsFeed = new List<string>();
        }

        public void Update(CurrencyRate rate)
        {
            if (!_updateCount.ContainsKey(rate.Currency))
                _updateCount[rate.Currency] = 0;

            _updateCount[rate.Currency]++;

            string news = $"Курс {rate.Currency} изменился до {rate.Rate:F2} руб. ({rate.Change:+0.00;-0.00})";
            _newsFeed.Add(news);

            Console.WriteLine($"[{Name}] НОВОСТЬ: {news}");

            if (_newsFeed.Count >= 3)
            {
                Console.WriteLine($"[{Name}] Последние 3 новости:");
                for (int i = Math.Max(0, _newsFeed.Count - 3); i < _newsFeed.Count; i++)
                {
                    Console.WriteLine($"  {_newsFeed[i]}");
                }
            }
        }

        public Dictionary<string, int> GetUpdateCount()
        {
            return _updateCount;
        }
    }

    public class MobileAppObserver : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _updateCount;
        private string _deviceId;

        public MobileAppObserver(string deviceId)
        {
            Name = $"Моб.Приложение ({deviceId})";
            _deviceId = deviceId;
            _updateCount = new Dictionary<string, int>();
        }

        public void Update(CurrencyRate rate)
        {
            if (!_updateCount.ContainsKey(rate.Currency))
                _updateCount[rate.Currency] = 0;

            _updateCount[rate.Currency]++;

            string direction = rate.Change >= 0 ? "↑" : "↓";
            Console.WriteLine($"[{Name}] УВЕДОМЛЕНИЕ: {rate.Currency} {direction} {Math.Abs(rate.Change):F2} → {rate.Rate:F2} руб.");
        }

        public Dictionary<string, int> GetUpdateCount()
        {
            return _updateCount;
        }
    }

    public class ObserverProgram
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("СИСТЕМА КУРСОВ ВАЛЮТ (ПАТТЕРН НАБЛЮДАТЕЛЬ)");
            Console.WriteLine("===========================================");

            var exchange = new CurrencyExchange();

            var bank1 = new BankObserver("Сбербанк", 1.5);
            var bank2 = new BankObserver("ВТБ", 1.0);
            var trader1 = new TraderObserver("Иванов");
            var trader2 = new TraderObserver("Петров");
            var news1 = new NewsAgencyObserver("РБК");
            var news2 = new NewsAgencyObserver("Интерфакс");
            var mobile1 = new MobileAppObserver("iPhone-001");
            var mobile2 = new MobileAppObserver("Android-001");

            Console.WriteLine("\nПодписка наблюдателей...");

            exchange.Attach(bank1, "USD");
            exchange.Attach(bank1, "EUR");

            exchange.Attach(bank2, "USD");
            exchange.Attach(bank2, "EUR");
            exchange.Attach(bank2, "GBP");

            exchange.Attach(trader1, "USD");
            exchange.Attach(trader1, "EUR");

            exchange.Attach(trader2, "GBP");
            exchange.Attach(trader2, "JPY");

            exchange.Attach(news1, "USD");
            exchange.Attach(news1, "EUR");
            exchange.Attach(news1, "GBP");

            exchange.Attach(news2, "USD");
            exchange.Attach(news2, "CNY");

            exchange.Attach(mobile1, "USD");
            exchange.Attach(mobile1, "EUR");

            exchange.Attach(mobile2, "JPY");
            exchange.Attach(mobile2, "CNY");

            Thread.Sleep(500);

            Console.WriteLine("\nИмитация изменений курсов...");
            Thread.Sleep(500);

            exchange.SimulateRandomChange();
            Thread.Sleep(500);

            exchange.SimulateEconomicNews("Заседание ФРС США", "USD", 0.8);
            Thread.Sleep(500);

            exchange.SimulateRandomChange();
            Thread.Sleep(500);

            exchange.SimulateEconomicNews("Решение ЕЦБ по ставке", "EUR", -0.5);
            Thread.Sleep(500);

            exchange.SimulateRandomChange();
            Thread.Sleep(500);

            Console.WriteLine("\nОтписка одного наблюдателя...");
            exchange.Detach(mobile1, "USD");

            Thread.Sleep(500);

            exchange.SimulateRandomChange();
            Thread.Sleep(500);

            Console.WriteLine("\n=== СТАТИСТИКА ПОЛУЧЕННЫХ ОБНОВЛЕНИЙ ===");
            var observers = new IObserver[] { bank1, bank2, trader1, trader2, news1, news2, mobile1, mobile2 };

            foreach (var observer in observers)
            {
                Console.WriteLine($"\n{observer.Name}:");
                var counts = observer.GetUpdateCount();
                if (counts.Count > 0)
                {
                    foreach (var pair in counts)
                    {
                        Console.WriteLine($"  {pair.Key}: {pair.Value} обновлений");
                    }
                }
                else
                {
                    Console.WriteLine("  Нет обновлений");
                }
            }

            exchange.ShowAllRates();
            exchange.ShowEventLog();

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}