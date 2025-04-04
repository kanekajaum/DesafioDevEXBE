using DesafioDev.API.Contexto;
using DesafioDev.API.Interfaces;
using DesafioDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DesafioDev.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public UsuarioController(ApplicationDbContext context, IUsuarioService usuarioService, ITokenService tokenService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Cadastrar usuarios
        /// </summary>
        /// <returns>Cadastro de usuarios</returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (_usuarioService.ValidarEmail(usuario))
            {
                return BadRequest("E-mail já cadastrado.");
            }

            await _usuarioService.SalvarNovoUsuario(usuario);

            return CreatedAtAction(nameof(CadastrarUsuario), new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Lista de usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
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

        /// <summary>
        /// Valida token
        /// </summary>
        /// <returns>Valida token de usuarios</returns>
        [HttpPost("validarLogin")]
        public async Task<IActionResult> ValidarLogin(string email, string senha)
        {
            var usuarioLogin = _usuarioService.BuscarUsuarioPorEmail(email);

            if (usuarioLogin == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            string senhaTela = HashSenhaSHA256(senha);

            if (senhaTela != usuarioLogin.SenhaHash)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var token = _tokenService.GenerateToken(usuarioLogin.SenhaHash);
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
}
