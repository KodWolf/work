using System.Text;

namespace DecoratorPattern
{
    public class SortingDecorator : ReportDecorator
    {
        private string _sortBy;
        private bool _ascending;

        public SortingDecorator(IReport report, string sortBy, bool ascending = true) : base(report)
        {
            _sortBy = sortBy;
            _ascending = ascending;
        }

        public override string Generate()
        {
            if (_report is SalesReport salesReport)
            {
                var sales = salesReport.GetSales();
                IEnumerable<SalesReport.Sale> sortedSales = _sortBy.ToLower() switch
                {
                    "сумма" => _ascending ? sales.OrderBy(s => s.Amount) : sales.OrderByDescending(s => s.Amount),
                    "дата" => _ascending ? sales.OrderBy(s => s.Date) : sales.OrderByDescending(s => s.Date),
                    "товар" => _ascending ? sales.OrderBy(s => s.ProductName) : sales.OrderByDescending(s => s.ProductName),
                    _ => sales
                };

                var sb = new StringBuilder();
                sb.AppendLine($"ОТЧЕТ ПО ПРОДАЖАМ (сортировка по: {_sortBy} {(_ascending ? "↑" : "↓")})");
                sb.AppendLine(new string('-', 60));
                sb.AppendLine("ID  Товар        Сумма    Дата        Покупатель");

                foreach (var sale in sortedSales)
                {
                    sb.AppendLine($"{sale.Id,-3} {sale.ProductName,-10} {sale.Amount,8}  {sale.Date.ToShortString(),-10} {sale.Customer}");
                }

                return sb.ToString();
            }

            return _report.Generate();
        }
    }
}