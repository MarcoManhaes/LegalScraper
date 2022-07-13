using ApiLegalScraper.Domain.Interfaces.Repositories;
using ApiLegalScraper.Domain.Interfaces.Services;
using ApiLegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Service.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        public ProcessoService(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        public async Task<IEnumerable<Processo>> Get()
        {
            return await _processoRepository.Get();
        }

        public async Task<Processo> Get(int id)
        {
            return await _processoRepository.Get(id);
        }

        public async Task<Processo> Create(Processo processo)
        {
            return await _processoRepository.Create(processo);
        }

        public async Task Update(Processo processo)
        {
            await _processoRepository.Update(processo);
        }

        public async Task Delete(int id)
        {
            await _processoRepository.Delete(id);
        }
    }
}
