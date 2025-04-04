using DesafioDev.API.Models;
using Microsoft.AspNetCore.Mvc;

public interface IUsuarioService
{
    Usuario? BuscarUsuarioPorEmail(string email);
    Task<List<Usuario>> ListarUsuariosAsync();
    Task SalvarNovoUsuario(Usuario usuario);
    bool ValidarEmail(Usuario usuario);
}