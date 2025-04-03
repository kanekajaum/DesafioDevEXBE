using DesafioDev.MVC.Models;
using DesafioDev.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
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
            var jogosFinalizados = await _homeService.ObterJogosFinalizados();
            var jogosDeHoje = await _homeService.ObterJogosDeHoje();
            var jogosChampions = await _homeService.ObterJogosChampionsLeague();
            await CarregarselectTimes();

            var viewModel = new HomeViewModel
            {
                JogosFinalizados = jogosFinalizados,
                JogosDeHoje = jogosDeHoje,
                JogosChampions = jogosChampions
            };

            return View(viewModel);
        }

        private async Task CarregarselectTimes()
        {
            var times = await _homeService.ObterTimes();
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
            await CarregarselectTimes();
            var time = await _homeService.BuscarTimeEPartidas(id);

            return View(time);
        }

        public IActionResult Registrar()
        {
            return View();
        }
    }
}