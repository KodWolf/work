using System.Text;

namespace DecoratorPattern
{
    public class SalesReport : IReport
    {
        private List<Sale> _sales;

        public SalesReport()
        {
            GenerateFakeData();
        }

        private void GenerateFakeData()
        {
            _sales = new List<Sale>
            {
                new Sale { Id = 1, ProductName = "Ноутбук", Amount = 75000, Date = new DateTime(2024, 3, 15), Customer = "Иванов И.И." },
                new Sale { Id = 2, ProductName = "Смартфон", Amount = 45000, Date = new DateTime(2024, 3, 16), Customer = "Петров П.П." },
                new Sale { Id = 3, ProductName = "Наушники", Amount = 5000, Date = new DateTime(2024, 3, 10), Customer = "Сидоров С.С." },
                new Sale { Id = 4, ProductName = "Планшет", Amount = 35000, Date = new DateTime(2024, 3, 18), Customer = "Козлова А.А." },
                new Sale { Id = 5, ProductName = "Монитор", Amount = 25000, Date = new DateTime(2024, 3, 5), Customer = "Морозов М.М." },
                new Sale { Id = 6, ProductName = "Клавиатура", Amount = 3000, Date = new DateTime(2024, 3, 12), Customer = "Иванов И.И." },
                new Sale { Id = 7, ProductName = "Мышь", Amount = 1500, Date = new DateTime(2024, 3, 14), Customer = "Петров П.П." }
            };
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            sb.AppendLine("ОТЧЕТ ПО ПРОДАЖАМ");
            sb.AppendLine("-----------------");
            sb.AppendLine("ID  Товар        Сумма    Дата        Покупатель");

            foreach (var sale in _sales)
            {
                sb.AppendLine($"{sale.Id,-3} {sale.ProductName,-10} {sale.Amount,8}  {sale.Date.ToShortString(),-10} {sale.Customer}");
            }

            return sb.ToString();
        }

        public List<Sale> GetSales() => _sales;

        public class Sale
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
            public string Customer { get; set; }
        }
    }

    // Extension method для красивого вывода даты
    public static class DateTimeExtensions
    {
        public static string ToShortString(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}