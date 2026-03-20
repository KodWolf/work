namespace DecoratorPattern
{
    public class PdfExportDecorator : ReportDecorator
    {
        private string _filename;

        public PdfExportDecorator(IReport report, string filename) : base(report)
        {
            _filename = filename;
        }

        public override string Generate()
        {
            var reportContent = _report.Generate();

            // Симуляция создания PDF
            Console.WriteLine($"\n[Экспорт] PDF файл создан: {_filename} (симуляция)");

            return reportContent + $"\n[PDF: {_filename}]";
        }
    }
}