
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Presistence.Contexts;
using Ecommerce.Presistence.Data_Seed;
using Ecommerce.Presistence.UnitOfWork;
using Ecommerce.Service.businessServices;
using Ecommerce.Service.MappingProfiles;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            //db context registration
            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IdataSeed, DataSeeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new ProjectProfiles()));
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            var app = builder.Build();

            //data seeding
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dataSeed = services.GetRequiredService<IdataSeed>();
            dataSeed.DataSeedAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
