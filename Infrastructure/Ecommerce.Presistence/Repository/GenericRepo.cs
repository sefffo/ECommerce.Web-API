using Ecommerce.Domain;
using Ecommerce.Domain.Models.Contracts.Repository.GenericReposatory;
using Ecommerce.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Repository
{
    public class GenericRepo<TEntity, TKey>(StoreDbContext context) : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        => await context.Set<TEntity>().FindAsync(id);

        public void add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public void update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public void delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

    }
}
