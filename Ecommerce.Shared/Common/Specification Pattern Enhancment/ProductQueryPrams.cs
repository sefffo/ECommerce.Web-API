using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common.Specification_Pattern_Enhancment
{
    public class ProductQueryPrams
    {

        //kol ma a3oz azwed param lazm a3oz azwed property hena 3shan a3oz a2dr a2bl el params di mn el service w a3ml 7aga b3dha
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingWay? SortingWay { get; set; }

        public string ? SearchValue { get; set; }

    }
}
