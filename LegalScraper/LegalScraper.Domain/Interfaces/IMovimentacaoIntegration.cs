using LegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Domain.Interfaces
{
    public interface IMovimentacaoIntegration
    {
        Task<Movimentacao> GetMovimentacao(int id);
        Task<List<Movimentacao>> GetMovimentacao();
        Task<bool> UpdateMovimentacao(Movimentacao movimentacao);
        Task<Movimentacao> CreateMovimentacao(Movimentacao movimentacao);
        Task<List<Movimentacao>> CreateMovimentacao(List<Movimentacao> movimentacao);
        Task<bool> DeleteMovimentacao(int id);
    }
}
