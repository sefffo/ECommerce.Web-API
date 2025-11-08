using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Service.businessServices.CartServices;
using Ecommerce.Service.businessServices.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices
{
    public class ServiceManger(IUnitOfWork unitOfWork , IMapper mapper , ICartRepo Repo) : IServiceManger
    {

        //Controller → ServiceManager → Business Services → UnitOfWork → Repositories → Database

        //zy ma ana kda atlop service product el manger ybthaly 
        private readonly Lazy<IProductService> LazyProjectService
                 = new Lazy<IProductService>(() => new Ecommerce.Service.businessServices.ProductServices.ProductService(unitOfWork, mapper));
        public IProductService ProductServices => LazyProjectService.Value;

        private readonly  Lazy<ICartService> service
            = new Lazy<ICartService>(() => new CartService(Repo, mapper));


     

        public ICartService CartService => service.Value;






        //// same idea as UnitOfWork: key = service name, value = instance
        //private readonly Dictionary<string, object> _services = new();



        //public TService GetService<TService>() where TService : class
        //{
        //    var typeName = typeof(TService).Name;

        //    if (_services.ContainsKey(typeName))
        //    {
        //        // already created
        //        return (TService)_services[typeName];
        //    }
        //    else
        //    {
        //        // create and cache
        //        object service = CreateService<TService>();
        //        _services.Add(typeName, service);
        //        return (TService)service;
        //    }
        //}

        //private object CreateService<TService>() where TService : class
        //{
        //    // map each interface to its concrete implementation
        //    return typeof(TService).Name switch
        //    {
        //        nameof(IProductService) => new ProductService(_unitOfWork, _mapper)

        //    };
        //}

        //// Shortcut properties (like UnitOfWork.GetRepository)
        ////public IProductService ProductService => GetService<IProductService>();

        //public IProductService ProductServices => GetService<IProductService>();
    }
}
