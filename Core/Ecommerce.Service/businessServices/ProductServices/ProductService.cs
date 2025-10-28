using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.DTOs.ProductDro_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices.ProductServices
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllAsync();
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return ProductsDto;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id);
            var ProductDto = mapper.Map<Product, ProductDto>(product);
            return ProductDto;
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var repo = unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandDto = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandDto;
        }
        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();
            var Types = await repo.GetAllAsync();
            var TypesDto = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }
    }
}
