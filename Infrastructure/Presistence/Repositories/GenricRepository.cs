using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

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

        public void Delete(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
            //if(trackChanges)
            //    return await _storeContext.Set<TEntity>().ToListAsync();
            //return await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

            => trackChanges
                ? await _storeContext.Set<TEntity>().ToListAsync()
                : (IEnumerable<TEntity>)await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(Tkey id) => await _storeContext.Set<TEntity>().FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
                    => await ApplySpecifications(specifications).ToListAsync();
        public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
            => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpecifications(Specifications<TEntity> specifications)
            => SpecificationEvaluator.GetQuery<TEntity>(_storeContext.Set<TEntity>(), specifications);

    }
}
