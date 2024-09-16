using BCrypt.Net;  // Import BCrypt for password hashing

namespace TaskManagementSystem.API.Services
{
    // Interface definition for AuthService
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

    // Implementation of AuthService
    public class AuthService : IAuthService
    {
        // Hashes the password before storing it in the database
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verifies if the input password matches the stored hashed password
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
