using System.Text;

namespace DecoratorPattern
{
    public class CustomerFilterDecorator : ReportDecorator
    {
        private string _city;
        private int? _minAge;
        private int? _maxAge;
        private decimal? _minSpent;

        public CustomerFilterDecorator(IReport report, string city = null, int? minAge = null, int? maxAge = null, decimal? minSpent = null)
            : base(report)
        {
            _city = city;
            _minAge = minAge;
            _maxAge = maxAge;
            _minSpent = minSpent;
        }

        public override string Generate()
        {
            if (_report is UserReport userReport)
            {
                var users = userReport.GetUsers();

                var filteredUsers = users.Where(u =>
                    (string.IsNullOrEmpty(_city) || u.City.Equals(_city, StringComparison.OrdinalIgnoreCase)) &&
                    (!_minAge.HasValue || u.Age >= _minAge) &&
                    (!_maxAge.HasValue || u.Age <= _maxAge) &&
                    (!_minSpent.HasValue || u.TotalSpent >= _minSpent)
                ).ToList();

                var conditions = new List<string>();
                if (!string.IsNullOrEmpty(_city)) conditions.Add($"город: {_city}");
                if (_minAge.HasValue) conditions.Add($"возраст >= {_minAge}");
                if (_maxAge.HasValue) conditions.Add($"возраст <= {_maxAge}");
                if (_minSpent.HasValue) conditions.Add($"потрачено >= {_minSpent}");

                var sb = new StringBuilder();
                sb.AppendLine($"ОТЧЕТ ПО ПОЛЬЗОВАТЕЛЯМ (фильтр: {string.Join(", ", conditions)})");
                sb.AppendLine(new string('-', 80));
                sb.AppendLine("ID  Имя            Email                 Регистрация  Покупок  Сумма   Город  Возраст");

                foreach (var user in filteredUsers)
                {
                    sb.AppendLine($"{user.Id,-3} {user.Name,-13} {user.Email,-20} {user.RegistrationDate.ToShortString(),-11} {user.TotalPurchases,-7} {user.TotalSpent,6}  {user.City,-5} {user.Age}");
                }

                sb.AppendLine($"\nВсего записей: {filteredUsers.Count} из {users.Count}");

                return sb.ToString();
            }

            return _report.Generate();
        }
    }
}