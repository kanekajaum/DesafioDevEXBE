using DesafioDev.API.Contexto;
using DesafioDev.API.Interfaces;
using DesafioDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public AuthController(ITokenService tokenService, ApplicationDbContext context)
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