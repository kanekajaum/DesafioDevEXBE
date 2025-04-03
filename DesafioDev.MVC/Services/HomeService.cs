using DesafioDev.MVC.Models;
using System;
using System.Net.Http.Headers;
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


        public async Task<List<JogoViewModel>> ObterJogosFinalizados(string token)
        {
            var url = $"{_apiBaseUrl}ObterJogosBrasileirao";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<JogoViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            var jogos = JsonSerializer.Deserialize<List<JogoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (token != null)
            {
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

            return jogos
                .Where(j => j.Status == "FINISHED")
                .OrderByDescending(j => DateTime.Parse(j.UtcDate))
                .Take(9)
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

        public async Task<List<TimeViewModel>> ObterTimes(string token)
        {
            var url = $"{_apiBaseUrl}TimesDecompeticoes/{2013}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return new List<TimeViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TimeViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<TimeViewModel> BuscarTimeEPartidas(int id, string token)
        {
            var urlTime = $"{_apiBaseUrl}Times/Id?Id={id}";
            using var requestTime = new HttpRequestMessage(HttpMethod.Post, urlTime);

            requestTime.Content = new StringContent(JsonSerializer.Serialize(new { id = id }), Encoding.UTF8, "application/json");
            requestTime.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseTime = await _httpClient.SendAsync(requestTime);

            if (!responseTime.IsSuccessStatusCode)
                return new TimeViewModel();

            var jsonTime = await responseTime.Content.ReadAsStringAsync();
            var time = JsonSerializer.Deserialize<TimeViewModel>(jsonTime, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var urlJogos = $"{_apiBaseUrl}TimesJogosRecentes/{id}";
            using var requestJogos = new HttpRequestMessage(HttpMethod.Get, urlJogos);
            requestJogos.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseJogos = await _httpClient.SendAsync(requestJogos);

            if (!responseJogos.IsSuccessStatusCode)
                return time ?? new TimeViewModel();

            var jsonJogos = await responseJogos.Content.ReadAsStringAsync();
            var jogosRecentes = JsonSerializer.Deserialize<JogosRecentesViewModel>(jsonJogos, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            time.JogosRecentes = jogosRecentes?.MatchesVM ?? new List<MatchViewModel>();

            return time ?? new TimeViewModel();
        }

        public async Task<LoginRequest> ObterUsuario(string email)
        {
            var url = $"{_apiBaseUrl}api/usuarios";
            var resposta = await _httpClient.GetAsync(url);

            if (!resposta.IsSuccessStatusCode)
                return new LoginRequest();

            var json = await resposta.Content.ReadAsStringAsync();
            var usuario = JsonSerializer.Deserialize<List<LoginRequest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return usuario.FirstOrDefault(x => x.Email == email) ?? new LoginRequest();
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