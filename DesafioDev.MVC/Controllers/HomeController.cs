using DesafioDev.MVC.Models;
using DesafioDev.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DesafioDev.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public HomeController(HomeService homeService, HttpClient httpClient, IConfiguration configuration)
        {
            _homeService = homeService;
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var email = HttpContext.Session.GetString("EmailToken");

            var jogosFinalizados = await _homeService.ObterJogosFinalizados(token);
            var jogosDeHoje = await _homeService.ObterJogosDeHoje();
            var jogosChampions = await _homeService.ObterJogosChampionsLeague();
            var topJogadores = await ObterTopJogadoresBrasileirao(token);
            await CarregarSelectTimes();

            var viewModel = new HomeViewModel
            {
                JogosFinalizados = jogosFinalizados,
                JogosDeHoje = jogosDeHoje,
                JogosChampions = jogosChampions,
                TopJogadores = topJogadores
            };

            if (!string.IsNullOrEmpty(token))
            {
                var usuarioBanco =  await _homeService.ObterUsuario(email);
                var usuario = new
                {
                    Nome = usuarioBanco.Nome,
                    Email = usuarioBanco.Email
                };

                ViewBag.Usuario = usuario;
            }

            return View(viewModel);
        }

        private async Task<List<TopJogadorViewModel>> ObterTopJogadoresBrasileirao(string token)
        {
            var url = $"{_apiBaseUrl}ObterTopJogadoresBrasileirao";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return new List<TopJogadorViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TopJogadorViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task CarregarSelectTimes()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var times = await _homeService.ObterTimes(token);
            ViewBag.Times = times;
        }

        [HttpPost]
        public async Task<IActionResult> ObterDetalhesTime(int id)
        {
            var url = $"{_apiBaseUrl}Times/{id}";
            var response = await _httpClient.PostAsync(url, null);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Erro ao buscar detalhes do time.");

            var json = await response.Content.ReadAsStringAsync();
            var time = JsonSerializer.Deserialize<TimeDetalhesViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Json(time);
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Time(int id)
        {
            await CarregarSelectTimes();
            var token = HttpContext.Session.GetString("AuthToken");
            var time = await _homeService.BuscarTimeEPartidas(id, token);

            return View(time);
        }

        public IActionResult Registrar()
        {
            return View();
        }
    }
}