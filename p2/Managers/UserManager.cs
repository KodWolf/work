using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementSystem.Models;

namespace UserManagementSystem.Managers
{
    public class UserManager
    {

        private List<User> _users = new List<User>();




        public void AddUser(string name, string email, string role)
        {

            ValidateInput(name, email, role);


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

            Console.WriteLine($" Пользователь '{name}' добавлен.");
        }


        public void RemoveUser(string email)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {
                _users.Remove(user);
                Console.WriteLine($" Пользователь '{user.Name}' удален.");
            }
            else
            {
                Console.WriteLine($" Пользователь с email {email} не найден.");
            }
        }


        public void UpdateUser(string email, string newName = null, string newRole = null)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(newName))
                {
                    user.Name = newName;
                }
                if (!string.IsNullOrEmpty(newRole))
                {
                    user.Role = newRole;
                }
                Console.WriteLine($" Пользователь обновлен: {user}");
            }
            else
            {
                Console.WriteLine($" Пользователь с email {email} не найден.");
            }
        }


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


        private User FindUserByEmail(string email)
        {
            return _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

  
        private void ValidateInput(string name, string email, string role)
        {

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


            if (!email.Contains("@"))
            {
                throw new ArgumentException("Некорректный email адрес.");
            }
        }
    }
}