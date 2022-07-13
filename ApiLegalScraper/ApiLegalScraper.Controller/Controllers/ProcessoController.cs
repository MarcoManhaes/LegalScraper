using ApiLegalScraper.Domain.Interfaces.Services;
using ApiLegalScraper.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLegalScraper.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        private IProcessoService _processoService;

        public ProcessoController(IProcessoService processoService)
        {
            _processoService = processoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Processo>> GetProcessos()
        {
            return await _processoService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Processo>> GetProcessos(int id)
        {
            return await _processoService.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Processo>> PostProcessos([FromBody] Processo processo)
        {
            var newProcesso = await _processoService.Create(processo);
            return CreatedAtAction(nameof(GetProcessos), new { id = newProcesso.Id }, newProcesso);
        }

        [HttpPut]
        public async Task<ActionResult> PutProcessos([FromBody] Processo processo)
        {
            await _processoService.Update(processo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProcessos(int id)
        {
            var processoToDelete = await _processoService.Get(id);
            if(processoToDelete == null)
                return NotFound();

            await _processoService.Delete(id);
            return NoContent();
        }
    }
}
