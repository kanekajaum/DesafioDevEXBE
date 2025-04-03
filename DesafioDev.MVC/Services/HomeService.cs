using DesafioDev.MVC.Models;
using System.Text;
using System.Text.Json;

namespace DesafioDev.MVC.Services
{
    public class HomeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public HomeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }


        public async Task<List<JogoViewModel>> ObterJogosFinalizados()
        {
            var url = $"{_apiBaseUrl}ObterJogosBrasileirao";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<JogoViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            var jogos = JsonSerializer.Deserialize<List<JogoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return jogos
                .Where(j => j.Status == "FINISHED")
                .OrderByDescending(j => DateTime.Parse(j.UtcDate))
                .Take(6)
                .Select(j => new JogoViewModel
                {
                    Id = j.Id,
                    UtcDate = j.UtcDate,
                    Status = TraduzirStatus(j.Status),
                    HomeTeam = j.HomeTeam,
                    AwayTeam = j.AwayTeam,
                    Score = j.Score
                })
                .ToList();
        }

        public async Task<List<JogoViewModel>> ObterJogosDeHoje()
        {
            var url = $"{_apiBaseUrl}ObterJogosDeHoje";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<JogoViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            var jogos = JsonSerializer.Deserialize<List<JogoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return jogos
                .OrderBy(j => DateTime.Parse(j.UtcDate))
                .Take(6)
                .Select(j => new JogoViewModel
                {
                    Id = j.Id,
                    UtcDate = j.UtcDate,
                    Status = TraduzirStatus(j.Status),
                    HomeTeam = j.HomeTeam,
                    AwayTeam = j.AwayTeam,
                    Score = j.Score
                })
                .ToList();
        }

        public async Task<List<JogoViewModel>> ObterJogosChampionsLeague()
        {
            var url = $"{_apiBaseUrl}ObterJogosChampionsLeague";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<JogoViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            var jogos = JsonSerializer.Deserialize<List<JogoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return jogos
                .OrderBy(j => DateTime.Parse(j.UtcDate))
                .Take(6)
                .Select(j => new JogoViewModel
                {
                    Id = j.Id,
                    UtcDate = j.UtcDate,
                    Status = TraduzirStatus(j.Status),
                    HomeTeam = j.HomeTeam,
                    AwayTeam = j.AwayTeam,
                    Score = j.Score
                })
                .ToList();
        }

        public async Task<List<TimeViewModel>> ObterTimes()
        {
            var url = $"{_apiBaseUrl}TimesDecompeticoes/{2013}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<TimeViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TimeViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<TimeViewModel> BuscarTimeEPartidas(int id)
        {

            var urlTime = $"{_apiBaseUrl}Times/Id?Id={id}";
            var content = new StringContent(JsonSerializer.Serialize(new { id = id }), Encoding.UTF8, "application/json");
            var responseTime = await _httpClient.PostAsync(urlTime, content);



            var jsonTime = await responseTime.Content.ReadAsStringAsync();
            var time = JsonSerializer.Deserialize<TimeViewModel>(jsonTime, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var urlJogos = $"{_apiBaseUrl}TimesJogosRecentes/{id}";
            var responseJogos = await _httpClient.GetAsync(urlJogos);

            var jsonJogos = await responseJogos.Content.ReadAsStringAsync();
            var jogosRecentes = JsonSerializer.Deserialize<JogosRecentesViewModel>(jsonJogos, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            time.JogosRecentes = jogosRecentes?.MatchesVM ?? new List<MatchViewModel>();

            return time ?? new TimeViewModel();
        }

        private string TraduzirStatus(string status)
        {
            return status switch
            {
                "TIMED" => "Agendado",
                "SCHEDULED" => "Programado",
                "FINISHED" => "Finalizado",
                _ => "Desconhecido"
            };
        }
    }
}