using DesafioDev.API.Models;
using DesafioDev.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompeticaoController : ControllerBase
    {
        private readonly CompeticaoService _competicaoService;

        public CompeticaoController(CompeticaoService competicaoService)
        {
            _competicaoService = competicaoService;
        }

        [Authorize]
        [HttpGet]
        [Route("/ObterCompeticoes")]
        public async Task<IActionResult> ObterCompeticoesComEquipesEPartidas()
        {
            var competicoes = await _competicaoService.ObterCompeticoesComEquipesEPartidasAsync();

            if (competicoes == null)
            {
                return NotFound();
            }

            return Ok(competicoes);
        }

        [Authorize]
        [HttpPost]
        [Route("/PorArea/{areaId}")]
        public async Task<ActionResult<List<CompeticaoInfo>>> ObterCompeticõesPorArea(int areaId)
        {
            var competicoes = await _competicaoService.ObterCompeticõesPorAreaAsync(areaId);

            if (competicoes == null || !competicoes.Any())
            {
                return NotFound("Nenhuma competição encontrada para esta área.");
            }

            return Ok(competicoes);
        }

        [Authorize]
        [HttpGet]
        [Route("/ObterJogosDeHoje")]
        public async Task<IActionResult> ObterJogosDeHoje()
        {
            try
            {
                var jogos = await _competicaoService.ObterJogosDeHojeAsync();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar jogos: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/ObterJogosChampionsLeague")]
        public async Task<IActionResult> ObterJogosChampionsLeague()
        {
            try
            {
                var jogos = await _competicaoService.ObterJogosChampionsLeague();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar jogos: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/ObterJogosBrasileirao")]
        public async Task<IActionResult> ObterJogosBrasileirao()
        {
            try
            {
                var jogos = await _competicaoService.ObterJogosBrasileirao();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar jogos: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/ObterTopJogadoresBrasileirao")]
        public async Task<IActionResult> ObterTopJogadoresBrasileirao()
        {
            try
            {
                var jogadores = await _competicaoService.ObterTopJogadoresBrasileirao();
                return Ok(jogadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar jogos: {ex.Message}");
            }
        }
    }
}