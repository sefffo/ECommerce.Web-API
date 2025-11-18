using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Orders
{
    public class ItemTobeOrderd
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductImgUrl { get; set; } = null!;


    }
}
