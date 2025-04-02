using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simulação de autenticação (substitua por validação real)
        if (request.Username == "admin" && request.Password == "123456")
        {
            var token = _tokenService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized("Usuário ou senha inválidos");
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}