using DesafioDev.API.Contexto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public AuthController(TokenService tokenService, ApplicationDbContext context)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var usuario = _context.Usuarios.SingleOrDefault(u => u.Email == request.Username);

        if (usuario == null)
        {
            return Unauthorized("Usuário não encontrado.");
        }

        string senhaTela = HashSenhaSHA256(request.Password);

        if (senhaTela != usuario.SenhaHash)
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        // Gerar o token JWT
        var token = _tokenService.GenerateToken(usuario.Email);

        return Ok(new { Token = token });
    }

    public static string HashSenhaSHA256(string senha)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}