using DesafioDev.API.Models;

public interface IUsuarioService
{
    Task<List<Usuario>> ListarUsuariosAsync();
    Task SalvarNovoUsuario(Usuario usuario);
}