using Ecommerce.Domain;
using Ecommerce.Domain.Models.Contracts.Repository.GenericReposatory;
using Ecommerce.Domain.Models.Contracts.Specifications;
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



        //a3ml mkhzn ystlem mny el expresions 3shan a3ml dynamic queries ==> From service to Repo layer => then use it to create the dynamic Queries


        //all are static queries so we need to make it more dynamic queries to make it more reusable and Be more With the O in SOLID principles
        //so we gonna use specification pattern to make it more dynamic
        //=>most common way to implement specification pattern is to create a class that implements ISpecification<TEntity> interface
        //and then pass that class to the repository method that needs to use it.
        //but for now we will keep it simple and just implement the basic methods
        //and later we will refactor it to use specification pattern
        //another way to make it more dynamic is to use expression trees

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);

        public void add(TEntity entity)
            => context.Set<TEntity>().Add(entity);

        public void update(TEntity entity)
            => context.Set<TEntity>().Update(entity);

        public void delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);




        //Specification Pattern Methods Implementation
        //=================================================
        //we will implement the methods that use specifications here
        //build the query based on the specifications passed
        public async Task<IEnumerable<TEntity>> GetAllWithSpecificatonsAsync(ISpecification<TEntity, TKey> Specifications)
        {
           var baseQuery= context.Set<TEntity>().AsQueryable();
            //apply criteria
            #region Bad Practise Implmentation 
            //if (Specifications.Criteria != null)
            //{
            //    //lw fe 7aga fe el criteria 3ml where w ab3t leha el expression Specifications.Criteria
            //    baseQuery = baseQuery.Where(Specifications.Criteria);
            //}
            ////apply includes
            //foreach (var include in Specifications.Includes)
            //{
            //    baseQuery = baseQuery.Include(include);
            //}
            ////apply ordering
            //if (Specifications.OrderBy != null)
            //{
            //    baseQuery = baseQuery.OrderBy(Specifications.OrderBy);
            //}
            //else if (Specifications.OrderByDesc != null)
            //{
            //    baseQuery = baseQuery.OrderByDescending(Specifications.OrderByDesc);
            //}
            //return Task.FromResult(baseQuery.AsEnumerable());

            #endregion

            #region Best Practise Implmentation
            //Using SpecificationEvaluator to build the query based on the specifications passed


            var query = await SpecificationEvaluator.CreateQuery<TEntity, TKey>(baseQuery, Specifications).ToListAsync();


            return query;
            #endregion
        }

        public async Task<TEntity> GetByIdWithSpecificationsAync(ISpecification<TEntity, TKey> Specifications)
        {
            var baseQuery = context.Set<TEntity>();

            return await SpecificationEvaluator.CreateQuery<TEntity, TKey>(baseQuery, Specifications).FirstOrDefaultAsync();


           
        }

        public async Task<int> GetCountWithSpecificatonsAsync(ISpecification<TEntity, TKey> Specifications)
        {
            //var baseQuery = context.Set<TEntity>().AsQueryable();

            var Query = SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Specifications);

            return await Query.CountAsync();
         
        }
    }
}
