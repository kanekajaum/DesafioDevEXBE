using DesafioDev.API.Models;

public interface IUsuarioService
{
    Usuario? BuscarUsuarioPorEmail(string email);
    Task<List<Usuario>> ListarUsuariosAsync();
    Task SalvarNovoUsuario(Usuario usuario);
    bool ValidarEmail(Usuario usuario);
}