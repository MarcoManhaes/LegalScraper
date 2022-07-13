using ApiLegalScraper.Domain.Interfaces.Repositories;
using ApiLegalScraper.Domain.Models;
using LegalScraper.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly ApplicationContext _context;

        public MovimentacaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Movimentacao>> Create(List<Movimentacao> movimentacoes)
        {
            _context.Movimentacoes.AddRange(movimentacoes);
            await _context.SaveChangesAsync();

            return movimentacoes;
        }

        public async Task<Movimentacao> Create(Movimentacao movimentacao)
        {
            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return movimentacao;
        }

        public async Task Delete(int id)
        {
            var processoToDelete = await _context.Movimentacoes.FindAsync(id);
            _context.Movimentacoes.Remove(processoToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movimentacao>> Get()
        {
            return await _context.Movimentacoes.ToListAsync();
        }

        public async Task<Movimentacao> Get(int id)
        {
            return await _context.Movimentacoes.FindAsync(id);
        }

        public async Task Update(Movimentacao movimentacao)
        {
            _context.Entry(movimentacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
