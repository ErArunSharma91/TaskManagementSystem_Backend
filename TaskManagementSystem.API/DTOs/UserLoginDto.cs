namespace TaskManagementSystem.API.DTOs
{
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } // Added email property
    }
}
