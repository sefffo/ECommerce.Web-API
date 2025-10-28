using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Data_Seed
{
    public class DataSeeeding : IdataSeed
    {
        private readonly StoreDbContext context;

        public DataSeeeding(StoreDbContext context)
        {
            this.context = context;
        }
        public void DataSeed()
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();


            //bd2na b da 3shan el product mo3tamed 3lih fel foreign key
            if (!context.ProductBrands.Any())
            {
                var productBrandsData = File.ReadAllText(@"..\Infrastructure\Ecommerce.Presistence\Data\brands.json");
                var productbrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                if (productbrands is not null && productbrands.Any())
                {
                    context.ProductBrands.AddRange(productbrands);
                    //context.SaveChanges();
                }
            }

            //bd2na b da 3shan el product mo3tamed 3lih fel foreign key
            if (!context.ProductTypes.Any())
            {
                var ProductTypesData = File.ReadAllText(@"..\Infrastructure\Ecommerce.Presistence\Data\types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                if (ProductTypes is not null && ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(ProductTypes);
                    //context.SaveChanges();
                }
            }

            if (!context.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"..\Infrastructure\Ecommerce.Presistence\Data\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    context.Products.AddRange(Products);
                    //context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}
