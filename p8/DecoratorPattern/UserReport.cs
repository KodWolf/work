using System.Text;

namespace DecoratorPattern
{
    public class UserReport : IReport
    {
        private List<User> _users;

        public UserReport()
        {
            GenerateFakeData();
        }

        private void GenerateFakeData()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Иванов Иван", Email = "ivanov@mail.com", RegistrationDate = new DateTime(2024, 1, 15), TotalPurchases = 5, TotalSpent = 125000, City = "Москва", Age = 35 },
                new User { Id = 2, Name = "Петров Петр", Email = "petrov@mail.com", RegistrationDate = new DateTime(2024, 2, 20), TotalPurchases = 3, TotalSpent = 65000, City = "СПб", Age = 28 },
                new User { Id = 3, Name = "Сидорова Анна", Email = "sidorova@mail.com", RegistrationDate = new DateTime(2024, 3, 1), TotalPurchases = 2, TotalSpent = 40000, City = "Москва", Age = 42 },
                new User { Id = 4, Name = "Козлова Елена", Email = "kozlova@mail.com", RegistrationDate = new DateTime(2024, 3, 10), TotalPurchases = 1, TotalSpent = 35000, City = "Казань", Age = 31 },
                new User { Id = 5, Name = "Смирнов Алексей", Email = "smirnov@mail.com", RegistrationDate = new DateTime(2024, 2, 5), TotalPurchases = 4, TotalSpent = 82000, City = "Москва", Age = 45 },
                new User { Id = 6, Name = "Волкова Мария", Email = "volkova@mail.com", RegistrationDate = new DateTime(2024, 1, 20), TotalPurchases = 6, TotalSpent = 156000, City = "СПб", Age = 29 }
            };
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            sb.AppendLine("ОТЧЕТ ПО ПОЛЬЗОВАТЕЛЯМ");
            sb.AppendLine("----------------------");
            sb.AppendLine("ID  Имя            Email                 Регистрация  Покупок  Сумма   Город  Возраст");

            foreach (var user in _users)
            {
                sb.AppendLine($"{user.Id,-3} {user.Name,-13} {user.Email,-20} {user.RegistrationDate.ToShortString(),-11} {user.TotalPurchases,-7} {user.TotalSpent,6}  {user.City,-5} {user.Age}");
            }

            return sb.ToString();
        }

        public List<User> GetUsers() => _users;

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime RegistrationDate { get; set; }
            public int TotalPurchases { get; set; }
            public decimal TotalSpent { get; set; }
            public string City { get; set; }
            public int Age { get; set; }
        }
    }
}
