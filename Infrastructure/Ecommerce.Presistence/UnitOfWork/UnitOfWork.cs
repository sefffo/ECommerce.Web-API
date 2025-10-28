using Ecommerce.Domain;
using Ecommerce.Domain.Models.Contracts.Repository.GenericReposatory;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Presistence.Contexts;
using Ecommerce.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.UnitOfWork
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        //esm el Key (product) , value hwa el repo (GenericRepo<Product,int>)
        private Dictionary<string, object> repositories = [];
        public IGenericRepo<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;//esm el repo

            if (repositories.ContainsKey(TypeName))//3shan lw el repo da atalb w mwgood abl kda
            {
                return (IGenericRepo<TEntity, TKey>)repositories[TypeName];//u must cast l Repo 3shan ynf3 tb3to 
            }
            else
            {
                var repo = new GenericRepo<TEntity, TKey>(context); //lw mafish e3mlo craete w 5zn 3shan lw geh tany fe nfs el rquest yst5dmha
                repositories.Add(TypeName, repo);
                return repo;//m7tag trg3o b2a 
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
