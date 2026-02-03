namespace DesignPrinciplesDemo.YAGNI
{
    public class SimpleFileReader
    {
        public string ReadFile(string filePath)
        {
            Console.WriteLine($"Чтение файла: {filePath}");
            // Простая логика чтения файла
            return "file content";
        }
    }
}