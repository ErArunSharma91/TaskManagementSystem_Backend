namespace TaskManagementSystem.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Stores the hashed password
        public string Email { get; set; } // New property for email
    }
}
