using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstraction.Services
{
    public interface IServiceManger
    {
        public IProductService ProductServices { get; } //el opbject hytl3 bs mn el service manger 
    }
}
