using Ecommerce.Domain;
using Ecommerce.Domain.Models.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {

        //[Controller] ➜ [Service/Handler] ➜ [Specification] ➜ [Repository] ➜ [EF Core Query Execution]
        #region Where 
        protected BaseSpecifications(Expression<Func<TEntity, bool>> _Criteria)
        {
            //only a private set for Criteria to be set only in the constructor
            Criteria = _Criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; } //store the where condition
        #endregion

        #region Order
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> _OrderBy)
        {
            OrderBy = _OrderBy;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> _OrderByDesc)
        {
            OrderByDesc = _OrderByDesc;
        }

        #endregion

        #region Include

        public List<Expression<Func<TEntity, Object>>> Includes { get; } = [];



        //method to add include expressions to the list
        //so that the derived classes can call it to add their include expressions
        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpresion)
        {
            Includes.Add(IncludeExpresion);
        }


        #endregion


        #region Pagination

        public int take { get; private set; }

        public int skip { get; private set; }

        public bool isPaginable { get; set; }


        public void ApplyPagination(int PagexIndex,int PageSize)
        {
            isPaginable = true; //m3naha en mhtag el pagination 
            take = PageSize;
            skip = (PagexIndex-1)*PageSize; //(3-1)*10 = skip  first 20
            //pageIndex = 3 
            //pageSize = 10 



        }

        #endregion

    }
}
