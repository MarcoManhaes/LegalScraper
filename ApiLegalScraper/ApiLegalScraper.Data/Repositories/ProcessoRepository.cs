using LegalScraper.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ApiLegalScraper.Domain.Models;
using ApiLegalScraper.Domain.Interfaces.Repositories;

namespace ApiLegalScraper.Data.Repositories
{
    public class ProcessoRepository : IProcessoRepository
    {
        private readonly ApplicationContext _context;

        public ProcessoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Processo> Create(Processo processo)
        {
            _context.Processos.Add(processo);
            await _context.SaveChangesAsync();

            return processo;
        }

        public async Task Delete(int id)
        {
            var processoToDelete = await _context.Processos.FindAsync(id);
            _context.Processos.Remove(processoToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Processo>> Get()
        {
            return await _context.Processos.ToListAsync();
        }

        public async Task<Processo> Get(int id)
        {
            return await _context.Processos.FindAsync(id);
        }

        public async Task Update(Processo processo)
        {
            _context.Entry(processo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
