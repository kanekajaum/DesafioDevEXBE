using DesafioDev.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    [Route("api/[controller]")]
    public class FutebolController : ControllerBase
    {
        private readonly FutebolService _futebolService;

        public FutebolController(FutebolService futebolService)
        {
            _futebolService = futebolService;
        }

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

        [HttpGet("/TimesJogosRecentes/{timeId}/")]
        public async Task<IActionResult> TimesJogosRecentes(int timeId)
        {
            var times = await
            _futebolService.TimesJogosRecentes(timeId);

            if (times == null )
            {
                return NotFound($"Nenhum time encontrado.");
            }

            return Ok(times);
        }

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