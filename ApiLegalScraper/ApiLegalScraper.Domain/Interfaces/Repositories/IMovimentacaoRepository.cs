using ApiLegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Domain.Interfaces.Repositories
{
    public interface IMovimentacaoRepository : IRepositoryBase<Movimentacao>
    {
        Task<List<Movimentacao>> Create(List<Movimentacao> movimentacoes);
    }
}
