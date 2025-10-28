using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Contracts.Repository.GenericReposatory
{
    public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        void add(TEntity entity);
        void update(TEntity entity);
        void delete(TEntity entity);

    }
}
