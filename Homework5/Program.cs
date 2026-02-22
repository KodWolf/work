using System;
using System.Collections.Generic;
using System.IO;

public sealed class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, string> _settings;

    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
    }

    public static ConfigurationManager GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
            }
        }
        return _instance;
    }

    public void LoadSettings(Dictionary<string, string> settings)
    {
        _settings = settings;
    }

    public string GetSetting(string key)
    {
        if (_settings.ContainsKey(key))
            return _settings[key];
        else
            return null;
    }

    public void SetSetting(string key, string value)
    {
        _settings[key] = value;
    }

    public void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var kvp in _settings)
            {
                writer.WriteLine($"{kvp.Key}={kvp.Value}");
            }
        }
    }

    public void LoadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            var settings = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    settings[parts[0]] = parts[1];
                }
            }
            _settings = settings;
        }
    }
}

class Program
{
    static void Main()
    {
        ConfigurationManager config1 = ConfigurationManager.GetInstance();
        ConfigurationManager config2 = ConfigurationManager.GetInstance();

        Console.WriteLine($"Один экземпляр? {config1 == config2}");

        Dictionary<string, string> settings = new Dictionary<string, string>
        {
            { "app_name", "MyApp" },
            { "version", "1.0" }
        };

        config1.LoadSettings(settings);
        config1.SaveToFile("config.txt");

        config2.LoadFromFile("config.txt");
        Console.WriteLine(config2.GetSetting("app_name"));
    }
}