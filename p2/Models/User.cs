namespace UserManagementSystem.Models
{
    // Класс пользователя - максимально простой (KISS)
    public class User
    {
        // Только необходимые свойства (YAGNI)
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Простое представление (KISS)
        public override string ToString()
        {
            return $"{Name} ({Email}) - {Role}";
        }
    }
}