namespace DecoratorPattern
{
    public class CsvExportDecorator : ReportDecorator
    {
        private string _filename;

        public CsvExportDecorator(IReport report, string filename) : base(report)
        {
            _filename = filename;
        }

        public override string Generate()
        {
            var reportContent = _report.Generate();

            try
            {
                File.WriteAllText(_filename, reportContent);
                Console.WriteLine($"\n[Экспорт] Отчет сохранен в файл: {_filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Ошибка экспорта] {ex.Message}");
            }

            return reportContent + $"\n[Файл: {_filename}]";
        }
    }
}