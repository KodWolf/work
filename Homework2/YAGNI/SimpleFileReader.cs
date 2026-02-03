namespace DesignPrinciplesDemo.YAGNI
{
    public class SimpleFileReader
    {
        public string ReadFile(string filePath)
        {
            Console.WriteLine($"Чтение файла: {filePath}");

            return "file content";
        }
    }
}