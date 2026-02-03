namespace UserManagementSystem.Models
{

    public class User
    {
 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


        public override string ToString()
        {
            return $"{Name} ({Email}) - {Role}";
        }
    }
}