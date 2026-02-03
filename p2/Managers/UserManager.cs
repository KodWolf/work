using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementSystem.Models;

namespace UserManagementSystem.Managers
{
    public class UserManager
    {
        // Простое хранение в списке (KISS)
        private List<User> _users = new List<User>();

        // Только необходимые методы (YAGNI)

        // Добавление пользователя
        public void AddUser(string name, string email, string role)
        {
            // Валидация - вынесена в отдельный метод (DRY)
            ValidateInput(name, email, role);

            // Проверка уникальности email
            if (FindUserByEmail(email) != null)
            {
                Console.WriteLine($"Ошибка: пользователь с email {email} уже существует.");
                return;
            }

            _users.Add(new User
            {
                Name = name,
                Email = email,
                Role = role
            });

            Console.WriteLine($"✅ Пользователь '{name}' добавлен.");
        }

        // Удаление пользователя по email
        public void RemoveUser(string email)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {
                _users.Remove(user);
                Console.WriteLine($"✅ Пользователь '{user.Name}' удален.");
            }
            else
            {
                Console.WriteLine($"❌ Пользователь с email {email} не найден.");
            }
        }

        // Обновление пользователя
        public void UpdateUser(string email, string newName = null, string newRole = null)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {
                // Обновляем только переданные параметры (KISS)
                if (!string.IsNullOrEmpty(newName))
                {
                    user.Name = newName;
                }
                if (!string.IsNullOrEmpty(newRole))
                {
                    user.Role = newRole;
                }
                Console.WriteLine($"✅ Пользователь обновлен: {user}");
            }
            else
            {
                Console.WriteLine($"❌ Пользователь с email {email} не найден.");
            }
        }

        // Вывод всех пользователей (для демонстрации)
        public void PrintAllUsers()
        {
            if (_users.Count == 0)
            {
                Console.WriteLine("Список пользователей пуст.");
                return;
            }

            Console.WriteLine($"Всего пользователей: {_users.Count}");
            foreach (var user in _users)
            {
                Console.WriteLine($"  - {user}");
            }
        }

        // ============ Вспомогательные методы (DRY) ============

        // Поиск пользователя по email
        private User FindUserByEmail(string email)
        {
            // Простой линейный поиск (KISS) - достаточно для небольшого количества пользователей
            return _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Валидация входных данных
        private void ValidateInput(string name, string email, string role)
        {
            // Минимальная валидация (KISS) - только самое необходимое
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя пользователя не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Роль не может быть пустой.");
            }

            // Простая проверка email
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Некорректный email адрес.");
            }
        }
    }
}