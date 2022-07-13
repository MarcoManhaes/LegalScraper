using ApiLegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Domain.Interfaces.Services
{
    public interface IMovimentacaoService
    {
        Task<IEnumerable<Movimentacao>> Get();
        Task<Movimentacao> Get(int id);
        Task<Movimentacao> Create(Movimentacao movimentacao);
        Task<List<Movimentacao>> Create(List<Movimentacao> movimentacoes);
        Task Update(Movimentacao processo);
        Task Delete(int id);
    }
}
