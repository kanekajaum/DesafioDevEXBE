namespace DesafioDev.MVC.Interfaces
{
    using System.Threading.Tasks;
    using DesafioDev.MVC.Models;

    public interface IAuthService
    {
        Task<string> ValidateUserAsync(LoginRequest request);
        Task<string> RegistrarUserAsync(LoginRequest request);
    }
}