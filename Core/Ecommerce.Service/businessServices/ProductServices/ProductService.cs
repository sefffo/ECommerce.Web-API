using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Service.Specifications;
using Ecommerce.Shared.Common;
using Ecommerce.Shared.Common.Specification_Pattern_Enhancment;
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
        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryPrams productQueryPrams)
        {
            //we must use specifications to get products with their types and brands
            //aslo we must add the the spec in the repository method
            var spec = new  ProductSpecifications(productQueryPrams); //create specification instance
            var repo = unitOfWork.GetRepository<Product, int>();
            var products = await repo.GetAllWithSpecificatonsAsync(spec);
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return ProductsDto;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            var repo = unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdWithSpecificationsAync(spec);
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
