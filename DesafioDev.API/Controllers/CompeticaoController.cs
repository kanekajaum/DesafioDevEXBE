using DesafioDev.API.Interfaces;
using DesafioDev.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompeticaoController : ControllerBase
    {
        private readonly ICompeticaoService _competicaoService;

        public CompeticaoController(ICompeticaoService competicaoService)
        {
            _competicaoService = competicaoService;
        }

        /// <summary>
        /// Lista todas as competições disponíveis.
        /// </summary>
        /// <returns>Lista de Competições</returns>
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

        /// <summary>
        /// Lista todas as áreas de competições disponíveis.
        /// </summary>
        /// <returns>Lista de áreas</returns>
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

        /// <summary>
        /// Lista todas as jogos do dia.
        /// </summary>
        /// <returns>Lista de jogos do dia</returns>
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

        /// <summary>
        /// Lista todos os jogos da Champions League.
        /// </summary>
        /// <returns>Lista de jogos da Champions League</returns>
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

        /// <summary>
        /// Lista todos os jogos do brasileirão.
        /// </summary>
        /// <returns>Lista de jogos do brasileirão</returns>
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

        /// <summary>
        /// Top 10 artilheiros do brasil.
        /// </summary>
        /// <returns>Lista de jogadores com mais gols no brasileirão</returns>
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