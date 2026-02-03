namespace DesignPrinciplesDemo.YAGNI
{
    public class UserRepository
    {
        public void SaveToDatabase(User user)
        {
            Console.WriteLine($"Сохранение пользователя {user.Name} в БД");

        }
    }
}