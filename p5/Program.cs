using System;
using System.IO;
using System.Threading;
using System.Text.Json;

public enum LogLevel
{
    INFO,
    WARNING,
    ERROR
}

public class LoggerConfig
{
    public LogLevel CurrentLevel { get; set; }
    public string LogFilePath { get; set; }
}

public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new object();
    private LogLevel _currentLevel;
    private string _logFilePath;

    private Logger()
    {
  
        try
        {
            if (File.Exists("logger_config.json"))
            {
                string json = File.ReadAllText("logger_config.json");
                LoggerConfig config = JsonSerializer.Deserialize<LoggerConfig>(json);
                _currentLevel = config.CurrentLevel;
                _logFilePath = config.LogFilePath;
            }
            else
            {
                _currentLevel = LogLevel.INFO;
                _logFilePath = "logs.txt";
            }
        }
        catch
        {
            _currentLevel = LogLevel.INFO;
            _logFilePath = "logs.txt";
        }
    }

    public static Logger GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }
    }

    public void SetLogLevel(LogLevel level)
    {
        lock (_lock)
        {
            _currentLevel = level;
        }
    }

    public void Log(string message, LogLevel level)
    {
        if (level >= _currentLevel)
        {
            lock (_lock)
            {
                try
                {
                    string logEntry = $"{DateTime.Now} [{level}] {message}";
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
                catch { }
            }
        }
    }

    public string LogFilePath => _logFilePath;
}


public class LogReader
{
    private Logger _logger;

    public LogReader()
    {
        _logger = Logger.GetInstance();
    }

    public void ReadAndDisplayLogs(LogLevel? filterLevel = null)
    {
        try
        {
            if (!File.Exists(_logger.LogFilePath))
            {
                Console.WriteLine("Файл логов не найден");
                return;
            }

            string[] lines = File.ReadAllLines(_logger.LogFilePath);

            foreach (string line in lines)
            {
                if (filterLevel.HasValue)
                {
                    if (line.Contains($"[{filterLevel.Value}]"))
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch { }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тестирование Logger ===\n");

        var config = new LoggerConfig { CurrentLevel = LogLevel.INFO, LogFilePath = "test.log" };
        File.WriteAllText("logger_config.json", JsonSerializer.Serialize(config));

        Logger logger = Logger.GetInstance();

        Console.WriteLine("Тест 1: Запись сообщений");
        logger.Log("Запуск программы", LogLevel.INFO);
        logger.Log("Предупреждение", LogLevel.WARNING);
        logger.Log("Ошибка", LogLevel.ERROR);

        Console.WriteLine("\nТест 2: Смена уровня на WARNING");
        logger.SetLogLevel(LogLevel.WARNING);
        logger.Log("INFO сообщение", LogLevel.INFO);
        logger.Log("WARNING сообщение", LogLevel.WARNING);

        Console.WriteLine("\nТест 3: Многопоточная запись");
        Thread[] threads = new Thread[3];

        for (int i = 0; i < 3; i++)
        {
            int threadNum = i;
            threads[i] = new Thread(() =>
            {
                Logger log = Logger.GetInstance();
                for (int j = 0; j < 2; j++)
                {
                    log.Log($"Поток {threadNum} сообщение {j}", LogLevel.INFO);
                    Thread.Sleep(10);
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads) t.Join();

        Console.WriteLine("\nТест 4: Чтение всех логов");
        LogReader reader = new LogReader();
        reader.ReadAndDisplayLogs();

        Console.WriteLine("\nТест 5: Фильтр по ERROR");
        reader.ReadAndDisplayLogs(LogLevel.ERROR);

        Console.WriteLine("\nГотово");
        Console.ReadKey();
    }
}