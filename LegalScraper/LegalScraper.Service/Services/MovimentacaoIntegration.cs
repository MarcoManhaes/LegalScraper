using LegalScraper.Domain.Interfaces;
using LegalScraper.Domain.Models;
using LegalScraper.Service.Integrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Service.Services
{
    public class MovimentacaoIntegration : IMovimentacaoIntegration
    {
        private readonly ApiLegalScraperIntegrationBase _apiLegalScraperIntegration;

        public MovimentacaoIntegration(ApiLegalScraperIntegrationBase apiLegalScraperIntegration)
        {
            _apiLegalScraperIntegration = apiLegalScraperIntegration;
        }

        public async Task<List<Movimentacao>> CreateMovimentacao(List<Movimentacao> movimentacoes)
        {
            var result = await _apiLegalScraperIntegration.CreateEntity<List<Movimentacao>>("/api/Movimentacao/lista", movimentacoes);
            return result;
        }

        public Task<Movimentacao> CreateMovimentacao(Movimentacao movimentacao)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMovimentacao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movimentacao> GetMovimentacao(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movimentacao>> GetMovimentacao()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovimentacao(Movimentacao movimentacao)
        {
            throw new NotImplementedException();
        }
    }
}
