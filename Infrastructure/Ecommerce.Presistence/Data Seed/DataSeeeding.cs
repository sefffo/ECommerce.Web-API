#region old code befoore deployment 
//using Ecommerce.Domain.Models.Contracts.Seed;
//using Ecommerce.Domain.Models.Identity;
//using Ecommerce.Domain.Models.Orders;
//using Ecommerce.Domain.Models.Products;
//using Ecommerce.Presistence.Contexts;
////using Ecommerce.Presistence.Identity.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Ecommerce.Presistence.Data_Seed
//{
//    public class DataSeeeding : IdataSeed
//    {
//        private readonly StoreDbContext context;
//        private readonly StoreIdntityDbContext storeIdntityDbContext;
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly RoleManager<IdentityRole> roleManager;

//        public DataSeeeding(StoreDbContext context, StoreIdntityDbContext storeIdntityDbContext , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager )
//        {
//            this.context = context;
//            this.storeIdntityDbContext = storeIdntityDbContext;
//            this.userManager = userManager;
//            this.roleManager = roleManager;
//        }
//        public async Task DataSeedAsync()
//        {
//            //msh manteky en kolo ykon async asln 
//            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
//            if (pendingMigrations.Any())
//                context.Database.Migrate();


//            //bd2na b da 3shan el product mo3tamed 3lih fel foreign key
//            if (!context.ProductBrands.Any())
//            {
//                var productBrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\brands.json");
//                var productbrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
//                if (productbrands is not null && productbrands.Any())
//                {
//                    context.ProductBrands.AddRange(productbrands);
//                    //context.SaveChanges();
//                }
//            }

//            //bd2na b da 3shan el product mo3tamed 3lih fel foreign key
//            if (!context.ProductTypes.Any())
//            {
//                var ProductTypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\types.json");
//                var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
//                if (ProductTypes is not null && ProductTypes.Any())
//                {
//                    context.ProductTypes.AddRange(ProductTypes);
//                    //context.SaveChanges();
//                }
//            }

//            if (!context.Products.Any())
//            {
//                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\products.json");
//                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
//                if (Products is not null && Products.Any())
//                {
//                    context.Products.AddRange(Products);
//                    //context.SaveChanges();
//                }
//            }

//            if (!context.DeliveryMethods.Any())
//            {
//                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Ecommerce.Presistence\Data\delivery.json");

//                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

//                if (deliveryMethods is not null && deliveryMethods.Any())
//                    context.DeliveryMethods.AddRange(deliveryMethods);
//            }
//            context.SaveChanges();
//        }

//        public async Task IdentityInitializAsync()
//        {

//            try
//            {
//                if (!roleManager.Roles.Any())
//                {
//                    await roleManager.CreateAsync(new IdentityRole("Admin"));
//                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

//                }

//                if (!userManager.Users.Any())
//                {
//                    var User1 = new ApplicationUser()
//                    {
//                        Email = "saif@gmail.com",
//                        DisplayName = "Saif Lotfy",
//                        PhoneNumber = "1234567890",
//                        UserName = "Saif",
//                    };
//                    var User2 = new ApplicationUser()
//                    {
//                        Email = "omar@gmail.com",
//                        DisplayName = "Omar Lotfy",
//                        PhoneNumber = "1234567890",
//                        UserName = "Omar",
//                    };
//                    await userManager.CreateAsync(User1, "P@ssw0rd");
//                    await userManager.CreateAsync(User2, "P@ssw0rd");

//                    await userManager.AddToRoleAsync(User1, "SuperAdmin");
//                    await userManager.AddToRoleAsync(User2, "Admin");


//                }


//                await storeIdntityDbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }


//        }
//    }
//}

#endregion


using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Presistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO; // Ensure this is using System.IO
using System.Linq;
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

        public DataSeeeding(StoreDbContext context, StoreIdntityDbContext storeIdntityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.storeIdntityDbContext = storeIdntityDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task DataSeedAsync()
        {
            // 1. Get the path to wwwroot/Data regardless of where the app is running
            // Directory.GetCurrentDirectory() returns the root folder of the published app on the server
            var webRootPath = Directory.GetCurrentDirectory();
            var dataPath = Path.Combine(webRootPath, "wwwroot", "Data");

            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                context.Database.Migrate();


            // Brands
            if (!context.ProductBrands.Any())
            {
                var filePath = Path.Combine(dataPath, "brands.json");
                if (File.Exists(filePath))
                {
                    var productBrandsData = await File.ReadAllTextAsync(filePath);
                    var productbrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (productbrands is not null && productbrands.Any())
                    {
                        context.ProductBrands.AddRange(productbrands);
                    }
                }
            }

            // Types
            if (!context.ProductTypes.Any())
            {
                var filePath = Path.Combine(dataPath, "types.json");
                if (File.Exists(filePath))
                {
                    var ProductTypesData = await File.ReadAllTextAsync(filePath);
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        context.ProductTypes.AddRange(ProductTypes);
                    }
                }
            }

            // Products
            if (!context.Products.Any())
            {
                var filePath = Path.Combine(dataPath, "products.json");
                if (File.Exists(filePath))
                {
                    var ProductsData = await File.ReadAllTextAsync(filePath);
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        context.Products.AddRange(Products);
                    }
                }
            }

            // Delivery Methods
            if (!context.DeliveryMethods.Any())
            {
                var filePath = Path.Combine(dataPath, "delivery.json");
                if (File.Exists(filePath))
                {
                    var deliveryData = await File.ReadAllTextAsync(filePath);
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                    if (deliveryMethods is not null && deliveryMethods.Any())
                        context.DeliveryMethods.AddRange(deliveryMethods);
                }
            }

            // Only save changes if we actually added something
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
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

                    // Check if user exists first to prevent errors
                    if (await userManager.FindByEmailAsync(User1.Email) == null)
                    {
                        await userManager.CreateAsync(User1, "P@ssw0rd");
                        await userManager.AddToRoleAsync(User1, "SuperAdmin");
                    }

                    if (await userManager.FindByEmailAsync(User2.Email) == null)
                    {
                        await userManager.CreateAsync(User2, "P@ssw0rd");
                        await userManager.AddToRoleAsync(User2, "Admin");
                    }
                }
                // Saving is handled by UserManager, no need for context.SaveChanges here usually, 
                // but if you have custom logic:
                // await storeIdntityDbContext.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                // Ideally log this error
                throw;
            }
        }
    }
}
