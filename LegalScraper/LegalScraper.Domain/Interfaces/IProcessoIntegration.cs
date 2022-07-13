using LegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Domain.Interfaces
{
    public interface IProcessoIntegration
    {
        Task<Processo> GetProcesso(int id);
        Task<List<Processo>> GetProcesso();
        Task<bool> UpdateProcesso(Processo processo);
        Task<Processo> CreateProcesso(Processo processo);
        Task<bool> DeleteProcesso(int id);
    }
}
