using DesafioDev.API.Contexto;
using DesafioDev.API.Models;
using DesafioDev.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(ApplicationDbContext context, UsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                return BadRequest("E-mail já cadastrado.");
            }

            usuario.SenhaHash = PasswordService.HashSenha(usuario.SenhaHash);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CadastrarUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListarUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(usuarios);
        }
    }
}
