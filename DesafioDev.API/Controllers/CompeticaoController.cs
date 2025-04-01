using DesafioDev.API.Services;
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

        [HttpGet]
        public async Task<IActionResult> ObterCompeticoesComEquipesEPartidas()
        {
            var competicoes = await _competicaoService.ObterCompeticoesComEquipesEPartidasAsync();

            if (competicoes == null)
            {
                return NotFound();
            }

            return Ok(competicoes);
        }
    }
}
