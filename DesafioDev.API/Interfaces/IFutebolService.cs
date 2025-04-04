using DesafioDev.API.Models;

namespace DesafioDev.API.Interfaces
{
    public interface IFutebolService
    {
        Task<List<Time>?> ObterTimesAsync();
        Task<List<Time>?> ObterTimesPorArea(int area);
        Task<List<TimeInfo>> ObterTimesComListaJogosAsync();
        Task<TimeDetalhado?> ObterTimesIDComListaJogosAsync(int timeId);
        Task<List<AreaInfo>> ObterAreasAsync();
        Task<List<Time>?> ObterTimesPorCompeticaoAsync(int competicaoId);
        Task<EquipeResponse> TimesJogosRecentes(int timeId);
    }
}