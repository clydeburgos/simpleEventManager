using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Application.Interfaces
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
    }
}
