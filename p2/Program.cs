using System;
using UserManagementSystem.Models;
using UserManagementSystem.Managers;

namespace UserManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления пользователями ===\n");

            var userManager = new UserManager();

            // Демонстрация добавления пользователей
            Console.WriteLine("1. Добавление пользователей:");
            userManager.AddUser("Иван Иванов", "ivan@example.com", "Admin");
            userManager.AddUser("Петр Петров", "petr@example.com", "User");
            userManager.AddUser("Анна Сидорова", "anna@example.com", "User");

            // Вывод всех пользователей
            Console.WriteLine("\n2. Все пользователи:");
            userManager.PrintAllUsers();

            // Демонстрация обновления
            Console.WriteLine("\n3. Обновление пользователя:");
            userManager.UpdateUser("petr@example.com", newName: "Петр Сергеев");
            userManager.UpdateUser("anna@example.com", newRole: "Manager");

            // Вывод после обновления
            Console.WriteLine("\n4. Пользователи после обновления:");
            userManager.PrintAllUsers();

            // Демонстрация удаления
            Console.WriteLine("\n5. Удаление пользователя:");
            userManager.RemoveUser("ivan@example.com");

            // Вывод после удаления
            Console.WriteLine("\n6. Пользователи после удаления:");
            userManager.PrintAllUsers();

            // Попытка удаления несуществующего пользователя
            Console.WriteLine("\n7. Попытка удаления несуществующего пользователя:");
            userManager.RemoveUser("unknown@example.com");

            Console.WriteLine("\n=== Программа завершена ===");
        }
    }
}