using System;

namespace DesignPrinciplesDemo.DRY
{
    public class Logger
    {
        public void Log(string level, string message)
        {
            Console.WriteLine($"{level.ToUpper()}: {message}");
        }

        public void LogError(string message) => Log("ERROR", message);
        public void LogWarning(string message) => Log("WARNING", message);
        public void LogInfo(string message) => Log("INFO", message);
    }
}