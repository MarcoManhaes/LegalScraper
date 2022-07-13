using ApiLegalScraper.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLegalScraper.Domain.Interfaces.Services
{
    public interface IProcessoService
    {
        Task<IEnumerable<Processo>> Get();
        Task<Processo> Get(int id);
        Task<Processo> Create(Processo processo);
        Task Update(Processo processo);
        Task Delete(int id);
    }
}
