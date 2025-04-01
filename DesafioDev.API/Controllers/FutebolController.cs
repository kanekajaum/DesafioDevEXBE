using DesafioDev.API.Services;
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
            var times = await _futebolService.ObterTimesComListaJogosAsync();
            if (times == null || !times.Any())
            {
                return NotFound("Nenhum time encontrado.");
            }
            return Ok(times);
        }

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