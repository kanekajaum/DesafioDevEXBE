using DesafioDev.API.Models;
using Microsoft.Extensions.FileSystemGlobbing;
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

            var responsePartidas = await _httpClient.GetAsync("matches");
            if (!responsePartidas.IsSuccessStatusCode) return new List<TimeInfo>();

            var jsonPartidas = await responsePartidas.Content.ReadAsStringAsync();
            var partidas = JsonSerializer.Deserialize<PartidaResponse>(jsonPartidas, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var times = new Dictionary<int, TimeInfo>();

            foreach (var time in timeResponse?.Teams ?? new List<Time>())
            {
                times[time.Id] = new TimeInfo { Id = time.Id, Name = time.Name, Partidas = new List<PartidaInfo>() };
            }

            if (partidas?.Matches != null)
            {
                foreach (var partida in partidas.Matches)
                {
                    var partidaInfo = new PartidaInfo
                    {
                        Id = partida.Id,
                        Data = partida.Data,
                        Competicao = partida.Competicao,
                        TimeCasa = partida.TimeCasa,
                        TimeVisitante = partida.TimeVisitante
                    };
                }
            }

            return times.Values.ToList();
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
    }
}
