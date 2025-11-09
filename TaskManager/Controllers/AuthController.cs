using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Registro: body deve conter Nome, Email e Senha (texto simples)
        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario user)
        {
            if (user == null) return BadRequest("Dados inválidos.");

            if (_context.Usuarios.Any(u => u.Email == user.Email))
                return BadRequest("Usuário já cadastrado.");

            // Hash da senha com BCrypt
            user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.SenhaHash);
            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }

        // Login: body deve conter Email e Senha (texto)
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario login)
        {
            if (login == null) return BadRequest("Dados inválidos.");

            var dbUser = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);
            if (dbUser == null) return Unauthorized("Credenciais inválidas.");

            // Verifica senha
            if (!BCrypt.Net.BCrypt.Verify(login.SenhaHash, dbUser.SenhaHash))
                return Unauthorized("Credenciais inválidas.");

            // Gera o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}
