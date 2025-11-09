
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Presistence.Contexts;
using Ecommerce.Presistence.Data_Seed;
using Ecommerce.Presistence.Identity.Models;
using Ecommerce.Presistence.Repository;
using Ecommerce.Presistence.UnitOfWork;
using Ecommerce.Service.businessServices;
using Ecommerce.Service.MappingProfiles;
using Ecommerce.Shared.Common.ErrorModels;
using ECommerce.Web.Custom_MiddleWares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace ECommerce.Web
{
    public class Program
    {
        public  static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            //db context registration
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) => //its Singlton bec of we need as we move in the app 
                //creating a Func
                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"))

           );
            builder.Services.AddDbContext<StoreIdntityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddAutoMapper(typeof(Program));


            //business Services
            builder.Services.AddScoped<IdataSeed, DataSeeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfiles(builder.Configuration)));//3shan ash8l el sora 
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.Configure<ApiBehaviorOptions>(
                (options) => options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(e => e.Value.Errors.Any())
                    .Select(e => new PramValidationError()
                    {
                        Field = e.Key,
                        Errors = e.Value.Errors.Select(e => e.ErrorMessage)
                    });

                    var Response = new PramValidationsReturn()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(Response);
                }


            );

            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdntityDbContext>();

            //builder.Services.AddScoped<>
            var app = builder.Build();

            //data seeding

            SeedDatabase(app).GetAwaiter().GetResult();

            //await SeedDatabaseAsync(app);

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //var dataSeed = services.GetRequiredService<IdataSeed>();
            //dataSeed.DataSeedAsync();
            //dataSeed.IdentityInitializAsync();





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseMiddleware<CustomExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }

        /// <summary>
        /// Seeds both the Identity and App databases with initial data.
        /// </summary>

        private static async Task SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbIntializer = scope.ServiceProvider.GetRequiredService<IdataSeed>();
            await DbIntializer.DataSeedAsync();
            await DbIntializer.IdentityInitializAsync();


          
        }
    }
}
