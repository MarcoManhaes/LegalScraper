using ApiLegalScraper.Domain.Interfaces.Repositories;
using ApiLegalScraper.Domain.Interfaces.Services;
using ApiLegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Service.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<Movimentacao> Create(Movimentacao movimentacao)
        {
            return await _movimentacaoRepository.Create(movimentacao);
        }

        public async Task<List<Movimentacao>> Create(List<Movimentacao> movimentacoes)
        {
            return await _movimentacaoRepository.Create(movimentacoes);
        }

        public async Task Delete(int id)
        {
            await _movimentacaoRepository.Delete(id);
        }

        public async Task<IEnumerable<Movimentacao>> Get()
        {
            return await _movimentacaoRepository.Get();
        }

        public async Task<Movimentacao> Get(int id)
        {
            return await _movimentacaoRepository.Get(id);
        }

        public async Task Update(Movimentacao processo)
        {
            await _movimentacaoRepository.Update(processo);
        }
    }
}
