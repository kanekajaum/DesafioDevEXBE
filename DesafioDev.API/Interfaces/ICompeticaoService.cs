using DesafioDev.API.Models;
using static DesafioDev.API.Models.TopJogadoresResponse;

namespace DesafioDev.API.Interfaces
{
    public interface ICompeticaoService
    {
        Task<List<CompeticaoInfo>?> ObterCompeticoesComEquipesEPartidasAsync();
        Task<List<CompeticaoInfo>> ObterCompeticõesPorAreaAsync(int areaId);
        Task<List<Match>> ObterJogosDeHojeAsync();
        Task<List<Match>> ObterJogosChampionsLeague();
        Task<List<Match>> ObterJogosBrasileirao();
        Task<List<Scorer>> ObterTopJogadoresBrasileirao();
    }
}