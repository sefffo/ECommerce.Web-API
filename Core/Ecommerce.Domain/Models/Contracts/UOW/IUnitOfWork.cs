using Ecommerce.Domain.Models.Contracts.Repository.GenericReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Contracts.UOW
{
    public interface IUnitOfWork
    {

        //m7tagen nrg repo el matlob msh zy el mvc barag3 kolo fe unit of work wa7da


        IGenericRepo<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

        Task<int> SaveChangesAsync();





    }
}
