using DesafioDev.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    [Route("api/[controller]")]
    public class FutebolController : ControllerBase
    {
        private readonly IFutebolService _futebolService;

        public FutebolController(IFutebolService futebolService)
        {
            _futebolService = futebolService;
        }

        /// <summary>
        /// Lista os times em geral
        /// </summary>
        /// <returns>Lista de Times</returns>
        [HttpGet("/Times")]
        public async Task<IActionResult> ObterTimes()
        {
            var times = await _futebolService.ObterTimesAsync();
            if (times == null || !times.Any())
            {
                return NotFound("Nenhum time encontrado.");
            }
            return Ok(times);
        }

        /// <summary>
        /// busca um time
        /// </summary>
        /// <returns>Buscar Time</returns>

        [Authorize]
        [HttpPost("/Times/Id")]
        public async Task<IActionResult> ObterTimesPorId(int id)
        {
            var times = await _futebolService.ObterTimesIDComListaJogosAsync(id);
            if (times == null)
            {
                return NotFound("Nenhum time encontrado.");
            }
            return Ok(times);
        }

        /// <summary>
        /// Lista todos os times de uma competição especifica
        /// </summary>
        /// <returns>Buscar os times de uma competição</returns>
        [Authorize]
        [HttpGet("/TimesDecompeticoes/{competicaoId}/")]
        public async Task<IActionResult> ObterTimesPorCompeticao(int competicaoId)
        {
            var times = await _futebolService.ObterTimesPorCompeticaoAsync(competicaoId);

            if (times == null || !times.Any())
            {
                return NotFound($"Nenhum time encontrado para a competição com ID {competicaoId}.");
            }

            return Ok(times);
        }

        /// <summary>
        /// Lista os jogos de um time
        /// </summary>
        /// <returns>Buscar os jogos recentes de um Time</returns>
        [Authorize]
        [HttpGet("/TimesJogosRecentes/{timeId}/")]
        public async Task<IActionResult> TimesJogosRecentes(int timeId)
        {
            var times = await _futebolService.TimesJogosRecentes(timeId);

            if (times == null )
            {
                return NotFound($"Nenhum time encontrado.");
            }

            return Ok(times);
        }

        /// <summary>
        /// Lista as areas por paises
        /// </summary>
        /// <returns>Buscar as areas de competições por paises</returns>
        [Authorize]
        [HttpGet("/Areas")]
        public async Task<IActionResult> ObterAreas()
        {
            var areas = await _futebolService.ObterAreasAsync();
            if (areas == null || !areas.Any())
            {
                return NotFound("Nenhum time encontrado.");
            }
            return Ok(areas);
        }
    }
}