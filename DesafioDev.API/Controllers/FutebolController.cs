using DesafioDev.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.API.Controllers
{
    public class FutebolController : ControllerBase
    {
        private readonly FutebolService _futebolService;

        public FutebolController(FutebolService futebolService)
        {
            _futebolService = futebolService;
        }

        [HttpGet("api/Times")]
        public async Task<IActionResult> ObterTimes()
        {
            var times = await _futebolService.ObterTimesComListaJogosAsync();
            if (times == null || !times.Any())
            {
                return NotFound("Nenhum time encontrado.");
            }
            return Ok(times);
        }

        [HttpGet("api/Areas")]
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