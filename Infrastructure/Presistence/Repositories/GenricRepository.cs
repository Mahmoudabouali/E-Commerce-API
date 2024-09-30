using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenricRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> 
        where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreContext _storeContext;
        public GenricRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) =>  _storeContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
            //if(trackChanges)
            //    return await _storeContext.Set<TEntity>().ToListAsync();
            //return await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

            => trackChanges
                ? await _storeContext.Set<TEntity>().ToListAsync()
                : (IEnumerable<TEntity>)await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(Tkey id) => await _storeContext.Set<TEntity>().FindAsync(id);
    }
}
