using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Contracts.Specifications
{
    public interface ISpecification <TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        //store the different parts of the query


        Expression<Func<TEntity,bool>> Criteria { get; /* no set here for making it abstract  */ } //where condition


        //stores the include expressions 
        //list of expressions 3shan a3ml include l kol el related entities eli 3ayzha
        //3shan bt3tmd 3 el relationships ben el entities
        List<Expression<Func<TEntity,object>>> Includes { get; } //includes bta3et el barnd w el type 




        Expression<Func<TEntity, object>> OrderBy { get; } // order by Ascending 

        Expression<Func<TEntity, object>> OrderByDesc { get; } // order by Descending



    }
}
