using DesafioDev.API.Models;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DesafioDev.API.Services
{
    public class FutebolService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FutebolService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["FootballApi:BaseUrl"]!);
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _configuration["FootballApi:ApiKey"]);
        }

        public async Task<List<Time>?> ObterTimesAsync()
        {
            var response = await _httpClient.GetAsync("teams");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var timeResponse = JsonSerializer.Deserialize<TimeResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return timeResponse?.Teams;
        }

        public async Task<List<Time>?> ObterTimesPorArea(int area)
        {
            var response = await _httpClient.GetAsync("teams");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var timeResponse = JsonSerializer.Deserialize<TimeResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return timeResponse?.Teams;
        }

        public async Task<List<TimeInfo>> ObterTimesComListaJogosAsync()
        {
            var responseTimes = await _httpClient.GetAsync("teams");
            if (!responseTimes.IsSuccessStatusCode) return new List<TimeInfo>();

            var jsonTimes = await responseTimes.Content.ReadAsStringAsync();
            var timeResponse = JsonSerializer.Deserialize<TimeResponse>(jsonTimes, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var times = new Dictionary<int, TimeInfo>();

            foreach (var time in timeResponse?.Teams ?? new List<Time>())
            {
                times[time.Id.Value] = new TimeInfo { Id = time.Id, Name = time.Name, Tla = time.Tla, Crest = time.Crest, Website = time.Website, Partidas = new List<PartidaInfo>() };
            }

            foreach (var time in times.Values)
            {
                var responsePartidas = await _httpClient.GetAsync($"teams/{time.Id}/matches");
                if (!responsePartidas.IsSuccessStatusCode) continue;

                var jsonPartidas = await responsePartidas.Content.ReadAsStringAsync();
                var partidas = JsonSerializer.Deserialize<PartidaResponse>(jsonPartidas, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (partidas?.Matches != null)
                {
                    foreach (var partida in partidas.Matches)
                    {
                        var partidaInfo = new PartidaInfo
                        {
                            Id = partida.Id,
                            Data = partida.UtcDate,
                            Competicao = partida.Competition.Name,
                            TimeCasa = partida.HomeTeam.Name,
                            TimeVisitante = partida.AwayTeam.Name,
                        };

                        if (partidaInfo.Data.Year.ToString() == DateTime.Now.ToString("yyyy"))
                        {
                            time.Partidas?.Add(partidaInfo);
                        }
                    }
                }
            }

            return times.Values.ToList();
        }

        public async Task<TimeDetalhado?> ObterTimesIDComListaJogosAsync(int timeId)
        {
            var response = await _httpClient.GetAsync($"teams/{timeId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var timeResponse = JsonSerializer.Deserialize<TimeDetalhado>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return timeResponse;
        }

        public async Task<List<AreaInfo>> ObterAreasAsync()
        {
            var response = await _httpClient.GetAsync("areas");

            if (!response.IsSuccessStatusCode)
                return new List<AreaInfo>();

            var json = await response.Content.ReadAsStringAsync();
            var areaResponse = JsonSerializer.Deserialize<AreaResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return areaResponse?.Areas ?? new List<AreaInfo>();
        }

        public async Task<List<Time>?> ObterTimesPorCompeticaoAsync(int competicaoId)
        {
            var response = await _httpClient.GetAsync($"competitions/{competicaoId}/teams");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var teamsResponse = JsonSerializer.Deserialize<EquipeResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return teamsResponse?.Teams;
        }
    }
}
