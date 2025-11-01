using Ecommerce.Domain;
using Ecommerce.Domain.Models.Contracts.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence
{
    public static class SpecificationEvaluator
    {

        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> BaseQuery, ISpecification<TEntity, TKey> specification ) where TEntity : BaseEntity<TKey>
        {
            var query = BaseQuery;
            // Apply criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            //3shan el order lazm ykon ba3d el where 3shan lw kan fe filter w order by fel nafs el wa2t lazm ykono b tartib 
            //w abl el includes 3shan el includes msh hay2dr y3ml 7aga 3la el order aw el where

            // Apply ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDesc != null)
            {
                query = query.OrderByDescending(specification.OrderByDesc);
            }

            // Apply includes
            if (specification.Includes !=null  && specification.Includes.Any())
            {
                foreach (var include in specification.Includes)
                {
                    query = query.Include(include);
                }

                //both has the same peroformance using foreach or aggregate method but the for each is more readable    


                //query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

          


            return query;
        }

    }
}
