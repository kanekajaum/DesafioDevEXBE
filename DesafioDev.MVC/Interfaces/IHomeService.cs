using System.Threading.Tasks;
using DesafioDev.MVC.Models;

namespace DesafioDev.MVC.Interfaces
{
    public interface IHomeService
    {
        Task<List<JogoViewModel>> ObterJogosFinalizados(string token);

        Task<List<JogoViewModel>> ObterJogosDeHoje();

        Task<List<JogoViewModel>> ObterJogosChampionsLeague();

        Task<List<TimeViewModel>> ObterTimes(string token);

        Task<TimeViewModel> BuscarTimeEPartidas(int id, string token);

        Task<LoginRequest> ObterUsuario(string email);
    }
}