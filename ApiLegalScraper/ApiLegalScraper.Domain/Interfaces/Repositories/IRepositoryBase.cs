using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiLegalScraper.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task<T> Create(T obj);
        Task Update(T obj);
        Task Delete(int id);
    }
}
