#region program.cs before deployment and using swagger 

//using Ecommerce.Abstraction.Services;
//using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
//using Ecommerce.Domain.Models.Contracts.Repository;
//using Ecommerce.Domain.Models.Contracts.Seed;
//using Ecommerce.Domain.Models.Contracts.UOW;
//using Ecommerce.Domain.Models.Identity;
//using Ecommerce.Presistence.Contexts;
//using Ecommerce.Presistence.Data_Seed;
////using Ecommerce.Presistence.Identity.Models;
//using Ecommerce.Presistence.Repository;
//using Ecommerce.Presistence.UnitOfWork;
//using Ecommerce.Service.businessServices;
//using Ecommerce.Service.MappingProfiles;
//using Ecommerce.Shared.Common.ErrorModels;
//using ECommerce.Web.Custom_MiddleWares;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.HttpOverrides;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using StackExchange.Redis;
//using System.Text;

//namespace ECommerce.Web
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//            builder.Services.AddOpenApi();


//            //db context registration
//            builder.Services.AddSingleton<IConnectionMultiplexer>((_) => //its Singlton bec of we need as we move in the app 
//                                                                         //creating a Func
//                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"))

//           );
//            builder.Services.AddDbContext<StoreIdntityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

//            builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//            //builder.Services.AddAutoMapper(typeof(Program));


//            //business Services
//            builder.Services.AddScoped<IdataSeed, DataSeeeding>();
//            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfiles(builder.Configuration)));//3shan ash8l el sora 
//            builder.Services.AddScoped<IServiceManger, ServiceManger>();
//            builder.Services.Configure<ApiBehaviorOptions>(
//                (options) => options.InvalidModelStateResponseFactory = (context) =>
//                {
//                    var errors = context.ModelState.Where(e => e.Value.Errors.Any())
//                    .Select(e => new PramValidationError()
//                    {
//                        Field = e.Key,
//                        Errors = e.Value.Errors.Select(e => e.ErrorMessage)
//                    });

//                    var Response = new PramValidationsReturn()
//                    {
//                        Errors = errors
//                    };
//                    return new BadRequestObjectResult(Response);
//                }


//            );

//            builder.Services.AddScoped<ICartRepo, CartRepo>();
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreIdntityDbContext>();
//            builder.Services.AddScoped<ICacheRepo, CacheRepo>();
//            builder.Services.AddScoped<ICacheService, CacheService>();


//            builder.Services.AddAuthentication(config =>
//            {
//                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddCookie() // Needed for OAuth flow
//            .AddGoogle(options =>
//            {
//                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]
//                    ?? throw new InvalidOperationException("Google ClientId is not configured");

//                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
//                    ?? throw new InvalidOperationException("Google ClientSecret is not configured");

//                // Match what's in Google Console


//                options.CallbackPath = "/signin-google";  // <= Changed to match Google Console

//                /*
//                    This tells ASP.NET : "When Google finishes authenticating the user they will redirect to "https://localhost:7109/signin-google"
//                    What happens=>
//                    {
//                        User clicks => Login with Google
//                        Redirects to Google's login page
//                        User logs in at Google
//                        Google redirects back to "https://localhost:7109/signin-google​​"
//                        ASP.NET middleware automatically handles this path extracts user info from Google, and stores it in a temporary cookie​
//                        This MUST match what you configured in Google Cloud Console under => Authorised redirect URIs
//                    }

//                 */


//                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//            }).AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters()
//                {
//                    ValidateIssuer = true,
//                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
//                    ValidateAudience = true,
//                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
//                    ValidateLifetime = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(
//                        Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecurityKey"])
//                    )
//                };
//            });










//            //builder.Services.AddScoped<>
//            var app = builder.Build();

//            app.UseForwardedHeaders(new ForwardedHeadersOptions
//            {
//                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//            });
//            //data seeding

//            SeedDatabase(app).GetAwaiter().GetResult();

//            //await SeedDatabaseAsync(app);

//            //using var scope = app.Services.CreateScope();
//            //var services = scope.ServiceProvider;
//            //var dataSeed = services.GetRequiredService<IdataSeed>();
//            //dataSeed.DataSeedAsync();
//            //dataSeed.IdentityInitializAsync();





//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.MapOpenApi();
//            }

//            app.UseMiddleware<CustomExceptionMiddleWare>();

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseAuthentication();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }

//        /// <summary>
//        /// Seeds both the Identity and App databases with initial data.
//        /// </summary>

//        private static async Task SeedDatabase(WebApplication app)
//        {
//            using var scope = app.Services.CreateScope();
//            var DbIntializer = scope.ServiceProvider.GetRequiredService<IdataSeed>();
//            await DbIntializer.DataSeedAsync();
//            await DbIntializer.IdentityInitializAsync();



//        }
//    }
//}

#endregion


#region program with swagger
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Models.Contracts.RedisInMemoryRepository;
using Ecommerce.Domain.Models.Contracts.Repository;
using Ecommerce.Domain.Models.Contracts.Seed;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Presistence.Contexts;
using Ecommerce.Presistence.Data_Seed;
using Ecommerce.Presistence.Repository;
using Ecommerce.Presistence.UnitOfWork;
using Ecommerce.Service.businessServices;
using Ecommerce.Service.MappingProfiles;
using Ecommerce.Shared.Common.ErrorModels;
using ECommerce.Web.Custom_MiddleWares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
//using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
namespace ECommerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(
                    builder.Configuration.GetConnectionString("RedisConnection")!
                )
            );

            // DbContexts
            builder.Services.AddDbContext<StoreIdntityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
            );

            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Business services
            builder.Services.AddScoped<IdataSeed, DataSeeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(m =>
                m.AddProfile(new ProjectProfiles(builder.Configuration))
            );
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            // Validation response customization
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value!.Errors.Any())
                        .Select(e => new PramValidationError
                        {
                            Field = e.Key,
                            Errors = e.Value!.Errors.Select(er => er.ErrorMessage)
                        });

                    var response = new PramValidationsReturn
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // Repositories & cache
            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<ICacheRepo, CacheRepo>();
            builder.Services.AddScoped<ICacheService, CacheService>();

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdntityDbContext>();

            // Authentication (Google + JWT)
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie() // Needed for OAuth flow
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]
                    ?? throw new InvalidOperationException("Google ClientId is not configured");

                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
                    ?? throw new InvalidOperationException("Google ClientSecret is not configured");

                options.CallbackPath = "/signin-google";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["JWTOptions:SecurityKey"]
                            ?? throw new InvalidOperationException("JWT SecurityKey is not configured")
                        )
                    )
                };
            });

            var app = builder.Build();

            // Respect X-Forwarded-* headers (needed on MonsterASP / reverse proxy)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Seed databases
            SeedDatabase(app).GetAwaiter().GetResult();

            // Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // Optional: enable Swagger in production too
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
                    // c.RoutePrefix = string.Empty; // uncomment to show Swagger UI at '/'
                });
            }

            // Custom exception handling
            app.UseMiddleware<CustomExceptionMiddleWare>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        /// <summary>
        /// Seeds both the Identity and App databases with initial data.
        /// </summary>
        private static async Task SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IdataSeed>();
            await dbInitializer.DataSeedAsync();
            await dbInitializer.IdentityInitializAsync();
        }
    }
}

#endregion
