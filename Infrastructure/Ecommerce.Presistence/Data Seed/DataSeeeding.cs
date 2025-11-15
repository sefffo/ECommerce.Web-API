using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Presistence.Contexts;
//using Ecommerce.Presistence.Identity.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly StoreIdntityDbContext storeIdntityDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataSeeeding(StoreDbContext context, StoreIdntityDbContext storeIdntityDbContext , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            this.context = context;
            this.storeIdntityDbContext = storeIdntityDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task DataSeedAsync()
        {
            //msh manteky en kolo ykon async asln 
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                context.Database.Migrate();


            //bd2na b da 3shan el product mo3tamed 3lih fel foreign key
            if (!context.ProductBrands.Any())
            {
                var productBrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\brands.json");
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
                var ProductTypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\types.json");
                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                if (ProductTypes is not null && ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(ProductTypes);
                    //context.SaveChanges();
                }
            }

            if (!context.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    context.Products.AddRange(Products);
                    //context.SaveChanges();
                }
            }

            context.SaveChanges();
        }

        public async Task IdentityInitializAsync()
        {

            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }

                if (!userManager.Users.Any())
                {
                    var User1 = new ApplicationUser()
                    {
                        Email = "saif@gmail.com",
                        DisplayName = "Saif Lotfy",
                        PhoneNumber = "1234567890",
                        UserName = "Saif",
                    };
                    var User2 = new ApplicationUser()
                    {
                        Email = "omar@gmail.com",
                        DisplayName = "Omar Lotfy",
                        PhoneNumber = "1234567890",
                        UserName = "Omar",
                    };
                    await userManager.CreateAsync(User1, "P@ssw0rd");
                    await userManager.CreateAsync(User2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User1, "SuperAdmin");
                    await userManager.AddToRoleAsync(User2, "Admin");


                }


                await storeIdntityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }
    }
}
