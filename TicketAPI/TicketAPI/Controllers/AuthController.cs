using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketAPI.Data;
using TicketAPI.Models;
using TicketAPI.Services;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthController(TicketingSystemDbContext context, PasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUtilisateurDTO loginDto)
        {
            // Retrieve user by email
            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (utilisateur == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Compare the password using BCrypt.Verify
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, utilisateur.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, utilisateur.UtilisateurId.ToString()),
                    new Claim(ClaimTypes.Name, utilisateur.Email),
                    new Claim(ClaimTypes.Role, utilisateur.Role.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

    }
}
