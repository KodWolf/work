using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StockSystem
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    double oldPrice = _price;
                    _price = value;
                    OnPriceChanged(oldPrice);
                }
            }
        }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        protected virtual void OnPriceChanged(double oldPrice)
        {
            PriceChanged?.Invoke(this, new PriceChangedEventArgs
            {
                Symbol = this.Symbol,
                OldPrice = oldPrice,
                NewPrice = this.Price
            });
        }
    }

    public class PriceChangedEventArgs : EventArgs
    {
        public string Symbol { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
    }

    public interface IObserver
    {
        string Name { get; }
        void Update(Stock stock, PriceChangedEventArgs e);
        List<string> GetSubscribedStocks();
        int GetNotificationCount(string symbol);
    }

    public interface ISubject
    {
        void Attach(IObserver observer, string stockSymbol);
        void Detach(IObserver observer, string stockSymbol);
        void DetachAll(IObserver observer);
        void Notify(Stock stock, PriceChangedEventArgs e);
    }

    public class StockExchange : ISubject
    {
        private Dictionary<string, List<IObserver>> _observers = new Dictionary<string, List<IObserver>>();
        private Dictionary<string, Stock> _stocks = new Dictionary<string, Stock>();
        private List<string> _eventLog = new List<string>();
        private Random _random = new Random();

        public void AddStock(Stock stock)
        {
            if (!_stocks.ContainsKey(stock.Symbol))
            {
                _stocks[stock.Symbol] = stock;
                stock.PriceChanged += OnStockPriceChanged;
                LogEvent($"Акция {stock.Symbol} добавлена");
            }
        }

        private void OnStockPriceChanged(object sender, PriceChangedEventArgs e)
        {
            var stock = sender as Stock;
            Notify(stock, e);
        }

        public void Attach(IObserver observer, string stockSymbol)
        {
            if (!_observers.ContainsKey(stockSymbol))
                _observers[stockSymbol] = new List<IObserver>();

            if (!_observers[stockSymbol].Contains(observer))
            {
                _observers[stockSymbol].Add(observer);
                LogEvent($"{observer.Name} подписался на {stockSymbol}");
            }
        }

        public void Detach(IObserver observer, string stockSymbol)
        {
            if (_observers.ContainsKey(stockSymbol) && _observers[stockSymbol].Contains(observer))
            {
                _observers[stockSymbol].Remove(observer);
                LogEvent($"{observer.Name} отписался от {stockSymbol}");
            }
        }

        public void DetachAll(IObserver observer)
        {
            foreach (var symbol in _observers.Keys.ToList())
            {
                if (_observers[symbol].Contains(observer))
                {
                    _observers[symbol].Remove(observer);
                }
            }
            LogEvent($"{observer.Name} отписался от всех акций");
        }

        public void Notify(Stock stock, PriceChangedEventArgs e)
        {
            string symbol = stock.Symbol;
            if (_observers.ContainsKey(symbol))
            {
                foreach (var observer in _observers[symbol].ToList())
                {
                    Task.Run(() => observer.Update(stock, e));
                }
            }
        }

        public void UpdateStockPrice(string symbol, double newPrice)
        {
            if (_stocks.ContainsKey(symbol))
            {
                _stocks[symbol].Price = newPrice;
                LogEvent($"Цена {symbol} изменена на {newPrice:C}");
            }
        }

        public void SimulateNewsEvent(string symbol, string news, double impact)
        {
            if (_stocks.ContainsKey(symbol))
            {
                var stock = _stocks[symbol];
                double newPrice = stock.Price * (1 + impact / 100);
                LogEvent($"НОВОСТЬ: {news} - Влияние: {impact}% на {symbol}");
                UpdateStockPrice(symbol, newPrice);
            }
        }

        public void SimulateEarningsReport(string symbol, double actualEarnings, double expectedEarnings)
        {
            if (_stocks.ContainsKey(symbol))
            {
                double surprise = ((actualEarnings - expectedEarnings) / expectedEarnings) * 100;
                double priceImpact = surprise * 0.5;
                LogEvent($"ОТЧЕТ: {symbol} - Факт: ${actualEarnings}, Ожидание: ${expectedEarnings}, Сюрприз: {surprise:F2}%");
                UpdateStockPrice(symbol, _stocks[symbol].Price * (1 + priceImpact / 100));
            }
        }

        public Stock GetStock(string symbol)
        {
            return _stocks.ContainsKey(symbol) ? _stocks[symbol] : null;
        }

        public List<Stock> GetAllStocks()
        {
            return _stocks.Values.ToList();
        }

        private void LogEvent(string message)
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss}: {message}";
            _eventLog.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void GenerateSubscribersReport()
        {
            Console.WriteLine("\n=== ОТЧЕТ ПО ПОДПИСЧИКАМ ===");
            foreach (var stock in _stocks.Values)
            {
                Console.WriteLine($"\n{stock.Symbol} - {stock.CompanyName} (${stock.Price:F2})");
                if (_observers.ContainsKey(stock.Symbol) && _observers[stock.Symbol].Count > 0)
                {
                    foreach (var observer in _observers[stock.Symbol])
                    {
                        int count = observer.GetNotificationCount(stock.Symbol);
                        Console.WriteLine($"  - {observer.Name}: {count} уведомлений");
                    }
                }
                else
                {
                    Console.WriteLine("  Нет подписчиков");
                }
            }
            Console.WriteLine("=============================");
        }

        public void ShowEventLog()
        {
            Console.WriteLine("\n=== ЖУРНАЛ СОБЫТИЙ ===");
            foreach (var log in _eventLog)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine("========================");
        }
    }

    public class Trader : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _notificationCount;
        private double _minChangePercent;

        public Trader(string name, double minChangePercent = 0)
        {
            Name = name;
            _notificationCount = new Dictionary<string, int>();
            _minChangePercent = minChangePercent;
        }

        public void Update(Stock stock, PriceChangedEventArgs e)
        {
            double changePercent = ((e.NewPrice - e.OldPrice) / e.OldPrice) * 100;

            if (Math.Abs(changePercent) >= _minChangePercent)
            {
                if (!_notificationCount.ContainsKey(stock.Symbol))
                    _notificationCount[stock.Symbol] = 0;

                _notificationCount[stock.Symbol]++;

                string direction = changePercent > 0 ? "ВВЕРХ" : "ВНИЗ";
                Console.WriteLine($"[{Name}] {stock.Symbol} {direction} {Math.Abs(changePercent):F2}%: ${e.OldPrice:F2} -> ${e.NewPrice:F2}");
            }
        }

        public List<string> GetSubscribedStocks()
        {
            return _notificationCount.Keys.ToList();
        }

        public int GetNotificationCount(string symbol)
        {
            return _notificationCount.ContainsKey(symbol) ? _notificationCount[symbol] : 0;
        }
    }

    public class TradingRobot : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _notificationCount;
        private double _buyThreshold;
        private double _sellThreshold;
        private Dictionary<string, bool> _holdings;

        public TradingRobot(string name, double buyThreshold, double sellThreshold)
        {
            Name = name;
            _notificationCount = new Dictionary<string, int>();
            _buyThreshold = buyThreshold;
            _sellThreshold = sellThreshold;
            _holdings = new Dictionary<string, bool>();
        }

        public void Update(Stock stock, PriceChangedEventArgs e)
        {
            if (!_notificationCount.ContainsKey(stock.Symbol))
                _notificationCount[stock.Symbol] = 0;

            _notificationCount[stock.Symbol]++;

            if (!_holdings.ContainsKey(stock.Symbol))
                _holdings[stock.Symbol] = false;

            Console.WriteLine($"[{Name}] Анализ {stock.Symbol} по цене ${e.NewPrice:F2}");

            if (!_holdings[stock.Symbol] && e.NewPrice < _buyThreshold)
            {
                Console.WriteLine($"[{Name}] ПОКУПКА {stock.Symbol} по ${e.NewPrice:F2}");
                _holdings[stock.Symbol] = true;
            }
            else if (_holdings[stock.Symbol] && e.NewPrice > _sellThreshold)
            {
                Console.WriteLine($"[{Name}] ПРОДАЖА {stock.Symbol} по ${e.NewPrice:F2}");
                _holdings[stock.Symbol] = false;
            }
        }

        public List<string> GetSubscribedStocks()
        {
            return _notificationCount.Keys.ToList();
        }

        public int GetNotificationCount(string symbol)
        {
            return _notificationCount.ContainsKey(symbol) ? _notificationCount[symbol] : 0;
        }
    }

    public class MobileApp : IObserver
    {
        public string Name { get; private set; }
        private Dictionary<string, int> _notificationCount;
        private string _deviceId;

        public MobileApp(string deviceId)
        {
            Name = $"МобПрил-{deviceId}";
            _deviceId = deviceId;
            _notificationCount = new Dictionary<string, int>();
        }

        public void Update(Stock stock, PriceChangedEventArgs e)
        {
            if (!_notificationCount.ContainsKey(stock.Symbol))
                _notificationCount[stock.Symbol] = 0;

            _notificationCount[stock.Symbol]++;

            double changePercent = ((e.NewPrice - e.OldPrice) / e.OldPrice) * 100;
            Console.WriteLine($"[{Name}] УВЕДОМЛЕНИЕ: {stock.Symbol} {changePercent:F2}% -> ${e.NewPrice:F2}");
        }

        public List<string> GetSubscribedStocks()
        {
            return _notificationCount.Keys.ToList();
        }

        public int GetNotificationCount(string symbol)
        {
            return _notificationCount.ContainsKey(symbol) ? _notificationCount[symbol] : 0;
        }
    }

    public class StockProgram
    {
        public static async Task Run()
        {
            Console.Clear();
            Console.WriteLine("БИРЖЕВАЯ СИСТЕМА");
            Console.WriteLine("================");

            var exchange = new StockExchange();

            exchange.AddStock(new Stock { Symbol = "AAPL", CompanyName = "Apple Inc.", Price = 175.50 });
            exchange.AddStock(new Stock { Symbol = "MSFT", CompanyName = "Microsoft Corp.", Price = 330.25 });
            exchange.AddStock(new Stock { Symbol = "GOOGL", CompanyName = "Alphabet Inc.", Price = 2800.00 });
            exchange.AddStock(new Stock { Symbol = "AMZN", CompanyName = "Amazon.com Inc.", Price = 3450.00 });
            exchange.AddStock(new Stock { Symbol = "TSLA", CompanyName = "Tesla Inc.", Price = 750.80 });

            var trader1 = new Trader("Иван (Трейдер)", 2.0);
            var trader2 = new Trader("Мария (Трейдер)");
            var robot1 = new TradingRobot("Робот-1", 700.00, 800.00);
            var robot2 = new TradingRobot("Робот-2", 320.00, 350.00);
            var mobile1 = new MobileApp("iPhone-123");
            var mobile2 = new MobileApp("Android-456");

            Console.WriteLine("\nПодписка наблюдателей...");
            exchange.Attach(trader1, "AAPL");
            exchange.Attach(trader1, "MSFT");
            exchange.Attach(trader1, "GOOGL");

            exchange.Attach(trader2, "AMZN");
            exchange.Attach(trader2, "TSLA");

            exchange.Attach(robot1, "TSLA");

            exchange.Attach(robot2, "AAPL");
            exchange.Attach(robot2, "MSFT");

            exchange.Attach(mobile1, "AAPL");
            exchange.Attach(mobile1, "GOOGL");
            exchange.Attach(mobile1, "TSLA");

            exchange.Attach(mobile2, "AMZN");
            exchange.Attach(mobile2, "MSFT");

            await Task.Delay(500);

            Console.WriteLine("\nИмитация биржевой активности...");

            exchange.SimulateNewsEvent("AAPL", "Запуск нового iPhone", 3.5);
            await Task.Delay(1000);

            exchange.SimulateEarningsReport("MSFT", 2.75, 2.50);
            await Task.Delay(1000);

            exchange.SimulateNewsEvent("TSLA", "Задержки производства", -2.8);
            await Task.Delay(1000);

            exchange.SimulateEarningsReport("AMZN", 15.20, 14.80);
            await Task.Delay(1000);

            exchange.SimulateNewsEvent("GOOGL", "Антимонопольное расследование", -1.5);
            await Task.Delay(1000);

            Console.WriteLine("\nОтписка наблюдателя...");
            exchange.Detach(mobile1, "TSLA");

            await Task.Delay(500);

            exchange.GenerateSubscribersReport();
            exchange.ShowEventLog();

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}