using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public class BaseEntity<TKey>
    {
        public TKey  Id { get; set; }
    }
}
