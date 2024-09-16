using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;  // Use interface IAuthService
        private readonly TaskManagementDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(IAuthService authService, TaskManagementDbContext context, IConfiguration configuration)
        {
            _authService = authService;
            _context = context;
            _configuration = configuration;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto request)
        {
            if (_context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
            {
                return BadRequest("Username or email already exists");
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = _authService.HashPassword(request.Password),
                Email = request.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        // POST: api/User/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            var user = _context.Users
                .SingleOrDefault(u => u.Username == request.Username || u.Email == request.Email);

            if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username, email, or password");
            }

            // Authentication successful, generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
