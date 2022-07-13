using ApiLegalScraper.Domain.Interfaces.Services;
using ApiLegalScraper.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLegalScraper.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(IMovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Movimentacao>> GetMovimentacoes()
        {
            return await _movimentacaoService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movimentacao>> GetMovimentacoes(int id)
        {
            return await _movimentacaoService.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Movimentacao>> PostMovimentacoes([FromBody] Movimentacao movimentacao)
        {
            var nMovimentacao = await _movimentacaoService.Create(movimentacao);
            return CreatedAtAction(nameof(GetMovimentacoes), new { id = nMovimentacao.Id }, nMovimentacao);
        }

        [Route("lista")]
        [HttpPost]
        public async Task<ActionResult<List<Movimentacao>>> PostMovimentacoes([FromBody] List<Movimentacao> movimentacoes)
        {
            var nMovimentacoes = await _movimentacaoService.Create(movimentacoes);
            return CreatedAtAction(nameof(GetMovimentacoes), nMovimentacoes);
        }

        [HttpPut]
        public async Task<ActionResult> PutMovimentacoes([FromBody] Movimentacao processo)
        {
            await _movimentacaoService.Update(processo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovimentacoes(int id)
        {
            var processoToDelete = await _movimentacaoService.Get(id);
            if (processoToDelete == null)
                return NotFound();

            await _movimentacaoService.Delete(id);
            return NoContent();
        }
    }
}
