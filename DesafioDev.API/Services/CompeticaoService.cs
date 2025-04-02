using DesafioDev.API.Models;
using System.Text.Json;

namespace DesafioDev.API.Services
{

    public class CompeticaoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CompeticaoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["FootballApi:BaseUrl"]!);
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _configuration["FootballApi:ApiKey"]);
        }

        public async Task<List<CompeticaoInfo>?> ObterCompeticoesComEquipesEPartidasAsync()
        {
            var response = await _httpClient.GetAsync("competitions");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var competicaoResponse = JsonSerializer.Deserialize<CompeticaoResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (competicaoResponse == null || competicaoResponse.Competitions == null)
            {
                return null;
            }

            foreach (var competicao in competicaoResponse.Competitions)
            {
                var equipesResponse = await _httpClient.GetAsync($"competitions/{competicao.Id}/teams");

                if (equipesResponse.IsSuccessStatusCode)
                {
                    var equipesJson = await equipesResponse.Content.ReadAsStringAsync();
                    var equipesData = JsonSerializer.Deserialize<EquipeResponse>(equipesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    competicao.Teams = equipesData?.TeamsResponse;
                }

                var partidasResponse = await _httpClient.GetAsync($"competitions/{competicao.Id}/matches");

                if (partidasResponse.IsSuccessStatusCode)
                {
                    var partidasJson = await partidasResponse.Content.ReadAsStringAsync();
                    var partidasData = JsonSerializer.Deserialize<PartidaResponse>(partidasJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    competicao.Matches = partidasData?.Matches;
                }
            }

            return competicaoResponse.Competitions;
        }

        public async Task<List<CompeticaoInfo>> ObterCompeticõesPorAreaAsync(int areaId)
        {
            var url = $"competitions?areas={areaId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<CompeticaoInfo>();

            var json = await response.Content.ReadAsStringAsync();
            var competicoesResponse = JsonSerializer.Deserialize<CompeticaoResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return competicoesResponse?.Competitions ?? new List<CompeticaoInfo>();
        }

        public async Task<List<Match>> ObterJogosDeHojeAsync()
        {
            var hoje = DateTime.Now;
            var url = $"matches";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao buscar jogos: {response.ReasonPhrase}");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<MatchResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var resolutadoFiltrado = data?.Matches.Find(x => x.UtcDate.Date == hoje.Date);
            return data?.Matches ?? new List<Match>();
        }
    }
}