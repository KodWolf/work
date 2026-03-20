using System.Text;

namespace DecoratorPattern
{
    public class DateFilterDecorator : ReportDecorator
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate) : base(report)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override string Generate()
        {
            if (_report is SalesReport salesReport)
            {
                var sales = salesReport.GetSales();
                var filteredSales = sales.Where(s => s.Date >= _startDate && s.Date <= _endDate).ToList();

                var sb = new StringBuilder();
                sb.AppendLine($"ОТЧЕТ ПО ПРОДАЖАМ (фильтр: {_startDate.ToShortString()} - {_endDate.ToShortString()})");
                sb.AppendLine(new string('-', 60));
                sb.AppendLine("ID  Товар        Сумма    Дата        Покупатель");

                foreach (var sale in filteredSales)
                {
                    sb.AppendLine($"{sale.Id,-3} {sale.ProductName,-10} {sale.Amount,8}  {sale.Date.ToShortString(),-10} {sale.Customer}");
                }

                sb.AppendLine($"\nВсего записей: {filteredSales.Count} из {sales.Count}");

                return sb.ToString();
            }

            return _report.Generate() + $"\n[Применен фильтр по датам]";
        }
    }
}